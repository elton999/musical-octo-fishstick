using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;

namespace Project.Components
{
    public class DeathAnimationComponent : Component
    {
        private AnimationComponent _animation;
        private string _animationName = "death";
        private bool _stopAllComponents = false;

        public override void Start()
        {
            _animation = GameObject.GetComponent<AnimationComponent>();
            HealthComponent.OnAnyEntityDie += OnEntityDie;
        }

        public override void OnDestroy() => HealthComponent.OnAnyEntityDie -= OnEntityDie;

        public void OnEntityDie(GameObject gameObject)
        {
            if (gameObject != GameObject) return;
            _animation.SetAnimation("death", AsepriteAnimation.AnimationDirection.FORWARD);
            StopAllComponents();
        }

        public void StopAllComponents() => _stopAllComponents = true;

        public override void Update(GameTime gameTime)
        {
            if (_stopAllComponents) return;
            base.Update(gameTime);
        }

        public override void UpdateData(GameTime gameTime)
        {
            if (_stopAllComponents) return;
            base.UpdateData(gameTime);
        }

    }
}
