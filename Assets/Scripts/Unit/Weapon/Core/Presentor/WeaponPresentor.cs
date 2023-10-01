using System;
using System.Collections.Generic;
using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public class WeaponPresenter : IWeaponPresentor
    {
        private readonly WeaponSettings settings;
        private readonly WeaponModel model;
        private List<BulletView> bullets = new List<BulletView>();

        public WeaponPresenter(IWeaponModel model)
        {
            this.model = (WeaponModel)model;
            settings = (WeaponSettings)model.Settings;
        }

        public void Update(Transform transform, bool isDead, Team team)
        {
            Attack(transform, isDead, team);
            BulletsUpdate();
        }

        public bool IsReloading()
        {
            return !model.IsActing &&
                    model.CanUse &&
                    model.NextUseTime > Time.time;
        }

        public bool SetAction(Transform transform, Team team)
        {
            if (model.IsActing)
            {
                return true;
            }

            UnitView enemy = WeaponHelper.GetNearestEnemy(transform.position, model.Settings, team);
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

        public void Attack(Transform transform, bool isDead, Team team)
        {
            if (isDead || IsReloading() || !SetAction(transform, team))
            {
                return;
            }

            transform.LookAt(model.Target.position);
            if (model.ActionTimer >= settings.BulletDelay)
            {
                CreateBullet(transform, team);
                model.ActionTimer = 0;
            }

            if (model.TTL >= settings.BulletCount * settings.BulletDelay)
            {
                model.IsActing = false;
            }

            model.ActionTimer += Time.deltaTime;
            model.TTL += Time.deltaTime;
        }

        private void CreateBullet(Transform transform, Team team)
        {
            var bulletSettings = new BulletRuntimeSettings(
                settings.Distance, transform.rotation,
                transform.position, team,
                settings.Effects);

            var bullet = model.BulletPool.Create(bulletSettings);
            bullet.OnDispose += DisposeBullet;

            bullets.Add(bullet);
        }

        private void DisposeBullet(BulletView view)
        {
            bullets.Remove(view);
        }

        public void Reset()
        {
            model.ActionTimer = 0;
            model.TTL = 0;
            model.IsActing = false;
        }
    }
}
