using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;

namespace Project.Components
{
    public class AnimationComponent : Component
    {
        [ShowEditor] private string _currentAnimation = "";
        private string _animationSettingsPath;
        private AsepriteAnimation _animation;
        private AsepriteAnimation.AnimationDirection _animationDirection;

        public void SetPath(string animationSettingsPath)
        {
            _animationSettingsPath = animationSettingsPath;
            var asepriteAnimation = GameObject.Scene.Content.Load<AsepriteDefinitions>(_animationSettingsPath);
            _animation = new AsepriteAnimation(asepriteAnimation);
        }

        public override void Update(GameTime gameTime)
        {
            if (_animation != null)
            {
                _animation.Play(gameTime, _currentAnimation, _animationDirection);
                GameObject.Body = _animation.Body;
            }
            base.Update(gameTime);
        }

        public void SetAnimation(string animationName, AsepriteAnimation.AnimationDirection direction = AsepriteAnimation.AnimationDirection.LOOP)
        {
            _animationDirection = direction;
            _currentAnimation = animationName;
        }

    }
}