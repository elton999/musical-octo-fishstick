using System;
using UmbrellaToolsKit;

namespace Project.Components
{
    public class HealthComponent : Component
    {
        public float HP = 10.0f;
        public bool IsAlive => HP > 0.0f;
        public static event Action<GameObject> OnAnyEntityDie;
        public event Action OnDie;

        public void TakeDamage(float damage)
        {
            if (!IsAlive) return;

            HP = Math.Clamp(HP - damage, 0, float.PositiveInfinity);

            if (HP > 0.0f) return;

            OnAnyEntityDie?.Invoke(GameObject);
            OnDie?.Invoke();
        }
    }
}

