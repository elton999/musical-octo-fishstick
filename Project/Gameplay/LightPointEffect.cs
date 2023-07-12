using System;
using UmbrellaToolsKit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Gameplay
{
    public class LightPointEffect : GameObject
    {
        public override void Start()
        {
            Tag = "Light Point";
            size = new Point(8);
            Sprite = Scene.Content.Load<Texture2D>("Sprites/Tilemap");
            Body = new Rectangle(new Point(72, 0), size);
            Light.Points.Add(new Tuple<float, GameObject>(0.13f, this));
            base.Start();
        }
    }
}