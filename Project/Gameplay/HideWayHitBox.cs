using System;
using Microsoft.Xna.Framework;
using System.Collections;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;

namespace Project.Gameplay
{
    public class HideWayHitBox : Actor
    {
        [ShowEditor] private bool _isShowTheWay = false;
        [ShowEditor] private float _speed = 5.0f;
        private CoroutineManagement _coroutineManagement = new();

        public override void Update(GameTime gameTime)
        {
            _coroutineManagement.Update(gameTime);
            base.Update(gameTime);
        }

        public IEnumerator ShowTheWay()
        {
            while (Scene.Backgrounds[1].Transparent > 0.0f)
            {
                float transparent = Scene.Backgrounds[0].Transparent;
                transparent -= _speed * (float)_coroutineManagement.GameTime.ElapsedGameTime.TotalSeconds;
                Scene.Backgrounds[0].Transparent = MathF.Max(transparent, 0.0f);
                yield return default;
            }

            yield return null;
        }

        public override void UpdateData(GameTime gameTime)
        {
            if (Scene.Players[0].GetActor().overlapCheck(this) && !_isShowTheWay)
            {
                _isShowTheWay = true;
                _coroutineManagement.StarCoroutine(ShowTheWay());
            }
            base.UpdateData(gameTime);
        }
    }
}