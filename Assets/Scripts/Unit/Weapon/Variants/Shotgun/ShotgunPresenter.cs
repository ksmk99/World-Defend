using UnityEngine;


namespace Unit
{
    public class ShotgunPresenter : AWeaponPresenter
    {
        protected ShotgunSettings settings;

        public ShotgunPresenter(IWeaponModel model) : base(model)
        {
            settings = (ShotgunSettings)model.Settings;
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
            model.ActionTimer = settings.ReloadTime;
            model.TTL = 0;

            return true;
        }

        public override void Attack(Transform transform, bool isDead, Team team)
        {
            if (isDead || IsReloading() || !SetAction(transform, team))
            {
                return;
            }

            if (model.TTL >= settings.WaveCount * settings.WaveDelay)
            {
                model.IsActing = false;
                return;
            }

            transform.LookAt(model.Target.position);
            if (model.ActionTimer >= settings.WaveDelay)
            {
                var angle = transform.rotation.eulerAngles.y - settings.Angle / 2f;
                var step = settings.Angle / settings.BulletInWave;
                for (int i = 0; i < settings.BulletInWave; i++)
                {
                    CreateBullet(transform, team, Quaternion.Euler(Vector3.up * angle));
                    model.ActionTimer = 0;
                    angle += step;

                    var signal = new SignalOnAttack();
                    //model.SignalBus.TryFire(signal);
                }
            }

            model.ActionTimer += Time.deltaTime;
            model.TTL += Time.deltaTime;
        }
    }
}
