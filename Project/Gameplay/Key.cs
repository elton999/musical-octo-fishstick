using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Components;

namespace Project.Gameplay
{
    public class Key : Actor
    {
        public override void Start()
        {
            HasGravity = false;
            Sprite = Scene.Content.Load<Texture2D>("Sprites/key");
            size = new Point(8);

            AddComponent<FloatingComponent>();

            base.Start();
        }
    }
}