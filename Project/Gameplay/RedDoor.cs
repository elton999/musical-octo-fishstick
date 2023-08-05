using System;
using System.Collections;
using UmbrellaToolsKit;
using Project.Entities;
using Microsoft.Xna.Framework;

namespace Project.Gameplay
{
    public class RedDoor : Door
    {
        protected override Point _closedDoorSprite => new Point(40, 64);
        protected override Point _openedDoorSprite => new Point(57, 64);
        private bool _needKey;

        public override bool CanOpenDoor => !_needKey || Player.CollectedRedKey;
        public override bool CanShowOpenedDoor => !_needKey || _needKey && Player.CollectedRedKey && _playerIsOnDoor;

        public static event Action<string> OnEnterOnRedDoor;

        public override void Start()
        {
            _needKey = bool.Parse(Values["needKey"]);
            base.Start();
        }

        public override IEnumerator OpenDoorDelay()
        {
            OnEnter();
            yield return CoroutineManagement.Wait(1000.0f);
            Player.CollectedRedKey = false;
            OnEnterOnRedDoor?.Invoke((string)Values["scene"]);
            yield return null;
        }

    }
}
