using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public class DefaultBulletPresenter : ABulletPresenter
    {
        public DefaultBulletPresenter(BulletModel model, BulletView view) : base(model, view)
        {
        }

        public override void Collide(GameObject target)
        {
            if (!model.CanCollide)
            {
                return;
            }

            model.CanCollide = false;
            if (target.TryGetComponent<UnitView>(out var view))
            {
                UnitPresenter presenter = view.GetPresenter();
                var isSuccess = presenter.TryApplyEffects(model.Weapon.Settings.Effects, model.Team);
                if (isSuccess)
                {
                    model.Weapon.CreateHit(view.transform.position);
                    return;
                }
            }
            else if (target.TryGetComponent<Obstacle>(out var obstacle))
            {
                model.Weapon.CreateHit(this.view.transform.position);
                return;
            }

            model.CanCollide = true;
        }
    }
}
