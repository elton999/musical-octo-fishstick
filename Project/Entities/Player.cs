using Project.Components;
using UmbrellaToolsKit.Sprite;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Entities
{
    public class Player : Actor
    {
        public float Speed = 80f;
        public float JumpForce = 250f;

        public override void Start()
        {
            tag = "Player";

            Sprite = Scene.Content.Load<Texture2D>("Sprites/player");

            size = new Point(16, 16);
            GravityScale = 20f;
            MaxVelocity = Gravity2D.Length() * GravityScale;

            AddComponent<AnimationComponent>().SetPath("Sprites/player_animation");
            AddComponent<MovementComponent>().SetSpeed(Speed);
            AddComponent<InputMovementComponent>();
            AddComponent<RevertSpriteByVelocityComponent>();
            AddComponent<WalkAnimationComponent>();
            AddComponent<JumpAnimation>();
            AddComponent<JumpComponent>().SetJumpForce(JumpForce);

            base.Start();
        }
    }
}

