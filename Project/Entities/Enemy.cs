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

            AddComponent<DamagerComponent>();
            AddComponent<LadderComponent>();
            AddComponent<AnimationComponent>().SetPath("Sprites/player_animation");
            AddComponent<JumpAnimation>().GroundBuffer = Vector2.UnitY * 4f;
            AddComponent<MovementComponent>().SetSpeed(Speed);
            AddComponent<RevertSpriteByVelocityComponent>();
            AddComponent<WalkAnimationComponent>();
            AddComponent<ActorToSolidWhenFailComponent>();
            Pathing = AddComponent<PathingComponent>();

            Square.Scene = Scene;
            Square.size = new Point(4, 4);
            Square.Origin = new Vector2(2, 2);
            Square.SquareColor = Color.White;
            Square.Start();

            ActorToSolidWhenFailComponent.OnAnySolidIsCreated += OnCreateSolidBlock;

            base.Start();
        }

        public void OnCreateSolidBlock(Solid solid, Actor actor)
        {
            if (actor != this) return;

            var animationComponent = solid.AddComponent<AnimationComponent>();
            animationComponent.SetPath("Sprites/player_animation");
            animationComponent.SetAnimation("fall");

            solid.Body = new Rectangle(Point.Zero, size);

            Destroy();
        }

        public override void OnDestroy() => ActorToSolidWhenFailComponent.OnAnySolidIsCreated -= OnCreateSolidBlock;

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