using System;

namespace Unit
{
    public abstract class EffectPresentor : IEffectPresentor
    {
        protected readonly IUnitModel player;
        protected readonly EffectModel model;

        public virtual event Action<IEffectPresentor> OnEffectEnd;

        public EffectPresentor(IUnitModel unit, EffectModel model)
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
