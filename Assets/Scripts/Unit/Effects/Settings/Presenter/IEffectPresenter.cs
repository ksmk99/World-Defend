using System;

namespace Unit
{
    public interface IEffectPresenter
    {
        event Action<IEffectPresenter> OnEffectEnd;
        public void Update();
        public void MakeAction();
        public void EndAction();
    }
}
