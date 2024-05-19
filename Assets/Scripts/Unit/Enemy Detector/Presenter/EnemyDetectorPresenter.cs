using System.Linq;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class EnemyDetectorPresenter : ITickable
    {
        private IEnemyDetectorModel model;
        private EnemyDetectorView view;

        public EnemyDetectorPresenter(IEnemyDetectorModel model, EnemyDetectorView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Tick()
        {
            var target = model.EnemySpawner.ActiveUnits
                    .Where(x => !x.GetPresenter().IsDead)
                    .OrderBy(x => Vector3.Distance(model.Transform.position, x.transform.position))
                    .FirstOrDefault();

            if (target == null)
            {
                view.Disable();
                return;
            }

            Rotate(target);
        }

        private void Rotate(UnitView target)
        {
            Vector3 direction = target.transform.position - view.transform.position;
            direction.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            var rotation = Quaternion.RotateTowards(view.transform.rotation, targetRotation, model.Data.RotationSpeed);
            view.SetRotation(rotation);
        }
    }
}
