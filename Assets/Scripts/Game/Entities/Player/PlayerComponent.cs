using Game.Entities.HitReceiver;
using Game.Managers.GameManager;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Entities.Player
{
    [AddComponentMenu("MyAsteroids/PlayerComponent")]
    public class PlayerComponent: MonoBehaviour, IHitReceiver
    {
        [SerializeField] 
        private PlayerDefinition definition;
        
        private PlayerHitController _hitController;
        private PlayerWeaponController _weaponController;
        private PlayerInputController _playerInput;
        private PlayerPhysicsController _physicsController;
        private PlayerScoreController _playerScoreController;

        private void Awake()
        {
            Assert.IsTrue(TryGetComponent<Rigidbody2D>(out var rigidbody2D), 
                $"Must have a {nameof(Rigidbody2D)}");

            Assert.IsTrue(TryGetComponent<SpriteRenderer>(out var spriteRenderer),
                $"Must have a {nameof(SpriteRenderer)}" );
            PlayerView playerView = new PlayerView(transform, rigidbody2D, definition, spriteRenderer);
            PlayerData playerData = new PlayerData();
            
            _hitController = new PlayerHitController(playerView, playerData, definition);
            _weaponController = new PlayerWeaponController(playerView, definition);
            _physicsController = new PlayerPhysicsController(playerView, definition);
            _playerScoreController = new PlayerScoreController(playerData);
            
            _playerInput = new PlayerInputController(
                GameSingleton.Instance.InputManger.PlayerControls, 
                _weaponController, 
                _physicsController);
            
            GameSingleton.Instance.GameUI.SetPlayer(playerData);
        }

        private void OnEnable()
        {
            _playerInput.BindControls();
            _playerScoreController.BindEvents();
        }

        private void OnDisable()
        {
            _playerInput.UnbindControls();
            _playerScoreController.UnbindEvents();
        }

        private void Update()
        {
            _weaponController.Update(Time.deltaTime);
            _playerInput.Update();
        }

        private void FixedUpdate()
        {
            _physicsController.FixedUpdate(Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _hitController.HandleCollisionEnter(col);
        }
        
        public void ReceiveHit(HitData hitData)
        {
           _hitController.ReceiveHit(hitData);
        }
    }
}