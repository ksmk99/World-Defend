using Unit.Bullet;
using UnityEngine;

namespace Unit
{ 
    public class DefaultBulletPresenter : ABulletPresenter
    {
        public DefaultBulletPresenter(BulletModel model, BulletView view) : base(model, view)
        {
        }

        public override void Collide(Collider other)
        {
            if (!model.CanCollide)
            {
                return;
            }

            var isSuccess = false;
            if (other.TryGetComponent<UnitView>(out var view))
            {
                UnitPresenter presenter = view.GetPresenter();
                isSuccess = presenter.TryApplyEffects(model.Weapon.Settings.Effects, model.Team);
            }

            if (isSuccess)
            {
                model.Weapon.CreateHit(view.transform.position);
            }

            model.CanCollide = !isSuccess;
        }
    }
}
