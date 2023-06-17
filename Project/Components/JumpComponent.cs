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
        private bool _jump = false;
        private bool _jumpButtonPressed = false;
        private bool _canJump { get => _jumpAnimation.IsGrounded && _actor.Gravity2D.Length() > 0; }

        private JumpAnimation _jumpAnimation;

        public void SetJumpForce(float jumpForce) => _jumpForce = jumpForce;

        public override void Start()
        {
            _actor = GameObject.GetActor();
            _jumpAnimation = GameObject.GetComponent<JumpAnimation>();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Z) && !_jumpButtonPressed && _canJump && !_jump)
            {
                _jump = true;
                _jumpButtonPressed = true;
            }

            _jumpButtonPressed = !Keyboard.GetState().IsKeyUp(Keys.Z);
            base.Update(gameTime);
        }

        public override void UpdateData(GameTime gameTime)
        {
            if (_jump)
            {
                _actor.Velocity.Y = -_jumpForce;
                _jump = false;
            }
            base.UpdateData(gameTime);
        }

    }
}