using System;
using System.Collections;
using Project.Entities;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Gameplay
{
    public class Door : Actor
    {
        [ShowEditor] protected bool _playerIsOnDoor = false;
        [ShowEditor] protected virtual Point _closedDoorSprite => new Point(0, 64);
        [ShowEditor] protected virtual Point _openedDoorSprite => new Point(17, 64);

        public virtual bool CanOpenDoor => !HasKeys || HasKeys && Player.CollectedKey;
        public virtual bool CanShowOpenedDoor => !HasKeys || HasKeys && Player.CollectedKey && _playerIsOnDoor;

        public static event Action OnEnterDoor;
        public static event Action OnEnterDoorDelay;
        public static bool HasKeys = false;

        public CoroutineManagement CoroutineManagement = new CoroutineManagement();

        public override void Start()
        {
            tag = "Door";
            HasGravity = false;
            size = new Point(16, 17);
            Sprite = Scene.Content.Load<Texture2D>("Sprites/Tilemap");
            base.Start();
        }

        public override void OnDestroy() => HasKeys = false;

        public void CheckPlayer()
        {
            if (_playerIsOnDoor || !CanOpenDoor) return;

            if (!overlapCheck(Scene.Players[0].GetActor())) return;

            _playerIsOnDoor = true;
            CoroutineManagement.StarCoroutine(OpenDoorDelay());
        }

        public virtual IEnumerator OpenDoorDelay()
        {
            OnEnter();
            yield return CoroutineManagement.Wait(1000.0f);
            OnEnterDoorDelay?.Invoke();
            yield return null;
        }

        public void SetDoorSprite()
        {
            Body = new Rectangle(_closedDoorSprite, size);
            if (!CanShowOpenedDoor) return;
            Body = new Rectangle(_openedDoorSprite, size);
        }

        public override void Update(GameTime gameTime)
        {
            CoroutineManagement.Update(gameTime);
            SetDoorSprite();
            base.Update(gameTime);
        }

        public override void UpdateData(GameTime gameTime)
        {
            CheckPlayer();
            base.UpdateData(gameTime);
        }

        public static void OnEnter() => OnEnterDoor?.Invoke();
    }
}

