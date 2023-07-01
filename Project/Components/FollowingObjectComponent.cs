using Microsoft.Xna.Framework;
using UmbrellaToolsKit;

namespace Project.Components
{
    public class FollowingObjectComponent : Component
    {
        private Vector2 _oldPosition;

        public GameObject Target;
        public float MaxDistance = 8f;
        public float Speed = 4f;

        public float CurrentDistance => Vector2.Distance(GameObject.Position, Target.Position);
        public bool CanFollow => CurrentDistance >= MaxDistance;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Target == null) return;
            if (!CanFollow) return;

            if (_oldPosition.Length() == 0)
                _oldPosition = Target.Position;

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            GameObject.Position = _oldPosition;

            _oldPosition = Vector2.Lerp(_oldPosition, Target.Position, delta * Speed);
        }
    }
}
