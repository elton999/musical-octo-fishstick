using System;
using System.Collections;
using Project.Components;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Entities
{
    public class Player : Actor
    {
        public static event Action OnDie;

        private HealthComponent _health;
        private CoroutineManagement _dieCoroutine = new CoroutineManagement();
        private CoroutineManagement _spriteCoroutine = new CoroutineManagement();

        public float Speed = 8f;
        public float JumpForce = 220f;

        public static bool CollectedKey = false;

        public override void Start()
        {
            tag = "Player";

            Sprite = Scene.Content.Load<Texture2D>("Sprites/player");

            size = new Point(16, 16);
            Gravity2D = Vector2.UnitY * 30f;
            MaxVelocity = JumpForce;

            _health = AddComponent<HealthComponent>();
            AddComponent<AnimationComponent>().SetPath("Sprites/player_animation");
            AddComponent<DeathAnimationComponent>();
            AddComponent<LadderComponent>();
            AddComponent<JumpAnimation>();
            AddComponent<MovementComponent>().SetSpeed(Speed);
            AddComponent<InputMovementComponent>();
            AddComponent<RevertSpriteByVelocityComponent>();
            AddComponent<WalkAnimationComponent>();
            AddComponent<JumpComponent>().SetJumpForce(JumpForce);

            _health.OnDie += OnPlayerDie;

            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            _dieCoroutine.Update(gameTime);
            _spriteCoroutine.Update(gameTime);
            base.Update(gameTime);
        }

        public override void OnDestroy()
        {
            _health.OnDie -= OnPlayerDie;
            CollectedKey = false;
        }

        public void OnPlayerDie() => _dieCoroutine.StarCoroutine(OnDieDelay());

        public IEnumerator OnDieDelay()
        {
            yield return _dieCoroutine.Wait(300.0f);
            OnDie?.Invoke();
            yield return null;
        }
    }
}

