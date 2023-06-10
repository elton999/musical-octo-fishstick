using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;

namespace Project.Gameplay
{
    public class Nodes : GameObject
    {
        private Square _square;
        public override void Start()
        {
            _square = new Square();
            _square.SquareColor = Color.Blue;
            _square.Scene = Scene;
            _square.size = new Point(8, 8);
            _square.Origin = new Vector2(4, 4);
            _square.Start();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _square.BeginDraw(spriteBatch);
            foreach (var nodePosition in Nodes)
            {
                _square.Position = nodePosition;
                _square.DrawSprite(spriteBatch);
            }
            _square.EndDraw(spriteBatch);
        }
    }
}