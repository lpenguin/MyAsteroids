﻿using Game.GameManager;
using Game.HitReceiver;
using Game.Player;
using UnityEngine;

namespace Game.Weapon
{
    [CreateAssetMenu(menuName = "MyAsteroids/Laser Weapon Definition")]
    public class LaserWeaponDefinition: WeaponDefinition
    {

        [Header("General")]
        [SerializeField]
        public float damage = 1f;
        
        [SerializeField]
        public float firstShotAmmo = 0.33f;
        
        [SerializeField]
        public float ammoPerSec = 0.3f;

        [SerializeField]
        public float restorePerSec = 0.1f;

        [Header("Collision")]
        [SerializeField]
        public float traceStep = 5;

        [SerializeField]
        public LayerMask hitMask;

        [SerializeField]
        public GameObject laserEffectPrefab;

        [SerializeField]
        public PlayerState playerState;

        public override IWeapon CreateWeapon(Transform parent, PlayerData playerData) => new LaserWeapon(this, parent, playerData);
    }

    public class LaserWeapon : IWeapon
    {
        private readonly LaserWeaponDefinition _definition;
        private readonly Transform _parent;
        private readonly PlayerData _playerData;
        private bool _isShooting;
        private float _charge;
        private Transform _effect;
        private Quaternion _lastRotation;
        private RaycastHit2D[] _raycastResults = new RaycastHit2D[16];

        public LaserWeapon(LaserWeaponDefinition definition, Transform parent, PlayerData playerData)
        {
            _definition = definition;
            _parent = parent;
            _playerData = playerData;
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

            _charge -= _definition.firstShotAmmo;

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
                Vector2 direction = currentRotation * Vector2.up;
                var hitCount = RayCast(ref _raycastResults, _parent.position, direction, _definition.hitMask);

                for (int i = 0; i < hitCount; i++)
                {
                    var hit = _raycastResults[i];

                    if (hit.collider != null &&
                        hit.collider.TryGetComponent<IHitReceiver>(out var hitReceiver))
                    {
                        hitReceiver.ReceiveHit(
                            new ReceiveHitData
                            {
                                Damage = _definition.damage,
                                PlayerData = _playerData,
                                Owner = this,
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