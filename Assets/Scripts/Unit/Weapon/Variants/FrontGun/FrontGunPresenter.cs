using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Barracuda;
using UnityEngine;

namespace Unit
{
    public class FrontGunPresenter : AWeaponPresenter
    {
        protected FrontGunSettings settings;

        public FrontGunPresenter(IWeaponModel model) : base(model)
        {
            settings = (FrontGunSettings)model.Settings;
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
            model.ActionTimer = settings.BulletDelay;
            model.TTL = 0;

            return true;
        }

        public override void Attack(Transform transform, bool isDead, Team team)
        {
            if (isDead || IsReloading() || !SetAction(transform, team))
            {
                return;
            }

            if (model.TTL >= settings.BulletCount * settings.BulletDelay)
            {
                model.IsActing = false;
                return;
            }

            transform.LookAt(model.Target.position);
            if (model.ActionTimer >= settings.BulletDelay)
            {
                CreateBullet(transform, team, transform.rotation);
                model.ActionTimer = 0;

                var signal = new SignalOnAttack();
                model.SignalBus.TryFire(signal);
            }

            model.ActionTimer += Time.deltaTime;
            model.TTL += Time.deltaTime;
        }
    }
}
