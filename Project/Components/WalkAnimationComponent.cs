using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;

namespace Project.Components
{
    public class WalkAnimationComponent : Component
    {
        private Actor _actor;
        private AsepriteAnimation _animation;

        private string _idleAnimation = "idle";
        private string _walkAnimation = "walk";
        private string _currentAnimation;

        private Vector2 _lastPosition;

        public WalkAnimationComponent(Actor actor, AsepriteAnimation animation)
        {
            _animation = animation;
            _actor = actor;

            _currentAnimation = _idleAnimation;
        }

        public override void Update(GameTime gameTime)
        {
            _animation.Play(gameTime, _currentAnimation, AsepriteAnimation.AnimationDirection.LOOP);

            _actor.Body = _animation.Body;
            base.Update(gameTime);
        }

        public override void UpdateData(GameTime gameTime)
        {
            if (_actor.Velocity.X != 0.0f) _currentAnimation = _walkAnimation;
            else _currentAnimation = _idleAnimation;

            base.UpdateData(gameTime);
        }
    }
}