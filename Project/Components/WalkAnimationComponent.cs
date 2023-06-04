using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;

namespace Project.Components
{
    public class WalkAnimationComponent : Component
    {
        private Actor _actor;
        private AnimationComponent _animation;
        private JumpAnimation _jumpAnimation;

        private string _idleAnimation = "idle";
        private string _walkAnimation = "walk";
        private string _currentAnimation;

        public override void Start()
        {
            _animation = GameObject.GetComponent<AnimationComponent>();
            _jumpAnimation = GameObject.GetComponent<JumpAnimation>();
            _actor = GameObject.GetActor();
            _currentAnimation = _idleAnimation;
        }

        public override void Update(GameTime gameTime)
        {
            if (_jumpAnimation.IsGrounded || _actor.Gravity2D.Length() == 0)
                _animation.SetAnimation(_currentAnimation);
            base.Update(gameTime);
        }

        public override void UpdateData(GameTime gameTime)
        {
            if (_actor.Velocity.X != 0.0f) _currentAnimation = _walkAnimation;
            else if (_actor.Gravity2D.Length() == 0 && !(_actor.Velocity.Length() == 0)) _currentAnimation = _walkAnimation;
            else _currentAnimation = _idleAnimation;

            base.UpdateData(gameTime);
        }
    }
}