using System;
using UnityEngine;
using Zenject;

namespace Unit.Bullet
{
    public class BulletPresentor : IBulletPresentor, ITickable
    {
        private  BulletModel model;
        private  BulletView view;

        public BulletPresentor(BulletModel model, BulletView result)
        {
            this.model = model;
            this.view = result;

            view.OnCollide += Collide;
            view.OnDataUpdate += DataUpdate;
        }

        private void DataUpdate(IBulletSettings settings1, BulletRuntimeSettings settings2)
        {
            model.Init(settings1, settings2);

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
            view.transform.GetComponent<BulletView>().Dispose();
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
    }
}
