using Helpers;

namespace Gameplay
{
    public interface ILevelProgressPresenter
    {
        void ChangeProgress(SignalOnProgressionChange signal);
    }
}