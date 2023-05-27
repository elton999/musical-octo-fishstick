using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolsKit.Sprite;

namespace Project.Entities
{
    public class Player : Square
    {
        public float Speed = 30f;

        public override void Start()
        {
            SquareColor = Color.Red;
            size = new Point(16, 16);
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 direction;

            bool left = Keyboard.GetState().IsKeyDown(Keys.Left);
            bool right = Keyboard.GetState().IsKeyDown(Keys.Right);
            direction.X = left ? -1 : right ? 1 : 0;

            bool up = Keyboard.GetState().IsKeyDown(Keys.Up);
            bool down = Keyboard.GetState().IsKeyDown(Keys.Down);
            direction.Y = up ? -1 : down ? 1 : 0;

            Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}

