using System;
using UnityEngine;

namespace Unit
{
    public class RoundAttackPresenter : AWeaponPresenter
    {
        protected RoundAttackSettings settings;

        public RoundAttackPresenter(IWeaponModel model) : base(model)
        {
            settings = (RoundAttackSettings)model.Settings;
        }

        public override bool SetAction(Transform transform, Team team)
        {
            if (model.IsActing)
            {
                return true;
            }

            UnitView enemy = WeaponHelper.GetNearestEnemy(transform.position, model.Settings, team);
            if (enemy == default)
            {
                model.Target = null;
                return false;
            }

            model.Target = enemy.transform;
            model.IsActing = true;
            model.Team = team;

            model.NextUseTime = Time.time + settings.ReloadTime;
            model.ActionTimer = 0;
            model.TTL = 0;

            return true;
        }

        public override void Attack(Transform transform, bool isDead, Team team)
        {
            if (isDead || IsReloading() || !SetAction(transform, team))
            {
                return;
            }

            if (model.TTL > 0)
            {
                model.IsActing = false;
                return;
            }

            transform.LookAt(model.Target.position);
            RoundAttack(transform, team);
            model.ActionTimer = 0;

            //var signal = new SignalOnAttack();
            ////model.SignalBus.TryFire(signal);

            model.ActionTimer += Time.deltaTime;
            model.TTL += Time.deltaTime;
        }

        private void RoundAttack(Transform transform, Team team)
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, settings.Distance, settings.TargetLayer);
            foreach (var target in targets)
            {
                if (target.gameObject.TryGetComponent<UnitView>(out var unit))
                {
                    var presenter = unit.GetPresenter();
                    if (!presenter.Team.Equals(team))
                    {
                        presenter.TryApplyEffects(settings.Effects, team);
                    }
                }
            }

            CreateHit(transform.position);
        }
    }
}
