using System;

namespace Unit
{
    public interface IEffectPresentor
    {
        event Action<IEffectPresentor> OnEffectEnd;
        public void Update();
        public void MakeAction();
        public void EndAction();
    }
}
