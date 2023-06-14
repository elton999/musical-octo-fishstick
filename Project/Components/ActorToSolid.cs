using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;

namespace Project.Components
{
    public class ActorToSolid : Component
    {
        private Solid _solid;
        private Actor _actor;
        private JumpAnimation _jumpAnimation;
        private LadderComponent _ladderComponent;

        public override void Start()
        {
            _actor = GameObject.GetActor();
            _jumpAnimation = GameObject.GetComponent<JumpAnimation>();
            _ladderComponent = GameObject.GetComponent<LadderComponent>();
        }

        public override void UpdateData(GameTime gameTime)
        {
            if (_solid == null && !_jumpAnimation.IsGrounded && !_ladderComponent.CanClimbLadder)
            {
                _solid = new Solid();
                _solid.size = _actor.size;
                _solid.Position = _actor.Position;
                GameObject.Scene.AddGameObject(_solid);
            }
            base.UpdateData(gameTime);
        }
    }
}