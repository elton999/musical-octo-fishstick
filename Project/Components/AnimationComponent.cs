using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;

namespace Project.Components
{
    public class AnimationComponent : Component
    {
        private string _currentAnimation = "";
        private string _animationSettingsPath;
        private AsepriteAnimation _animation;

        public AnimationComponent(string animationSettingsPath) => _animationSettingsPath = animationSettingsPath;

        public override void Start()
        {
            var asepriteAnimation = GameObject.Scene.Content.Load<AsepriteDefinitions>(_animationSettingsPath);
            _animation = new AsepriteAnimation(asepriteAnimation);
        }

        public override void Update(GameTime gameTime)
        {
            _animation.Play(gameTime, _currentAnimation, AsepriteAnimation.AnimationDirection.LOOP);
            GameObject.Body = _animation.Body;
            base.Update(gameTime);
        }

        public void SetAnimation(string animationName) => _currentAnimation = animationName;

    }
}