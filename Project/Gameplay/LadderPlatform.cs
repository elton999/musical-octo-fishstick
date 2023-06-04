using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Project.Components;

namespace Project.Gameplay
{
    public class LadderPlatform : Platform
    {
        public override bool check(Point size, Vector2 position, Actor actor = null)
        {
            if (actor != null)
            {
                var ladderComponent = actor.GetComponent<LadderComponent>();
                if (ladderComponent != null && ladderComponent.CanClimbLadder) return false;
            }
            return base.check(size, position, actor);
        }
    }
}