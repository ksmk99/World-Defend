using System;
using UnityEngine;
using Zenject;

namespace Unit.Bullet
{
    public class BulletPresenter : ITickable, IInitializable, IBulletPresenter
    {
        private BulletModel model;
        private BulletView view;

        public BulletPresenter(BulletModel model, BulletView result)
        {
            this.model = model;
            this.view = result;
        }

        private void Reinitialize(BulletRuntimeSettings settings2)
        {
            model.Init(settings2);

            view.transform.position = model.RuntimeSettings.Position;
            view.transform.rotation = model.RuntimeSettings.Rotation;
        }

        public void Collide(Collider other)
        {
            if (other.TryGetComponent<UnitView>(out var view))
            {
                var isSuccess = view.TryAddEffects(model.RuntimeSettings.Effects, model.RuntimeSettings.Team);
                model.CanCollide = !isSuccess;
            }
        }

        public void Tick()
        {
            if (CheckEnd())
            {
                Dispose();
            }
            else
            {
                Move();
            }
        }

        public void Move()
        {
            view.transform.position += view.transform.forward * Time.deltaTime * model.Settings.Speed;
        }

        private void Dispose()
        {
            view.Dispose();
        }

        public bool CheckEnd()
        {
            if (!model.CanCollide)
            {
                return true;
            }

            var distance = Vector3.Distance(model.RuntimeSettings.Position, view.transform.position);
            return distance > model.RuntimeSettings.Distance;
        }

        public void Initialize()
        {
            view.OnCollide += Collide;
            view.OnReinitialize += Reinitialize;
        }
    }
}
