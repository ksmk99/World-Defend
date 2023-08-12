using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public class WeaponPresentor : IWeaponPresentor
    {
        private readonly WeaponSettings settings;
        private readonly WeaponModel model;

        public WeaponPresentor(IWeaponModel model)
        {
            this.model = (WeaponModel)model;
            settings = (WeaponSettings)model.Settings;
        }

        public void Update(Transform transform, bool isDead, Team team)
        {
            //BulletsUpdate();
            Attack(transform, isDead, team);
        }

        private void BulletsUpdate()
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
            var bullet = model.BulletFactory.Create(bulletSettings);
            model.Bullets.Add(bullet);
        }

        public void Reset()
        {
            model.ActionTimer = 0;
            model.TTL = 0;
            model.IsActing = false;
        }
    }
}
