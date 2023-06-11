using Project.Components;
using UmbrellaToolsKit.Collision;
using UmbrellaToolsKit.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Entities
{
    public class Enemy : Actor
    {
        public float Speed = 6f;
        public float JumpForce = 200f;

        public Square Square = new Square();
        public PathingComponent Pathing;

        public override void Start()
        {
            tag = "Enemy";

            Sprite = Scene.Content.Load<Texture2D>("Sprites/player");

            size = new Point(16, 16);
            Gravity2D = Vector2.UnitY * 30f;
            MaxVelocity = 200f;

            SpriteColor = Color.Red;

            AddComponent<LadderComponent>();
            AddComponent<AnimationComponent>().SetPath("Sprites/player_animation");
            AddComponent<JumpAnimation>();
            AddComponent<MovementComponent>().SetSpeed(Speed);
            AddComponent<RevertSpriteByVelocityComponent>();
            AddComponent<WalkAnimationComponent>();
            Pathing = AddComponent<PathingComponent>();

            Square.Scene = Scene;
            Square.size = new Point(4, 4);
            Square.Origin = new Vector2(2, 2);
            Square.SquareColor = Color.White;
            Square.Start();

            base.Start();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch);
            DrawSprite(spriteBatch);

            if (Pathing.Path != null)
            {
                foreach (var node in Pathing.Path)
                {
                    Square.Position = node.Position;
                    Square.DrawSprite(spriteBatch);
                }
            }

            EndDraw(spriteBatch);
        }
    }
}