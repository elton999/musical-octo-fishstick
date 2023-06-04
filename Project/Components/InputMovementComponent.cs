using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolsKit;

namespace Project.Components
{
    public class InputMovementComponent : Component
    {
        private MovementComponent _movementComponent;

        public override void Start() => _movementComponent = GameObject.GetComponent<MovementComponent>();

        public override void Update(GameTime gameTime)
        {
            Vector2 direction = Vector2.Zero;

            bool left = Keyboard.GetState().IsKeyDown(Keys.Left);
            bool right = Keyboard.GetState().IsKeyDown(Keys.Right);
            direction.X = left ? -1 : right ? 1 : 0;

            bool up = Keyboard.GetState().IsKeyDown(Keys.Up);
            bool down = Keyboard.GetState().IsKeyDown(Keys.Down);

            direction.Y = up ? -1 : down ? 1 : 0;

            if (direction.Length() > 0)
                direction.Normalize();

            if (_movementComponent != null)
                _movementComponent.AddDirection(direction);

            base.Update(gameTime);
        }
    }
}