using UmbrellaToolsKit;
using Microsoft.Xna.Framework;

namespace Project.Gameplay
{
    public class LightPointEffect : GameObject
    {
        public override void Start()
        {
            Tag = "Light Point";
            size = new Point(8);
            Light.Points.Add(new Light.LightPoint(this, 0.1f));
            base.Start();
        }

    }
}