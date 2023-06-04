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
        private JumpAnimation _jumpComponent;

        public void SetSpeed(float speed) => _speed = speed;

        public override void Start()
        {
            _actor = GameObject.GetActor();
            _jumpComponent = GameObject.GetComponent<JumpAnimation>();
        }

        public void AddDirection(Vector2 direction) => _direction = direction;

        public override void UpdateData(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            var moveValue = _direction * (_jumpComponent.IsGrounded ? _speed : _speed / 2f) * deltaTime;

            _actor.Velocity.X = moveValue.X;
            if (_actor.Gravity2D.Length() == 0)
                _actor.Velocity.Y = moveValue.Y;

            base.UpdateData(gameTime);
        }
    }
}