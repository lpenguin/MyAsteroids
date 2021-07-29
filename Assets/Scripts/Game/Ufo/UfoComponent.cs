using System;
using Game.Physics;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Ufo
{
    public class UfoComponent: MonoBehaviour, IGameComponent, IHitReceiver, OffScreenSpawner.IHasSpawnerParent
    {
        [SerializeField] 
        private UfoDefinition definition;

        private UfoController _controller;
        
        private void Start()
        {
            Assert.IsTrue(TryGetComponent<PhysicsBody2DComponent>(out var physicsBody2DComponent), 
                "Must have a PhysicsBody2DComponent");

            var body = physicsBody2DComponent.Body2D;
            _controller = new UfoController(this, definition, body);
        }

        private void Update()
        {
            _controller.Update(Time.deltaTime);
        }

        public Transform Transform => transform;
        
        public void DestroyGameObject()
        {
            Destroy(gameObject);
            // TODO: move to controller
            Spawner?.Decrement();
        }

        public void ReceiveHit()
        {
            _controller?.HitReceived();
        }

        public OffScreenSpawner Spawner { get; set; }
    }
}