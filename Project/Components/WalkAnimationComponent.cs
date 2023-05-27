using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;
using Microsoft.Xna.Framework;

namespace Project.Components
{
    public class WalkAnimationComponent : Component
    {
        private AsepriteAnimation _animation;
        private GameObject _gameObject;

        private string _idleAnimation = "idle";
        private string _walkAnimation = "walk";

        private Vector2 _lastPosition;

        public WalkAnimationComponent(GameObject gameObject, AsepriteAnimation animation)
        {
            _animation = animation;
            _gameObject = gameObject;

            _lastPosition = gameObject.Position;
        }

        public override void Update(GameTime gameTime)
        {
            var direction = _gameObject.Position - _lastPosition;
            string currentAnimation = _idleAnimation;

            if (direction.Length() > 0) currentAnimation = _walkAnimation;

            _animation.Play(gameTime, currentAnimation, AsepriteAnimation.AnimationDirection.LOOP);

            base.Update(gameTime);

            _gameObject.Body = _animation.Body;
            _lastPosition = _gameObject.Position;
        }
    }
}