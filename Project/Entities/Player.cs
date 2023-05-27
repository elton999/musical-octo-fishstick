using Microsoft.Xna.Framework;
using UmbrellaToolsKit.Sprite;
using Project.Components;

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

            var movementComponent = new MovementComponent(this, Speed);

            AddComponent(new InputMovementComponent(movementComponent));
            AddComponent(movementComponent);
        }
    }
}

