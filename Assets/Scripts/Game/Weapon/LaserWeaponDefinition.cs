using Game.GameManager;
using UnityEngine;

namespace Game.Weapon
{
    [CreateAssetMenu(menuName = "MyAsteroids/Laser Weapon Definition")]
    public class LaserWeaponDefinition: WeaponDefinition
    {
        [SerializeField]
        private float traceStep = 5;
        
        [SerializeField] 
        private float ammoPerSec = 0.3f;

        [SerializeField] 
        private float restorePerSec = 0.1f;

        [SerializeField] 
        private LayerMask hitMask;

        [SerializeField] 
        private GameObject laserEffectPrefab;

        [SerializeField] 
        private PlayerState playerState;

        public override IWeapon CreateWeapon(Transform parent) => new Weapon(this, parent);

        private class Weapon : IWeapon
        {
            private readonly LaserWeaponDefinition _definition;
            private readonly Transform _parent;
            private bool _isShooting;
            private float _charge;
            private Transform _effect;
            private Quaternion _lastRotation;

            public Weapon(LaserWeaponDefinition definition, Transform parent)
            {
                _definition = definition;
                _parent = parent;
                _lastRotation = _parent.rotation;
                _charge = 1f;
                // TODO: get rid of playerState.playerData.LaserCharge
                _definition.playerState.playerData.LaserCharge = _charge;
            }

            public void Shoot()
            {
                if (_charge < 0.9)
                {
                    return;
                }
                _isShooting = true;
                
                _effect = Instantiate(_definition.laserEffectPrefab, _parent.position, _parent.rotation, _parent).transform;
                
            }

            public void CancelShoot()
            {
                _isShooting = false;
                if (_effect != null)
                {
                    Destroy(_effect.gameObject);
                }
            }

            public void UpdateWeapon(float timeStep)
            {
                if (!_isShooting)
                {
                    _charge = Mathf.Min(1f, _charge + _definition.restorePerSec * timeStep);
                }
                else
                {
                    ShootRays();

                    _charge = Mathf.Max(0, _charge - _definition.ammoPerSec * timeStep);
                    if (_charge == 0)
                    {
                        CancelShoot();
                    }
                }

                _lastRotation = _parent.rotation;
                _definition.playerState.playerData.LaserCharge = _charge;
            }

            private void ShootRays()
            {
                Quaternion currentRotation = _parent.rotation;

                float traceStepRad = Mathf.Deg2Rad * _definition.traceStep;
                do
                {
                    // TODO: all.
                    // TODO: refactor
                    Vector2 direction = currentRotation * Vector2.up;
                    
                    var hits = Physics2D.RaycastAll(_parent.position, direction, Mathf.Infinity, _definition.hitMask);
                    foreach (var hit in hits)
                    {
                        if (hit.collider != null && 
                            hit.collider.TryGetComponent<IHitReceiver>(out var hitReceiver))
                        {
                            hitReceiver.ReceiveHit(1);
                        }
                    }
                    
                    currentRotation = Quaternion.RotateTowards(currentRotation, _lastRotation, traceStepRad);
                } while (Quaternion.Angle(currentRotation, _lastRotation) >= traceStepRad);
            }
        }
    }
}