using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Project.Components;

namespace Project.Gameplay
{
    public class Ladder : Actor
    {
        private UmbrellaToolsKit.Sprite.Square _square;

        public override void Start()
        {
            base.Start();
            Gravity2D = Vector2.Zero;

            _square = new UmbrellaToolsKit.Sprite.Square();
            _square.SquareColor = Color.Green;
            _square.size = size;
            _square.Position = Position;
            Scene.AddGameObject(_square);

            var platform = new Gameplay.Platform();
            platform.size = new Point(size.X, 1);
            platform.Position = Position;

            Scene.AddGameObject(platform);
        }

        public override void UpdateData(GameTime gameTime)
        {
            base.UpdateData(gameTime);

            foreach (var actor in Scene.AllActors)
            {
                if (actor.tag != tag)
                {
                    var ladderComponent = actor.GetComponent<LadderComponent>();
                    if (ladderComponent != null) ladderComponent.IsInTheLadder = overlapCheck(actor);
                }
            }
        }
    }
}
