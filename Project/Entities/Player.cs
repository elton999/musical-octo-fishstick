using Project.Components;
using UmbrellaToolsKit.Sprite;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Entities
{
    public class Player : Actor
    {
        private AsepriteAnimation _animation;

        public float Speed = 80f;
        public float JumpForce = 250f;

        public override void Start()
        {
            tag = "Player";

            Sprite = Scene.Content.Load<Texture2D>("Sprites/player");

            var asepriteAnimation = Scene.Content.Load<AsepriteDefinitions>("Sprites/player_animation");
            _animation = new AsepriteAnimation(asepriteAnimation);

            size = new Point(16, 16);
            GravityScale = 20f;
            MaxVelocity = Gravity2D.Length() * GravityScale;

            var movementComponent = new MovementComponent(this, Speed);
            AddComponent(new InputMovementComponent(movementComponent));
            AddComponent(movementComponent);
            AddComponent(new RevertSpriteByVelocityComponent(this));
            AddComponent(new WalkAnimationComponent(this, _animation));
            AddComponent(new JumpComponent(this, JumpForce));

            base.Start();
        }
    }
}

