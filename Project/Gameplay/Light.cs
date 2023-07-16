using System;
using System.Collections;
using System.Collections.Generic;
using UmbrellaToolsKit;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project.Entities;

namespace Project.Gameplay
{
    public class Light : GameObject
    {
        private RenderTarget2D _BackBuffer;
        private RenderTarget2D _BackBuffer2;
        private Texture2D _lightSprite;
        private Effect _effect;
        private Effect _tilemapEffect;
        private float _turnOnSpeed = 5.0f;
        private float _turnOffSpeed = 8.0f;
        private float _lightScaleFactor = 0.0f;
        private GameTime _gameTime;

        private CoroutineManagement coroutineManagement = new CoroutineManagement();

        public static List<Tuple<float, GameObject>> Points = new List<Tuple<float, GameObject>>();

        public override void Start()
        {
            _lightSprite = Scene.Content.Load<Texture2D>("Sprites/light_sprite");
            _effect = Scene.Content.Load<Effect>("Shaders/Light");
            _effect.Parameters["Scale1"].SetValue(0.5f);
            _effect.Parameters["Scale2"].SetValue(0.05f);
            _effect.Parameters["LightColor1"].SetValue((new Color(65, 97, 251)).ToVector4());
            _effect.Parameters["LightColor2"].SetValue((new Color(32, 0, 178)).ToVector4());

            _tilemapEffect = Scene.Content.Load<Effect>("Shaders/TilemapLight");
            _tilemapEffect.Parameters["KeyColor1"]?.SetValue((new Color(255, 0, 0)).ToVector4());
            _tilemapEffect.Parameters["KeyColor2"]?.SetValue((new Color(0, 255, 0)).ToVector4());
            _tilemapEffect.Parameters["NewColor1"]?.SetValue((new Color(32, 0, 178)).ToVector4());
            _tilemapEffect.Parameters["NewColor2"]?.SetValue((new Color(65, 97, 251)).ToVector4());

            _BackBuffer = new RenderTarget2D(Scene.ScreenGraphicsDevice, Scene.Sizes.X, Scene.Sizes.Y);
            _BackBuffer2 = new RenderTarget2D(Scene.ScreenGraphicsDevice, Scene.Sizes.X, Scene.Sizes.Y);
            Tag = "Light";

            coroutineManagement.StarCoroutine(TurnOnLight());

            Player.OnDie += StartTurnOffLight;
            Gameplay.Door.OnEnterDoor += StartTurnOffLight;

            base.Start();
        }

        public override void OnDestroy()
        {
            Player.OnDie -= StartTurnOffLight;
            Gameplay.Door.OnEnterDoor -= StartTurnOffLight;
        }

        public override void Update(GameTime gameTime)
        {
            _gameTime = gameTime;
            coroutineManagement.Update(gameTime);
            base.Update(gameTime);
        }

        private void StartTurnOffLight() => coroutineManagement.StarCoroutine(TurnOffLight());

        private IEnumerator TurnOnLight()
        {
            while (_lightScaleFactor < 1.0f)
            {
                _lightScaleFactor = Math.Clamp(
                    _lightScaleFactor + (float)_gameTime.ElapsedGameTime.TotalSeconds * _turnOnSpeed,
                    0.0f,
                    1.0f
                );
                yield return null;
            }

            yield return null;
        }

        private IEnumerator TurnOffLight()
        {
            _lightScaleFactor = 1.0f;
            while (_lightScaleFactor > 0.0f)
            {
                _lightScaleFactor = Math.Clamp(
                    _lightScaleFactor - (float)_gameTime.ElapsedGameTime.TotalSeconds * _turnOffSpeed,
                    0.0f,
                    1.0f
                );
                yield return null;
            }

            yield return null;
        }

        public override void DrawBeforeScene(SpriteBatch spriteBatch)
        {
            Scene.ScreenGraphicsDevice.SetRenderTarget(_BackBuffer);
            Scene.ScreenGraphicsDevice.Clear(Color.Transparent);
            Effect = null;
            BeginDraw(spriteBatch);
            foreach (var point in Points)
            {
                try
                {
                    Sprite = _lightSprite;
                    Scale = MathF.Cos((float)_gameTime.TotalGameTime.TotalMinutes * 50f) * (point.Item1 / 30f);
                    Scale += point.Item1 * _lightScaleFactor;
                    Vector2 SpriteSize = new Vector2(_lightSprite.Width, _lightSprite.Height);
                    Origin = SpriteSize * 0.5f;
                    Position = point.Item2.Position + point.Item2.size.ToVector2() / 2.0f;
                    DrawSprite(spriteBatch);
                }
                catch { }
            }
            EndDraw(spriteBatch);

            Scene.ScreenGraphicsDevice.SetRenderTarget(null);
            Scene.ScreenGraphicsDevice.SetRenderTarget(_BackBuffer2);
            Scene.ScreenGraphicsDevice.Clear(Color.Transparent);
            Sprite = (Texture2D)_BackBuffer;
            Effect = _effect;

            BeginDraw(spriteBatch);
            Position = Vector2.Zero;
            Scale = 1f;
            Origin = Vector2.Zero;
            DrawSprite(spriteBatch);
            EndDraw(spriteBatch);

            Sprite = (Texture2D)_BackBuffer2;

            _tilemapEffect.Parameters["LightTexture"].SetValue(Sprite);
            Scene.Effect = _tilemapEffect;
            Effect = null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch);
            Position = Vector2.Zero;
            Scale = 1f;
            Origin = Vector2.Zero;
            DrawSprite(spriteBatch);
            EndDraw(spriteBatch);
        }

        public override void Dispose()
        {
            _BackBuffer.Dispose();
            Points.Clear();
            base.Dispose();
        }
    }
}