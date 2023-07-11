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
            base.Start();
        }

    }
}