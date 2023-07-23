namespace Unit.Bullet
{
    public interface IBulletSettings
    {
        [field: SerializeField]
        public float Speed { get; set; }
        [field: SerializeField]
        public int Damage { get; set; }

    }
}
