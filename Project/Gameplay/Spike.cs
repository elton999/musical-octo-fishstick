using Project.Components;
using UmbrellaToolsKit.Collision;
using UmbrellaToolsKit.Sprite;
using Microsoft.Xna.Framework;

namespace Project.Gameplay
{
    public class Spike : Actor
    {
        public override void Start()
        {
            Tag = "Spike";
            HasGravity = false;

            AddComponent<DamagerComponent>().InstantaneityDeath = true;

            var spikeRender = new Square();
            spikeRender.SquareColor = Color.Red;
            spikeRender.size = size;
            spikeRender.Position = Position;

            Scene.AddGameObject(spikeRender);
            base.Start();
        }
    }
}
