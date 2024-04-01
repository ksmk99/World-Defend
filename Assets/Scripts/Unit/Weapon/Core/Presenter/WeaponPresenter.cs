using Unit.Bullet;
using Unity.Barracuda;
using UnityEngine;

namespace Unit
{
    public class SignalOnAttack
    {

    }

    public class WeaponPresenter : IWeaponPresenter
    {
        private readonly WeaponSettings settings;
        private readonly WeaponModel model;

        public IWeaponSettings Settings => settings;

        public WeaponPresenter(IWeaponModel model)
        {
            this.model = (WeaponModel)model;
            settings = (WeaponSettings)model.Settings;
        }

        public Transform GetTarget()
        {
            return model.Target;
        }

        public void Update(Transform transform, bool isDead, Team team)
        {
            Attack(transform, isDead, team);
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

        private int count = 1;

        public void Attack(Transform transform, bool isDead, Team team)
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

            //transform.LookAt(model.Target.position);
            if (model.ActionTimer >= settings.BulletDelay)
            {
                CreateBullet(transform, team);
                model.ActionTimer = 0;

                var signal = new SignalOnAttack();
                model.SignalBus.TryFire(signal);
                count++;
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

            BulletView bullet = model.BulletPool.Create(bulletSettings);
            bullet.transform.SetParent(model.Parent.transform);
            bullet.transform.position += Vector3.up;
            bullet.name = "Bullet " + count;

            BulletPresenter presenter = bullet.GetPresenter();
            presenter.SetWeapon(this);
            bullet.OnDispose += DisposeView;

            model.Disposables.Add(bullet);
        }

        public bool BulletCollide(Collider collider, Vector3 position)
        {
            if (collider.TryGetComponent<UnitView>(out var view))
            {
                UnitPresenter presenter = view.GetPresenter();
                var isSuccess = presenter.TryApplyEffects(settings.Effects, model.Team);
                if (isSuccess)
                {
                    CreateHit(position);
                }

                return isSuccess;
            }

            CreateHit(position);

            return true;
        }

        private void CreateHit(Vector3 position)
        {
            var hitSettings = new HitRuntimeSettings(position, Quaternion.identity);
            var hit = model.HitPool.Create(hitSettings);
            hit.transform.SetParent(model.Parent.transform);
            hit.transform.position += Vector3.up;

            hit.OnDispose += DisposeView;

            model.Disposables.Add(hit);
        }

        private void DisposeView(ADisposeView view)
        {
            view.OnDispose -= DisposeView;
            view.transform.SetParent(model.Parent.transform);

            model.Disposables.Remove(view);
        }

        public void Reset()
        {
            model.ActionTimer = 0;
            model.TTL = 0;
            model.IsActing = false;
        }

        public void Disable()
        {
            model.ActionTimer = 0;
            model.TTL = 0;
            model.IsActing = false;

            foreach (var bullet in model.Disposables)
            {
                bullet.OnDispose -= DisposeView;
                bullet.Dispose();
            }

            model.Disposables.Clear();
        }
    }
}
