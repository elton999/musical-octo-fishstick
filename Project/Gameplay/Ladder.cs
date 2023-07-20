using System;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Project.Components;

namespace Project.Gameplay
{
    public class Ladder : Actor
    {

        public override void Start()
        {
            base.Start();
            Gravity2D = Vector2.Zero;

            var platform = new Gameplay.LadderPlatform();
            platform.size = new Point(size.X, 1);
            platform.Position = Position;

            Scene.AddGameObject(platform);
        }

        public override void UpdateData(GameTime gameTime)
        {
            base.UpdateData(gameTime);

            foreach (var actor in Scene.AllActors)
            {
                if (actor.tag == tag) continue;

                var ladderComponent = actor.GetComponent<LadderComponent>();
                var movementComponent = actor.GetComponent<MovementComponent>();

                if (movementComponent != null && ladderComponent != null && movementComponent.Direction.Length() > 0)
                {
                    ladderComponent.IsInTheLadder = overlapCheck(actor);
                    ladderComponent.CanClimbLadder = MathF.Sign(movementComponent.Direction.Y) != 0 &&
                    overlapCheck(actor.size, actor.Position + Vector2.UnitY) && !overlapCheck(actor.size, actor.Position);

                    ladderComponent.CanClimbLadder = ladderComponent.CanClimbLadder || overlapCheck(actor.size, actor.Position);
                }

            }
        }
    }
}
