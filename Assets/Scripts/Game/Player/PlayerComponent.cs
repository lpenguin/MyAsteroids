using System;
using Game.HitReceiver;
using Game.Input;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Player
{
    [AddComponentMenu("MyAsteroids/PlayerComponent")]
    public class PlayerComponent: MonoBehaviour, IGameComponent, IHitReceiver
    {
        [SerializeField] 
        private PlayerDefinition playerParameters;

        [SerializeField]
        private PlayerData _playerData;
        
        private PlayerController _playerController;

        private void Awake()
        {
            // TODO: move to controller
            playerParameters.playerState.playerTransform = transform;
        }

        private void Start()
        {
            Assert.IsTrue(TryGetComponent<Rigidbody2D>(out var physicsBody2DComponent), 
                "Must have a PhysicsBody2DComponent");

            var body = physicsBody2DComponent;
            // TODO: move initialization out PlayerShipController
            _playerData = playerParameters.playerState.playerData;
            _playerData.Score.Value = 0;
            _playerController = new PlayerController(body, playerParameters, this, _playerData);
        }

        private void Update()
        {
            _playerController.Update(Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _playerController.HandleCollisionEnter(col);
        }

        public Transform Transform => transform;
        
        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }

        
        // TODO:
        public void ReceiveHit(ReceiveHitData receiveHitData)
        {
            _playerController.TakeDamage(receiveHitData.Damage);
        }
    }
}