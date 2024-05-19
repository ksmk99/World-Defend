using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public class SignalOnAttack
    {

    }

    public abstract class AWeaponPresenter : IWeaponPresenter
    {
        public IWeaponSettings Settings { get; private set; }

        protected readonly WeaponModel model;

        public AWeaponPresenter(IWeaponModel model)
        {
            this.model = (WeaponModel)model;
            Settings = model.Settings;
        }

        public Transform GetTarget()
        {
            return model.Target;
        }

        public virtual void Update(Transform transform, bool isDead, Team team)
        {
            Attack(transform, isDead, team);
        }

        public abstract void Attack(Transform transform, bool isDead, Team team);
        public abstract bool SetAction(Transform transform, Team team);

        public virtual bool IsReloading()
        {
            return !model.IsActing &&
                    model.CanUse &&
                    model.NextUseTime > Time.time;
        }

        protected virtual void CreateBullet(Transform transform, Team team, Quaternion rotation)
        {
            var bulletSettings = new BulletRuntimeSettings(
                Settings.Distance, transform.rotation,
                transform.position, team,
                Settings.Effects);

            BulletView bullet = model.BulletPool.Create(bulletSettings);
            bullet.transform.SetParent(model.Parent.transform);
            bullet.transform.position += Vector3.up;
            bullet.transform.rotation = rotation;

            ABulletPresenter presenter = bullet.GetPresenter();
            presenter.SetWeapon(this);
            bullet.OnDispose += DisposeView;

            model.Disposables.Add(bullet);
        }

        public virtual void CreateHit(Vector3 position)
        {
            var hitSettings = new HitRuntimeSettings(position, Quaternion.identity);
            var hit = model.HitPool.Create(hitSettings);
            hit.transform.SetParent(model.Parent.transform);
            hit.transform.position += Vector3.up;

            hit.OnDispose += DisposeView;

            model.Disposables.Add(hit);
        }

        protected virtual void DisposeView(ADisposeView view)
        {
            view.OnDispose -= DisposeView;
            view.transform.SetParent(model.Parent.transform);

            model.Disposables.Remove(view);
        }

        public virtual void Reset()
        {
            model.ActionTimer = 0;
            model.TTL = 0;
            model.IsActing = false;
        }

        public virtual void Disable()
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
