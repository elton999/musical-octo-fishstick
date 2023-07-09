using UmbrellaToolsKit;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project.Gameplay
{
    public class Light : GameObject
    {
        public override void Start()
        {
            Sprite = Scene.Content.Load<Texture2D>("Sprites/light_sprite");
            Effect = Scene.Content.Load<Effect>("Shaders/Light");
            Scale = 0.3f;
            Origin = ((new Vector2(Sprite.Width, Sprite.Height)) * 0.5f + Vector2.One * 8.0f);

            Tag = "Light";
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            Position = Scene.AllActors[0].Position;
            base.Update(gameTime);
        }
    }
}