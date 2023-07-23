using System.Collections;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;

namespace Project.Components
{
    public class SqueezeSpriteOnJumpComponent : Component
    {
        private IScalableVector _scaler;
        private JumpAnimation _jumpAnimation;
        private Actor _actor;
        private bool _lastFrameGround;
        public CoroutineManagement _coroutineManagement = new CoroutineManagement();

        public override void Start()
        {
            _jumpAnimation = GameObject.GetComponent<JumpAnimation>();
            _actor = GameObject.GetActor();

            _lastFrameGround = _jumpAnimation.IsGrounded;
        }

        public override void Update(GameTime gameTime)
        {
            _coroutineManagement.Update(gameTime);
            base.Update(gameTime);
        }

        public override void UpdateData(GameTime gameTime)
        {
            bool isStartingJump = _lastFrameGround && !_jumpAnimation.IsGrounded;

            if (isStartingJump && _actor.Velocity.Y < 0.0f) OnSqueeze();

            _lastFrameGround = _jumpAnimation.IsGrounded;
            base.UpdateData(gameTime);
        }

        public void SetScaler(IScalableVector scaler) => _scaler = scaler;

        public void OnSqueeze() => _coroutineManagement.StarCoroutine(Squeeze());

        public IEnumerator Squeeze()
        {
            _scaler.ScaleVector = new Vector2(0.8f, 1.1f);
            _scaler.OffsetVector = new Vector2(-2f, 2f);
            yield return _coroutineManagement.Wait(200f);

            _scaler.ScaleVector = Vector2.One;
            _scaler.OffsetVector = Vector2.Zero;
            yield return null;
        }
    }
}
