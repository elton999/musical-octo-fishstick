using System;
using System.Collections.Generic;
using UmbrellaToolsKit;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project.Gameplay
{
    public class Light : GameObject
    {
        private RenderTarget2D _BackBuffer;
        private Texture2D _lightSprite;
        private Effect _effect;
        private GameTime _gameTime;

        public static List<Tuple<float, GameObject>> Points = new List<Tuple<float, GameObject>>();

        public override void Start()
        {
            _lightSprite = Scene.Content.Load<Texture2D>("Sprites/light_sprite");
            _effect = Scene.Content.Load<Effect>("Shaders/Light");
            _effect.Parameters["Scale1"].SetValue(0.5f);
            _effect.Parameters["Scale2"].SetValue(0.05f);
            _effect.Parameters["LightColor1"].SetValue((new Color(65, 97, 251)).ToVector4());
            _effect.Parameters["LightColor2"].SetValue((new Color(32, 0, 178)).ToVector4());

            Tag = "Light";
            _BackBuffer = new RenderTarget2D(Scene.ScreenGraphicsDevice, Scene.Sizes.X, Scene.Sizes.Y);

            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            _gameTime = gameTime;
            base.Update(gameTime);
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
                    Scale += point.Item1;
                    Vector2 SpriteSize = new Vector2(_lightSprite.Width, _lightSprite.Height);
                    Origin = SpriteSize * 0.5f;
                    Position = point.Item2.Position + point.Item2.size.ToVector2() / 2.0f;
                    DrawSprite(spriteBatch);
                }
                catch { }
            }
            EndDraw(spriteBatch);
            Scene.ScreenGraphicsDevice.SetRenderTarget(null);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Effect = _effect;
            BeginDraw(spriteBatch);
            Position = Vector2.Zero;
            Scale = 1.0f;
            Origin = Vector2.Zero;
            Sprite = (Texture2D)_BackBuffer;
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