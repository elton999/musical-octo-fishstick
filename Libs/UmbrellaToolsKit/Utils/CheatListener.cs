using System;
using UmbrellaToolsKit.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace UmbrellaToolsKit.Utils
{
    public class CheatListener : GameObject
    {
        private List<Tuple<Keys, Action>> _cheatList = new List<Tuple<Keys, Action>>();

        public override void Update(GameTime gameTime)
        {
            foreach (var cheat in _cheatList)
                if (KeyBoardHandler.KeyPressed(cheat.Item1))
                    cheat.Item2?.Invoke();
        }

        public void AddCheat(Keys key, Action action = null) => _cheatList.Add(new Tuple<Keys, Action>(key, action));

        public override void OnDestroy() => _cheatList.Clear();
    }
}