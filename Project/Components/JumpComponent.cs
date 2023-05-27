using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project.Components
{
    public class JumpComponent : Component
    {
        private Actor _actor;
        private float _jumpForce;
        private bool canJump = true;

        public JumpComponent(Actor actor, float jumpForce)
        {
            _actor = actor;
            _jumpForce = jumpForce;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Z) && canJump)
            {
                canJump = false;
                _actor.Velocity.Y -= _jumpForce;
            }
            base.Update(gameTime);
        }

    }
}