using System;
using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;

namespace Project.Components
{
    public class CollectableComponent : Component
    {
        private string _tagName = "Player";
        private Actor _actor;
        private bool _itemCollected = false;

        public Action OnCollectItem;

        public override void Start() => _actor = GameObject.GetActor();

        public override void UpdateData(GameTime gameTime)
        {
            if (!_itemCollected)
                foreach (var actor in GameObject.Scene.AllActors)
                    if (actor.tag == _tagName && actor.overlapCheck(_actor))
                        Collect();

            base.UpdateData(gameTime);
        }

        public void Collect()
        {
            _itemCollected = true;
            OnCollectItem?.Invoke();
        }
    }
}