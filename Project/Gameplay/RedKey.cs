using Project.Entities;
using Project.Components;
using UmbrellaToolsKit;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Gameplay
{
    public class RedKey : Key
    {
        public override void Start()
        {
            base.Start();
            tag = "red key";
            Sprite = Scene.Content.Load<Texture2D>("Sprites/redkey");
        }

        public override void SetLevelConfig(){}

        public override void OnCollected()
        {
            Components.Remove(CollectableComponent);
            Player.CollectedRedKey = true;
            var followingComponet = AddComponent<FollowingObjectComponent>();
            followingComponet.Target = Scene.Players[0];
            followingComponet.MaxDistance = 8f;
            followingComponet.Speed = 2f; 
        }
    }
}
