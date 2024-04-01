using Helpers;
using System;
using Unit;
using UnityEngine;
using Zenject;

namespace UI
{
    public class EnemyCounterPresenter : IInitializable, IRoomResettable
    {
        private EnemyCounterModel model;
        private EnemyCounterView view;

        public EnemyCounterPresenter(EnemyCounterModel model, EnemyCounterView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Initialize()
        {
            model.Count = model.SpawnerSettings.Count;
            view.SetValue(model.Count);
        }

        public void Death(SignalOnEnemyDeath signal)
        {
            model.Count--;
            model.Count = Mathf.Clamp(model.Count, 0, model.SpawnerSettings.Count);
            view.SetValue(model.Count);
        }

        public void Reset(SignalOnRoomReset signal)
        {
            Initialize();
        }
    }
}
