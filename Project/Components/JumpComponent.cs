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
        private bool _canJump = false;

        private JumpAnimation _jumpAnimation;

        public void SetJumpForce(float jumpForce) => _jumpForce = jumpForce;

        public override void Start()
        {
            _actor = GameObject.GetActor();
            _jumpAnimation = GameObject.GetComponent<JumpAnimation>();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Z) && _jumpAnimation.IsGrounded)
                _canJump = true;
            base.Update(gameTime);
        }

        public override void UpdateData(GameTime gameTime)
        {
            if (_canJump)
            {
                _actor.Velocity.Y = -_jumpForce;
                _canJump = false;
            }
            base.UpdateData(gameTime);
        }

    }
}