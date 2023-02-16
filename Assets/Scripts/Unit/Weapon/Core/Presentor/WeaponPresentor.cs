using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public class WeaponPresentor : IWeapon
    {
        private readonly PlayerModel playerModel;
        private readonly WeaponSettings settings;
        private readonly WeaponModel model;

        public WeaponPresentor(PlayerModel playerModel, IWeaponModel model)
        {
            this.playerModel = playerModel;
            this.model = (WeaponModel)model;
            settings = (WeaponSettings)model.Settings;
        }

        public void Update()
        {
            for (int i = 0; i < model.Bullets.Count; i++)
            {
                if (model.Bullets[i].CheckEnd())
                {
                    model.Bullets[i].Dispose();
                    model.Bullets.Remove(model.Bullets[i]);
                }
                else
                {
                    model.Bullets[i].Move();
                }
            }

            if (IsReloading())
            {
                return;
            }

            if (SetAction())
            {
                Attack();
            }
        }

        public bool IsReloading()
        {
            return !model.IsActing &&
                    model.CanUse &&
                    model.NextUseTime > Time.time;
        }

        public bool SetAction()
        {
            if (model.IsActing)
            {
                return true;
            }

            EnemyView enemy = WeaponHelper.GetNearestEnemy(playerModel.Transform.position, model.Settings);
            if (enemy == default)
            {
                return false;
            }

            model.Target = enemy.transform;
            model.IsActing = true;

            model.NextUseTime = Time.time + settings.ReloadTime;
            model.ActionTimer = settings.BulletDelay;
            model.TTL = 0;

            return true;
        }

        public void Attack()
        {
            if (model.IsActing)
            {
                playerModel.Transform.LookAt(model.Target.position);
            }

            if (model.ActionTimer >= settings.BulletDelay)
            {
                CreateBullet(playerModel.Transform);
                model.ActionTimer = 0;
            }

            if (model.TTL >= settings.BulletCount * settings.BulletDelay)
            {
                model.IsActing = false;
            }

            model.ActionTimer += Time.deltaTime;
            model.TTL += Time.deltaTime;
        }

        private void CreateBullet(Transform transform)
        {
            var bulletSettings = new BulletRuntimeSettings(
                settings.Distance, transform.rotation,
                transform.position, model.Team,
                settings.Effects);
            var bullet = model.BulletFactory.Create(bulletSettings);
            model.Bullets.Add(bullet);
        }
    }
}
