using System;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;

namespace Project.Gameplay
{
    public class Door : Actor
    {
        [ShowEditor] private bool _playerIsOnDoor = false;
        private Square _square;

        public static event Action OnEnterDoor;

        public override void Start()
        {
            tag = "Door";
            HasGravity = false;
            size = new Point(16, 16);

            _square = new Square();
            _square.Position = Position;
            _square.size = size;
            _square.SquareColor = Color.Purple;

            Scene.AddGameObject(_square);
            base.Start();
        }

        public void CheckPlayer()
        {
            if (_playerIsOnDoor) return;

            if (!overlapCheck(Scene.Players[0].GetActor())) return;

            _playerIsOnDoor = true;
            OnEnterDoor?.Invoke();
        }

        public override void UpdateData(GameTime gameTime)
        {

            CheckPlayer();
            base.UpdateData(gameTime);
        }


    }
}

