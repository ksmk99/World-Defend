namespace Unit
{
    public class HealthModel
    {
        public HealthSettings Settings { get; private set; }
        public HealthFollower Follower { get; private set; }    

        public float Health;
        public float NextHealTime;

        public bool IsDead;

        public HealthModel(HealthSettings settings, HealthFollower follower)
        {
            Settings = settings;
            Health = settings.MaxHealth;
            Follower = follower;
            IsDead = false;
        }
    }
}
