using System.Collections;
using System;
using Project.Entities;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;

namespace Project.Gameplay
{
    public class Door : Actor
    {
        [ShowEditor] private bool _playerIsOnDoor = false;

        public bool CanOpenDoor => !HasKeys || HasKeys && Player.CollectedKey;

        public static event Action OnEnterDoor;
        public static event Action OnEnterDoorDelay;
        public static bool HasKeys = false;

        public CoroutineManagement CoroutineManagement = new CoroutineManagement();

        public override void Start()
        {
            tag = "Door";
            HasGravity = false;
            size = new Point(16, 16);

            base.Start();
        }

        public override void OnDestroy() => HasKeys = false;

        public void CheckPlayer()
        {
            if (_playerIsOnDoor) return;

            if (!CanOpenDoor) return;

            if (!overlapCheck(Scene.Players[0].GetActor())) return;

            _playerIsOnDoor = true;
            CoroutineManagement.StarCoroutine(OpenDoorDelay());
        }

        public IEnumerator OpenDoorDelay()
        {
            OnEnterDoor?.Invoke();
            yield return CoroutineManagement.Wait(1000.0f);
            OnEnterDoorDelay?.Invoke();
            yield return null;
        }

        public override void Update(GameTime gameTime)
        {
            CoroutineManagement.Update(gameTime);
            base.Update(gameTime);
        }

        public override void UpdateData(GameTime gameTime)
        {
            CheckPlayer();
            base.UpdateData(gameTime);
        }

    }
}

