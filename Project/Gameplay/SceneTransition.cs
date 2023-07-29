using System;
using System.Collections;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Gameplay
{
    public class SceneTransition : Square
    {
        public float EffectFactor = 0.0f;
        public float Speed = 4.0f;

        public CoroutineManagement coroutineManagement = new CoroutineManagement();

        public override void Start()
        {
            SquareColor = Color.White;
            size = Scene.Sizes;
            Position = Vector2.Zero;

            Effect = Scene.Content.Load<Effect>("Shaders/CircleEffect");
            Effect.Parameters["factor"].SetValue(EffectFactor);

            coroutineManagement.StarCoroutine(OpenAnimationCoroutine());

            Entities.Player.OnDie += StartCloseAnimation;
            Gameplay.Door.OnEnterDoor += StartCloseAnimation;

            base.Start();
        }

        public override void OnDestroy()
        {
            Entities.Player.OnDie -= StartCloseAnimation;
            Gameplay.Door.OnEnterDoor -= StartCloseAnimation;
        }

        public void StartCloseAnimation() => coroutineManagement.StarCoroutine(CloseAnimationCoroutine());

        public IEnumerator OpenAnimationCoroutine()
        {
            EffectFactor = 0.0f;
            while (EffectFactor < 1.0f)
            {
                float dt = (float)coroutineManagement.GameTime.ElapsedGameTime.TotalSeconds;
                EffectFactor = Math.Clamp(EffectFactor + Speed * dt, 0.0f, 1.0f);
                Effect.Parameters["factor"].SetValue(EffectFactor);
                yield return null;
            }

            yield return null;
        }

        public IEnumerator CloseAnimationCoroutine()
        {
            EffectFactor = 1.0f;
            while (EffectFactor > 0.0f)
            {
                float dt = (float)coroutineManagement.GameTime.ElapsedGameTime.TotalSeconds;
                EffectFactor = Math.Clamp(EffectFactor - Speed * dt, 0.0f, 1.0f);
                Effect.Parameters["factor"].SetValue(EffectFactor);
                yield return null;
            }

            yield return null;
        }

        public override void Update(GameTime gameTime)
        {
            coroutineManagement.Update(gameTime);
            base.Update(gameTime);
        }
    }
}