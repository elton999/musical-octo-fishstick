using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;

namespace Project.Components
{
    public class InputMovementComponent : Component
    {
        private MovementComponent _movementComponent;

        public override void Start() => _movementComponent = GameObject.GetComponent<MovementComponent>();

        public override void Update(GameTime gameTime)
        {
            Vector2 direction = Vector2.Zero;

            bool left = KeyBoardHandler.KeyDown(Keys.Left);
            bool right = KeyBoardHandler.KeyDown(Keys.Right);
            direction.X = left ? -1 : right ? 1 : 0;

            bool up = KeyBoardHandler.KeyDown(Keys.Up);
            bool down = KeyBoardHandler.KeyDown(Keys.Down);

            direction.Y = up ? -1 : down ? 1 : 0;

            if (direction.Length() > 0)
                direction.Normalize();

            if (_movementComponent != null)
                _movementComponent.AddDirection(direction);

            base.Update(gameTime);
        }
    }
}