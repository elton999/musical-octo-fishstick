using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;
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
        private bool _canJump { get => _jumpAnimation.IsGrounded && _actor.Gravity2D.Length() > 0; }

        private JumpAnimation _jumpAnimation;

        public bool InputEnable = true;

        public void SetJumpForce(float jumpForce) => _jumpForce = jumpForce;

        public override void Start()
        {
            _actor = GameObject.GetActor();
            _jumpAnimation = GameObject.GetComponent<JumpAnimation>();
        }

        public override void Update(GameTime gameTime)
        {
            if (KeyBoardHandler.KeyPressed(Keys.Z) && _canJump && !_jump && InputEnable)
                _jump = true;
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