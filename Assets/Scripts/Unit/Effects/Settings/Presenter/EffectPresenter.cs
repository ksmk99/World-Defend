using System;

namespace Unit
{
    public abstract class EffectPresenter : IEffectPresenter
    {
        protected readonly IUnitModel player;
        protected readonly EffectModel model;

        public virtual event Action<IEffectPresenter> OnEffectEnd;

        public EffectPresenter(IUnitModel unit, EffectModel model)
        {
            this.player = unit;
            this.model = model;
        }

        public abstract void Update();
        public abstract void MakeAction();

        public virtual void EndAction()
        {
            OnEffectEnd?.Invoke(this);
        }
    }
}
