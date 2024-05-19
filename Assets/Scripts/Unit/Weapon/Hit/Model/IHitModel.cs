namespace Unit
{
    public interface IHitModel
    {
        HitRuntimeSettings RuntimeSettings { get; }
        HitSettings Settings { get; }

        void Init(HitRuntimeSettings settings);
    }
}