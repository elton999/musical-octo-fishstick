using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;

namespace Project.Components
{
    public class JumpAnimation : Component
    {
        private Actor _actor;
        private AnimationComponent _animation;
        private LadderComponent _ladderComponent;

        private string _jumpAnimation = "jump";

        [ShowEditor] public bool IsGrounded = false;
        [ShowEditor] public Vector2 GroundBuffer = Vector2.UnitY;

        public override void Start()
        {
            _animation = GameObject.GetComponent<AnimationComponent>();
            _ladderComponent = GameObject.GetComponent<LadderComponent>();
            _actor = GameObject.GetActor();
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsGrounded && !_ladderComponent.IsInTheLadder)
                _animation.SetAnimation(_jumpAnimation);

            base.Update(gameTime);
        }

        public override void UpdateData(GameTime gameTime)
        {
            CheckGround();
            base.UpdateData(gameTime);
        }

        public void CheckGround()
        {
            IsGrounded = false;

            foreach (Solid solid in _actor.Scene.AllSolids)
                if (solid.check(_actor.size, _actor.Position + GroundBuffer)) IsGrounded = true;

            if (_actor.Scene.Grid == null) return;

            if (_actor.Scene.Grid.checkOverlap(_actor.size, _actor.Position + GroundBuffer, _actor))
                IsGrounded = true;
        }
    }
}