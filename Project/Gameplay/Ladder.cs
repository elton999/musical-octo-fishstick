using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;

namespace Project.Gameplay
{
    public class Ladder : Actor
    {
        private UmbrellaToolsKit.Sprite.Square _square;

        public override void Start()
        {
            base.Start();
            Gravity2D = Vector2.Zero;

            _square = new UmbrellaToolsKit.Sprite.Square();
            _square.SquareColor = Color.Green;
            _square.size = size;
            _square.Position = Position;
            Scene.AddGameObject(_square);
        }
    }
}
