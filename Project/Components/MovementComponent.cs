using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;

namespace Project.Components
{
    public class MovementComponent : Component
    {
        private Actor _actor;
        private float _speed;
        private Vector2 _direction = Vector2.Zero;

        public MovementComponent(Actor actor, float speed)
        {
            _actor = actor;
            _speed = speed;
        }

        public void AddDirection(Vector2 direction) => _direction = direction;

        public override void UpdateData(GameTime gameTime)
        {
            var moveValue = _direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _actor.Velocity.X = moveValue.X;

            base.UpdateData(gameTime);
        }
    }
}