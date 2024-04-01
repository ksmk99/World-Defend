using Helpers;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class LevelProgressPresenter : IInitializable, ILevelProgressPresenter
    {
        private readonly ILevelProgressModel model;
        private readonly ILevelProgressView view;

        public LevelProgressPresenter(ILevelProgressModel model, ILevelProgressView view)
        {
            this.model = model;
            this.view = view;
        }

        public async void ChangeProgress(SignalOnProgressionChange signal)
        {
            if (signal.Percent.Equals(model.Percent))
            {
                return;
            }

            if (model.CancellationTokenSource != null)
            {
                model.CancellationTokenSource.Cancel();
            }

            await ChangeViewValue(signal);
            UpdateModelValues(signal);
        }

        public void Initialize()
        {
            view.Init(model.Level);
        }

        private async Task ChangeViewValue(SignalOnProgressionChange signal)
        {
            model.CancellationTokenSource = new CancellationTokenSource();

            var t = 0f;
            while (t < model.AnimationTime || model.CancellationTokenSource.IsCancellationRequested)
            {
                t += Time.deltaTime;
                float value = Mathf.Lerp(model.Percent, signal.Percent, t);
                view.SetValue(value);

                await Task.Delay(150);
            }

            view.SetValue(signal.Percent);
        }

        private void UpdateModelValues(SignalOnProgressionChange signal)
        {
            model.KillCount = signal.KillCount;
            model.Percent = signal.Percent;
        }
    }
}
