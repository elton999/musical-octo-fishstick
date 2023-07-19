using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using UmbrellaToolsKit.ParticlesSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Components
{
    public class ImitParticlesOnGround : Component
    {
        private JumpAnimation _jumpAnimation;
        private Actor _actor;
        private bool _lastFrameGround;
        private ParticlesSystem _particlesSystem;

        public override void Start()
        {
            _jumpAnimation = GameObject.GetComponent<JumpAnimation>();
            _actor = GameObject.GetActor();

            _lastFrameGround = _jumpAnimation.IsGrounded;
        }

        public override void UpdateData(GameTime gameTime)
        {
            bool isFalling = _actor.Velocity.Y > 0;
            bool onTouchGround = !_lastFrameGround && _jumpAnimation.IsGrounded;

            if (onTouchGround && isFalling) ShowParticles();

            _lastFrameGround = _jumpAnimation.IsGrounded;
            base.UpdateData(gameTime);
        }

        public void ShowParticles()
        {
            if (_particlesSystem == null)
            {
                _particlesSystem = new ParticlesSystem();
                _particlesSystem.EmitsFor = ParticlesSystem.TypeEmitter.FOR_TIME;
                _particlesSystem.ParticleVelocityAngle = -227.0f;
                _particlesSystem.ParticleAngleRotation = 180.0f;
                _particlesSystem.EmitterTime = 20.0f;
                _particlesSystem.ParticleRadiusSpawn = 2f;
                _particlesSystem.ParticleTransparent = 1f;
                _particlesSystem.MaxParticles = 20;
                _particlesSystem.ParticleMaxScale = 2f;
                _particlesSystem.ParticleVelocity = 5f;
                var sprite = new Texture2D(GameObject.Scene.ScreenGraphicsDevice, 1, 1);
                sprite.SetData(new Color[1] { Color.White });
                _particlesSystem.Sprites.Add(sprite);
                _particlesSystem.Tag = "Particles";
                GameObject.Scene.AddGameObject(_particlesSystem, Layers.FOREGROUND);
            }

            _particlesSystem.Position = GameObject.Position + new Vector2(GameObject.size.X / 2f, GameObject.size.Y);
            _particlesSystem.Restart();
        }
    }
}

