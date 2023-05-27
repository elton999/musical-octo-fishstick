using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Interfaces;

namespace Project.Components
{
    public class MovementComponent : Component
    {
        private IGameObject _gameObject;
        private float _speed;
        private Vector2 _direction = Vector2.Zero;

        public MovementComponent(GameObject gameObject, float speed)
        {
            _gameObject = gameObject;
            _speed = speed;
        }

        public void AddDirection(Vector2 direction) => _direction = direction;

        public override void Update(GameTime gameTime)
        {
            _gameObject.Position = _gameObject.Position + _direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }
    }
}

