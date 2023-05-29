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

        public void SetSpeed(float speed) => _speed = speed;

        public override void Start() => _actor = GameObject.GetActor();

        public void AddDirection(Vector2 direction) => _direction = direction;

        public override void UpdateData(GameTime gameTime)
        {
            var moveValue = _direction * _speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            _actor.Velocity.X = moveValue.X;

            base.UpdateData(gameTime);
        }
    }
}