using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;

namespace Project.Components
{
    public class ActorToSolidWhenFailComponent : Component
    {
        private Solid _solid;
        private Actor _actor;
        private JumpAnimation _jumpAnimation;
        private LadderComponent _ladderComponent;
        private Vector2 _lastPositionOnGround;

        public override void Start()
        {
            _actor = GameObject.GetActor();
            _jumpAnimation = GameObject.GetComponent<JumpAnimation>();
            _ladderComponent = GameObject.GetComponent<LadderComponent>();
            _lastPositionOnGround = GameObject.Position;
        }

        public override void UpdateData(GameTime gameTime)
        {
            if (_jumpAnimation.IsGrounded) _lastPositionOnGround = GameObject.Position;

            if (_solid == null && !_jumpAnimation.IsGrounded && !_ladderComponent.CanClimbLadder)
            {
                _solid = new Solid();
                _solid.size = _actor.size;
                _solid.Position = _actor.Position;
                _solid.Position.Y = _lastPositionOnGround.Y + _actor.size.Y;
                _actor.Position.Y = _solid.Position.Y;
                GameObject.Scene.AddGameObject(_solid);
            }
            base.UpdateData(gameTime);
        }
    }
}