using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;

namespace Project.Components
{
    public class DamagerComponent : Component
    {
        private Actor _actor;
        private float _damageValue => InstantaneityDeath ? float.PositiveInfinity : DamageValue;

        public bool InstantaneityDeath = false;
        public float DamageValue = 1.0f;

        public override void Start() => _actor = GameObject.GetActor();

        public override void UpdateData(GameTime gameTime)
        {
            foreach (var actor in GameObject.Scene.AllActors)
                if (actor != _actor && _actor.overlapCheck(actor))
                    SetDamage(actor);

            base.UpdateData(gameTime);
        }

        public void SetDamage(Actor actor)
        {
            var health = actor.GetComponent<HealthComponent>();
            health.TakeDamage(_damageValue);
        }
    }
}