using Game.Entities.HitReceiver;
using Game.UI;
using Game.Utils;
using UnityEngine;

namespace Game.Entities.Weapon.Laser
{
    public class LaserWeapon : IWeapon, ILaserWeapon
    {
        private readonly LaserWeaponDefinition _definition;
        private readonly Transform _parent;
        private bool _isShooting;
        private ObservableFloat _charge;
        private Transform _effect;
        private Quaternion _lastRotation;
        private RaycastHit2D[] _raycastResults = new RaycastHit2D[16];

        public LaserWeapon(LaserWeaponDefinition definition, Transform parent)
        {
            _definition = definition;
            _parent = parent;
            _lastRotation = _parent.rotation;
            _charge = new ObservableFloat(1f);
        }

        public ObservableFloat Charge => _charge;

        public void Shoot()
        {
            if (_charge.Value < 0.9)
            {
                return;
            }

            _charge.Value -= _definition.firstShotAmmo;

            _isShooting = true;

            _effect = Object.Instantiate(_definition.laserEffectPrefab, _parent.position, _parent.rotation, _parent).transform;
        }

        public void CancelShoot()
        {
            _isShooting = false;
            if (_effect != null)
            {
                Object.Destroy(_effect.gameObject);
            }
        }

        public void UpdateWeapon(float timeStep)
        {
            if (!_isShooting)
            {
                _charge.Value = Mathf.Min(1f, _charge.Value + _definition.restorePerSec * timeStep);
            }
            else
            {
                ShootRays();

                _charge.Value = Mathf.Max(0, _charge.Value - _definition.ammoPerSec * timeStep);
                if (_charge.Value == 0)
                {
                    CancelShoot();
                }
            }

            _lastRotation = _parent.rotation;
        }

        public void SetupUI(IPlayerWeaponsUI gameUI)
        {
            gameUI.SetupLaserWeapon(this);
        }

        private void ShootRays()
        {
            Quaternion currentRotation = _parent.rotation;

            float traceStepRad = Mathf.Deg2Rad * _definition.traceStep;
            do
            {
                Vector2 direction = currentRotation * Vector2.up;
                var hitCount = RayCast(ref _raycastResults, _parent.position, direction, _definition.hitMask);

                for (int i = 0; i < hitCount; i++)
                {
                    var hit = _raycastResults[i];

                    if (hit.collider != null &&
                        hit.collider.TryGetComponent<IHitReceiver>(out var hitReceiver))
                    {
                        hitReceiver.ReceiveHit(
                            new HitData
                            {
                                Damage = _definition.damage,
                                DamageType = DamageType.PreventSpawning,
                            }
                        );
                    }
                }

                currentRotation = Quaternion.RotateTowards(currentRotation, _lastRotation, traceStepRad);
            } while (Quaternion.Angle(currentRotation, _lastRotation) >= traceStepRad);
        }

        private static int RayCast(ref RaycastHit2D[] raycastResults, Vector2 position, Vector2 direction, int mask)
        {
            int hitCount;
            while (true)
            {
                hitCount = Physics2D.RaycastNonAlloc(position, direction, raycastResults, Mathf.Infinity, mask);
                if (hitCount <= raycastResults.Length)
                {
                    break;
                }

                raycastResults = new RaycastHit2D[hitCount * 2];
            }

            return hitCount;
        }
    }
}