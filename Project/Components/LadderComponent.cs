using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;

namespace Project.Components
{
    public class LadderComponent : Component
    {
        private Actor _actor;
        private Vector2 _gravityBackup;
        public bool IsInTheLadder = false;
        public bool CanClimbLadder = false;

        public override void Start()
        {
            _actor = GameObject.GetActor();
            _gravityBackup = _actor.Gravity2D;
        }

        public override void UpdateData(GameTime gameTime)
        {
            _actor.Gravity2D = IsInTheLadder ? Vector2.Zero : _gravityBackup;
            base.UpdateData(gameTime);
        }
    }
}