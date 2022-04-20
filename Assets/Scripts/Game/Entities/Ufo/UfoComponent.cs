using Game.Entities.HitReceiver;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Entities.Ufo
{
    public class UfoComponent: MonoBehaviour, IHitReceiver
    {
        [SerializeField] 
        private UfoDefinition definition;

        private UfoController _controller;
        
        private void Awake()
        {
            var rigidbody2D = GetComponent<Rigidbody2D>();
            Assert.IsNotNull(rigidbody2D, $"Must have a {nameof(Rigidbody2D)}");

            UfoView ufoView = new UfoView(rigidbody2D, transform, definition);
            _controller = new UfoController(ufoView, definition);
        }

        private void Update()
        {
            _controller.Update(Time.deltaTime);
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            _controller.HandleCollision(col.collider);
        }

        public void ReceiveHit(HitData data)
        {
            _controller.ReceiveHit(data);
        }
    }
}