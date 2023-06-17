using Project.Components;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Entities
{
    public class Player : Actor
    {
        public float Speed = 6f;
        public float JumpForce = 220f;

        public override void Start()
        {
            tag = "Player";

            Sprite = Scene.Content.Load<Texture2D>("Sprites/player");

            size = new Point(16, 16);
            Gravity2D = Vector2.UnitY * 30f;
            MaxVelocity = JumpForce;

            AddComponent<LadderComponent>();
            AddComponent<AnimationComponent>().SetPath("Sprites/player_animation");
            AddComponent<JumpAnimation>();
            AddComponent<MovementComponent>().SetSpeed(Speed);
            AddComponent<InputMovementComponent>();
            AddComponent<RevertSpriteByVelocityComponent>();
            AddComponent<WalkAnimationComponent>();
            AddComponent<JumpComponent>().SetJumpForce(JumpForce);

            base.Start();
        }
    }
}

