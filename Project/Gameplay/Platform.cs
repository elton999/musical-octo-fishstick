using System;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;

namespace Project.Gameplay
{
    public class Platform : Solid
    {
        public override void Start()
        {
            base.Start();
            Position += Vector2.UnitY;
        }

        public override bool check(Point size, Vector2 position, Actor actor = null)
        {
            if (actor != null && actor.Velocity.Length() > 0 && MathF.Sign(actor.Velocity.Y) == -1)
                return false;
            return base.check(size, position + Vector2.UnitY) && !base.check(size, position);
        }
    }
}

