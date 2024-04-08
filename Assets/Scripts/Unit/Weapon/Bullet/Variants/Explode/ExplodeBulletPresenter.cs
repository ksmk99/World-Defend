using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public class ExplodeBulletPresenter : ABulletPresenter
    {
        protected ExplodeBulletSetting setting;

        public ExplodeBulletPresenter(BulletModel model, BulletView view) : base(model, view)
        {
            setting = (ExplodeBulletSetting)model.Settings;
        }

        public override void Collide(Collider other)
        {
            if (!model.CanCollide)
            {
                return;
            }

            if (other.TryGetComponent<UnitView>(out var unitView))
            {
                UnitPresenter presenter = unitView.GetPresenter();
                if (presenter.Team == model.Team)
                {
                    return;
                }
            }
            else
            {
                return;
            }

            var colliders = Physics.OverlapSphere(view.transform.position, setting.Radius, model.Weapon.Settings.TargetLayer);
            foreach (var collider in colliders)
            {
                var presenter = collider.GetComponent<UnitView>().GetPresenter();
                presenter.TryApplyEffects(model.Weapon.Settings.Effects, model.Team);
            }

            var position = view.transform.position;
            position.y = 0;
            model.Weapon.CreateHit(position);
            model.CanCollide = false;
        }
    }
}
