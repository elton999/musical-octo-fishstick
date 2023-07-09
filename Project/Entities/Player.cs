using System;
using System.Collections;
using Project.Components;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Utils;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Entities
{
    public class Player : Actor, IScalableVector
    {
        public static event Action OnDieDaley;
        public static event Action OnDie;

        private HealthComponent _health;
        private CoroutineManagement _dieCoroutine = new CoroutineManagement();
        private CoroutineManagement _spriteCoroutine = new CoroutineManagement();

        private CheatListener _playerCheat;

        public float Speed = 8f;
        public float JumpForce = 220f;

        public Vector2 ScaleVector { get; set; }
        public Vector2 OffsetVector { get; set; }

        public static bool CollectedKey = false;

        public override void Start()
        {
            tag = "Player";

            Sprite = Scene.Content.Load<Texture2D>("Sprites/player");

            size = new Point(16, 16);
            Gravity2D = Vector2.UnitY * 30f;
            MaxVelocity = JumpForce;

            ScaleVector = Vector2.One;
            OffsetVector = Vector2.Zero;

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
            AddComponent<SmashSpriteOnFailComponent>().SetScaler(this);

            _health.OnDie += OnPlayerDie;

            _playerCheat = new CheatListener();
            _playerCheat.AddCheat(Keys.F1, _health.BeImmortal);
            Scene.AddGameObject(_playerCheat);

            base.Start();
        }

        public void OnPlayerDie() => _dieCoroutine.StarCoroutine(OnDieDelay());

        public IEnumerator OnDieDelay()
        {
            yield return _dieCoroutine.Wait(200.0f);
            OnDie?.Invoke();
            yield return _dieCoroutine.Wait(1000.0f);
            OnDieDaley?.Invoke();
            yield return null;
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

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            if (Sprite != null)
                spriteBatch.Draw(Sprite, Position, Body.IsEmpty ? null : Body, SpriteColor * Transparent, Rotation, Origin + OffsetVector, ScaleVector, spriteEffect, 0);
        }
    }
}

