using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit
{
    public abstract class EffectPresentor : IEffectPresentor
    {
        protected readonly PlayerModel player;
        protected readonly EffectModel model;

        public virtual event Action<IEffectPresentor> OnEffectEnd;

        public EffectPresentor(PlayerModel player, EffectModel model)
        {
            this.player = player;
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
