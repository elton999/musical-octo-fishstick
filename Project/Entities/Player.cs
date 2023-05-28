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

            AddComponent(new AnimationComponent("Sprites/player_animation"));
            AddComponent(new MovementComponent(this, Speed));
            AddComponent(new InputMovementComponent());
            AddComponent(new RevertSpriteByVelocityComponent(this));
            AddComponent(new WalkAnimationComponent(this));
            AddComponent(new JumpComponent(this, JumpForce));

            base.Start();
        }
    }
}

