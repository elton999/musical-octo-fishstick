using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Components;
using Project.Entities;

namespace Project.Gameplay
{
    public class Key : Actor
    {
        public CollectableComponent CollectableComponent;

        public override void Start()
        {
            Tag = "Key";

            HasGravity = false;
            Sprite = Scene.Content.Load<Texture2D>("Sprites/key");
            size = new Point(8);

            Door.HasKeys = true;

            AddComponent<FloatingComponent>();
            CollectableComponent = AddComponent<CollectableComponent>();

            CollectableComponent.OnCollectItem += OnCollected;

            base.Start();
        }

        public override void OnDestroy() => CollectableComponent.OnCollectItem -= OnCollected;

        public void OnCollected()
        {
            Components.Remove(CollectableComponent);
            Player.CollectedKey = true;
            AddComponent<FollowingObjectComponent>().Target = Scene.Players[0];
        }
    }
}