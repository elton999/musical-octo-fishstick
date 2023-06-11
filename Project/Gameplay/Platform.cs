using System;
using UmbrellaToolsKit.Sprite;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;

namespace Project.Gameplay
{
    public class Platform : Solid
    {
        private Square _square;

        public override void Start()
        {
            base.Start();

            _square = new Square();
            _square.SquareColor = Color.Blue;
            _square.size = size;
            _square.Position = Position;
            Scene.AddGameObject(_square);
        }

        public override bool check(Point size, Vector2 position, Actor actor = null)
        {
            if (actor != null && actor.Velocity.Length() > 0 && MathF.Sign(actor.Velocity.Y) == -1)
                return false;
            return base.check(size, position);
        }
    }
}

