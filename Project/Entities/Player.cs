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

        public float Speed = 30f;

        public override void Start()
        {
            var movementComponent = new MovementComponent(this, Speed);

            tag = "Player";

            Sprite = Scene.Content.Load<Texture2D>("Sprites/player");

            var asepriteAnimation = Scene.Content.Load<AsepriteDefinitions>("Sprites/player_animation");
            _animation = new AsepriteAnimation(asepriteAnimation);

            size = new Point(16, 16);

            AddComponent(new InputMovementComponent(movementComponent));
            AddComponent(movementComponent);
            AddComponent(new WalkAnimationComponent(this, _animation));

            base.Start();
        }
    }
}

