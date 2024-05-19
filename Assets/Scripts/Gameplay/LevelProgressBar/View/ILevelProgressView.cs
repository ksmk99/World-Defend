namespace Gameplay
{
    public interface ILevelProgressView
    {
        void Init(int level);
        void SetValue(float value);
    }
}