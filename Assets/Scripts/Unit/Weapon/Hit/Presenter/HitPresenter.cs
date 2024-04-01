using UnityEngine;
using Zenject;

namespace Unit
{
    public class HitPresenter : ITickable, IInitializable
    {
        private HitModel model;
        private HitView view;

        public HitPresenter(HitModel model, HitView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Initialize()
        {
            view.OnReinitialize += Reinitialize;
        }

        private void Reinitialize(HitRuntimeSettings settings)
        {
            model.Init(settings);

            model.EndOfLifeTime = Time.time + model.Settings.LifeTime;

            view.transform.position = model.RuntimeSettings.Position;
            view.transform.rotation = model.RuntimeSettings.Rotation;
        }

        public void Tick()
        {
            if (model.EndOfLifeTime < Time.time)
            {
                Dispose();
            }
        }

        private void Dispose()
        {
            view.Dispose();
        }
    }
}
