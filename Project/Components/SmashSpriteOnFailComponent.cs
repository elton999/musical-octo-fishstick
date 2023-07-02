using System.Collections;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;

namespace Project.Components
{
    public interface IScalableVector
    {
        Vector2 ScaleVector { get; set; }
        Vector2 OffsetVector { get; set; }
    }

    public class SmashSpriteOnFailComponent : Component
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
            bool isFalling = _actor.Velocity.Y > 0;
            bool onTouchGround = !_lastFrameGround && _jumpAnimation.IsGrounded;

            if (onTouchGround && isFalling) OnSmash();

            _lastFrameGround = _jumpAnimation.IsGrounded;
            base.UpdateData(gameTime);
        }

        public void SetScaler(IScalableVector scaler) => _scaler = scaler;

        public void OnSmash() => _coroutineManagement.StarCoroutine(Smash());

        public IEnumerator Smash()
        {
            _scaler.ScaleVector = new Vector2(1.2f, 0.8f);
            _scaler.OffsetVector = new Vector2(2f, -4f);
            yield return _coroutineManagement.Wait(100f);

            _scaler.ScaleVector = Vector2.One;
            _scaler.OffsetVector = Vector2.Zero;
            yield return null;
        }

    }
}