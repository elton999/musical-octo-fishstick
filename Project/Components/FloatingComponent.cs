using System;
using UmbrellaToolsKit;
using Microsoft.Xna.Framework;
using UmbrellaToolsKit.Collision;

namespace Project.Components
{
    public class FloatingComponent : Component
    {
        private Actor _actor;
        private float _speed = 2f;
        private float _distance = 5f;

        public override void Update(GameTime gameTime)
        {
            float timer = (float)gameTime.TotalGameTime.TotalSeconds;
            GameObject.Origin.Y = MathF.Cos(timer * _speed) * _distance;

            base.Update(gameTime);
        }

    }
}

