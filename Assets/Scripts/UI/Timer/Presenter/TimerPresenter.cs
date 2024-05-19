using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit;
using UnityEngine;
using Zenject;

namespace UI
{
    public class TimerPresenter : ITickable, IInitializable, IRoomResettable
    {
        private TimerView view;
        private TimerModel model;

        public TimerPresenter(TimerView view, TimerModel model)
        {
            this.view = view;
            this.model = model;
        }

        public void Initialize()
        {
            model.StartTime = Time.time;
            model.EndTime = Time.time + model.LevelSettingsData.Duration;
        }

        public void Reset(SignalOnRoomReset signal)
        {
            Initialize();
        }

        public void Tick()
        {
            var value = 1 - (Time.time - model.StartTime) / model.LevelSettingsData.Duration;
            value = Mathf.Clamp01(value);

            view.SetValue(value);
        }
    }
}
