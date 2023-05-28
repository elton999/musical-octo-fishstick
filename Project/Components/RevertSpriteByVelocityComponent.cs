using System;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Components
{
    public class RevertSpriteByVelocityComponent : Component
    {
        private Actor _actor;

        public override void Start() => _actor = GameObject.GetActor();

        public override void Update(GameTime gameTime)
        {
            int spriteSizeValue = MathF.Sign(_actor.Velocity.X);
            if (spriteSizeValue != 0)
                _actor.spriteEffect = spriteSizeValue > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            base.Update(gameTime);
        }
    }
}