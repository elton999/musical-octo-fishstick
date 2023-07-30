using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;

namespace Project
{
    public class Text : GameObject
    {
        private SpriteFont _font;
        private Vector2 stringSize;

        public override void Start()
        {
            _font = Scene.Content.Load<SpriteFont>("Font");
            tag = "Text";
            stringSize = _font.MeasureString((string)Values["text"]);
            Position = Position - stringSize / 2.0f;
            Position = Position.ToPoint().ToVector2();
            base.Start();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch);
            spriteBatch.DrawString(_font, (string)Values["text"], Position, SpriteColor);
            EndDraw(spriteBatch);
        }
    }
}