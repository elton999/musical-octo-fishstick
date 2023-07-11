using System.Collections.Generic;
using UmbrellaToolsKit;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project.Gameplay
{
    public class Light : GameObject
    {
        public class LightPoint
        {
            public GameObject GameObject;
            public float Scale;

            public LightPoint(GameObject gameObject, float scale)
            {
                GameObject = gameObject;
                Scale = scale;
            }
        }

        private RenderTarget2D _BackBuffer;
        private Texture2D _lightSprite;
        private Effect _effect;

        public static List<LightPoint> Points = new List<LightPoint>();

        public override void Start()
        {
            _lightSprite = Scene.Content.Load<Texture2D>("Sprites/light_sprite");
            _effect = Scene.Content.Load<Effect>("Shaders/Light");
            _effect.Parameters["Scale"].SetValue(0.5f);

            Tag = "Light";
            _BackBuffer = new RenderTarget2D(Scene.ScreenGraphicsDevice, Scene.Sizes.X, Scene.Sizes.Y);

            base.Start();
        }

        public override void DrawBeforeScene(SpriteBatch spriteBatch)
        {
            Scene.ScreenGraphicsDevice.SetRenderTarget(_BackBuffer);
            Scene.ScreenGraphicsDevice.Clear(Color.Transparent);
            Effect = _effect;
            BeginDraw(spriteBatch);
            foreach (var point in Points)
            {
                Sprite = _lightSprite;
                Scale = point.Scale;
                Vector2 SpriteSize = new Vector2(_lightSprite.Width, _lightSprite.Height);
                Origin = SpriteSize * 0.5f;
                Position = point.GameObject.Position + point.GameObject.size.ToVector2() / 2.0f;
                DrawSprite(spriteBatch);
            }
            EndDraw(spriteBatch);
            Scene.ScreenGraphicsDevice.SetRenderTarget(null);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Effect = null;
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