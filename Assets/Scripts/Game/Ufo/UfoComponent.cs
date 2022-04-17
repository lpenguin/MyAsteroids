using System;
using Game.HitReceiver;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Ufo
{
    public class UfoComponent: MonoBehaviour, IGameComponent, IHitReceiver
    {
        [SerializeField] 
        private UfoDefinition definition;

        private UfoController _controller;
        
        private void Awake()
        {
            Assert.IsTrue(TryGetComponent<Rigidbody2D>(out var body), 
                $"Must have a {nameof(Rigidbody2D)}");

            _controller = new UfoController(this, definition, body);
        }

        private void Update()
        {
            _controller.Update(Time.deltaTime);
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            _controller.HandleCollision(col.collider);
        }

        public Transform Transform => transform;
        
        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }

        public void ReceiveHit(ReceiveHitData data)
        {
            _controller?.ReceiveHit(data);
        }
    }
}