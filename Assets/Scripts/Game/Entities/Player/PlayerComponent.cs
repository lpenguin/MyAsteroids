using System.Collections.Generic;
using Game.Entities.HitReceiver;
using Game.Entities.Weapon;
using Game.Managers.GameManager;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Entities.Player
{
    [AddComponentMenu("MyAsteroids/PlayerComponent")]
    public class PlayerComponent: MonoBehaviour, IPlayerFacade, IHitReceiver
    {
        [SerializeField] 
        private PlayerDefinition definition;
        
        private PlayerHitController _hitController;
        private PlayerWeaponController _weaponController;
        private PlayerInputController _playerInput;
        private PlayerPhysicsController _physicsController;
        private PlayerScoreController _playerScoreController;
        private PlayerData _playerData;

        private void Awake()
        {
            var body = GetComponent<Rigidbody2D>();
            Assert.IsNotNull(body, $"Must have a {nameof(Rigidbody2D)}");

            var spriteRenderer = GetComponent<SpriteRenderer>();
            Assert.IsNotNull(spriteRenderer,  $"Must have a {nameof(SpriteRenderer)}" );
            
            PlayerView playerView = new PlayerView(transform, body, definition, spriteRenderer);
            
            _playerData = new PlayerData();
            _hitController = new PlayerHitController(playerView, _playerData, definition);
            _weaponController = new PlayerWeaponController(playerView, definition);
            _physicsController = new PlayerPhysicsController(playerView, definition);
            _playerScoreController = new PlayerScoreController(_playerData);
            
            _playerInput = new PlayerInputController(
                GameSingleton.Instance.InputManger.PlayerControls, 
                _weaponController, 
                _physicsController);
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

        public IReadOnlyCollection<IWeapon> Weapons => _weaponController.Weapons;
        public PlayerData PlayerData => _playerData;
    }
}