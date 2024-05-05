using Gameplay;
using Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unit;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Zenject;

public class PlayerAgent : Agent
{
    [SerializeField] private bool isDebug;
    [Space]
    [SerializeField] private float minCumulativeReward = -1000;
    [SerializeField] private float bulletVisionRadius = 5f;
    [SerializeField] private LayerMask bulletVisionLayer;
    [Space]
    [SerializeField] private float timeReward = 2f;
    [SerializeField] private AnimationCurve timeRewardCurve;
    [SerializeField] private float damageReward = 25f;
    [SerializeField] private AnimationCurve damageRewardCurve;
    [SerializeField] private float killReward = 100f;
    [SerializeField] private AnimationCurve killRewardCurve;

    [SerializeField] private float distanceReward = 100f;
    [SerializeField] private AnimationCurve distanceRewardCurve;

    [SerializeField] private float mobReward = 100f;
    [SerializeField] private AnimationCurve mobRewardCurve;
    [Space]
    [SerializeField] private float minBulletDistance = 3f;
    [SerializeField] private float bulletDistanceReward = 100f;
    [SerializeField] private AnimationCurve bulletDistanceRewardCurve;
    [Space]
    [SerializeField] private AnimationCurve deathRewardCurve;
    [SerializeField] private float deathReward;


    private IInputService inputService;

    private LevelSettingsData levelSettings;
    private MobSpawner mobSpawner;
    private EnemySpawner enemySpawner;
    private PlayerPresenter presenter;
    private SignalBus signalBus;
    private IWeaponPresenter weapon;

    private bool isEpisodeEnd;
    private float startTime;
    private CancellationTokenSource cts = new CancellationTokenSource();

    private float cumulative_reward;
    private Vector3[] bullets;
    private Vector3 roomSize => enemySpawner.RoomSize * 1.2f;

    [Inject]
    public void Init(IInputService inputService, LevelSettingsData levelSettings,
        EnemySpawner enemySpawner, MobSpawner mobSpawner,
        PlayerPresenter presenter, SignalBus signalBus, IWeaponPresenter weapon)
    {
        this.inputService = inputService;
        this.mobSpawner = mobSpawner;
        this.enemySpawner = enemySpawner;
        this.presenter = presenter;
        this.signalBus = signalBus;
        this.weapon = weapon;
        this.levelSettings = levelSettings;

        time = Time.time;
    }

    float time = 0;
    /// <summary>
    /// Вызывается каждый шаг движка. Здесь агент выполняет действие.
    /// </summary>
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        //Установка направления движения юнита
        Vector3 direction = GetMoveDirection(actionBuffers.ContinuousActions);
        inputService.SetMoveDirection(direction);

        //Отрицательная оценка за каждый шаг
        var reward = GetReward(timeRewardCurve, timeReward);
        UpdateRewardValue(-reward);

        //Оценивание дистанции до соперников
        SetEnemiesDistanceReward();
    }

    private void SetEnemiesDistanceReward()
    {
        var enemyUnits = GetNearestUnits(enemySpawner.ActiveUnits, transform.position);
        if (enemyUnits.Length > 0)
        {
            var distance = Vector3.Distance(enemyUnits[0].transform.position, transform.position);
            var index = distance > weapon.Settings.MinDistance && distance < weapon.Settings.Distance ? 1 : -1;

            if (index > 0)
            {
                var reward = GetReward(distanceRewardCurve, distanceReward);
                UpdateRewardValue(reward * index);
            }
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        var xNormalized = GetNormalizedValue(transform.position.x, enemySpawner.RoomSize.x, enemySpawner.SpawnOffset.x);
        sensor.AddObservation(xNormalized);

        var zNormalized = GetNormalizedValue(transform.position.z, enemySpawner.RoomSize.z, enemySpawner.SpawnOffset.z);
        sensor.AddObservation(zNormalized);

        AddUnitsObservation(sensor, enemySpawner.ActiveUnits, transform.position, enemySpawner.SpawnOffset);
        AddUnitsObservation(sensor, mobSpawner.ActiveUnits, transform.position, enemySpawner.SpawnOffset);

        AddBulletsObservation(sensor, transform.position, bulletVisionRadius, bulletVisionLayer);
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var direction = new Vector2(x, y);
        if (direction.Equals(Vector2.zero))
        {
            direction = inputService.GetMoveDirection();
        }

        continuousActionsOut[0] = direction.x;
        continuousActionsOut[1] = direction.y;
    }

    private void UpdateRewardValue(float reward)
    {
        AddReward(reward);
        cumulative_reward += reward;
    }

    private float GetNormalizedValue(float value, float roomSize, float offset)
    {
        value = value - offset;
        var minValue = -roomSize / 2f;
        var maxValue = roomSize / 2f;
        var normalizeValue = (value - minValue) / (maxValue - minValue);
        return normalizeValue;
    }

    private void AddBulletsObservation(VectorSensor sensor, Vector3 position, float radius, LayerMask layer)
    {
        var colliders = new Collider[1];
        var bulletsCount = Physics.OverlapSphereNonAlloc(position, radius, colliders, layer);
        for (int i = 0; i < 3; i++)
        {
            var bulletPos = Vector3.zero;
            if (bulletsCount > i)
            {
                bulletPos = colliders[i].transform.position;
            }

            var value = GetNormalizedValue(bulletPos.x, roomSize.x, enemySpawner.SpawnOffset.x);
            sensor.AddObservation(value);
            value = GetNormalizedValue(bulletPos.z, roomSize.z, enemySpawner.SpawnOffset.z);
            sensor.AddObservation(value);
        }


        bullets = colliders.Where(x => x != null).Select(x => x.transform.position).ToArray();
    }

    public void PlayerDeath(SignalOnPlayerDeath signal)
    {
        float reward = GetReward(deathRewardCurve, deathReward);
        UpdateRewardValue(reward);
    }

    private void OnDrawGizmos()
    {
        if (!isDebug || bullets == null)
        {
            return;
        }

        var sizes = new Color[3] { Color.red, Color.yellow, Color.green };
        for (int i = 0; i < bullets.Length; i++)
        {
            Gizmos.color = sizes[i % sizes.Length];
            Gizmos.DrawSphere(bullets[i], 1f);
        }
    }

    public void EnemyDeath(SignalOnEnemyDeath signal)
    {
        float reward = GetReward(killRewardCurve, killReward);
        UpdateRewardValue(reward);
    }

    public void TouchBorder(SignalOnObstacleTouch signal)
    {
        if (signal.Presenter.Equals(presenter))
        {
            UpdateRewardValue(-0.05f);
        }
    }


    private float GetReward(AnimationCurve rewardCurve, float rewardIndex)
    {
        var time = (Time.time - startTime) / levelSettings.Duration;
        var reward = rewardCurve.Evaluate(time) * rewardIndex;
        return reward;
    }

    public void MobActivate(SignalOnMobActivate signal)
    {
        var reward = GetReward(mobRewardCurve, mobReward);
        UpdateRewardValue(reward);
    }

    private void AddUnitsObservation(VectorSensor sensor, List<UnitView> views, Vector3 position, Vector3 offset)
    {
        var enemyUnits = GetNearestUnits(views, position, 3);
        foreach (var unit in enemyUnits) 
        {
            var observation = Vector3.zero;
            var id = 0;
            if (unit != null)
            {
                observation = unit.transform.position;
                observation -= offset;

                id = unit.GetPoolID();
            }

            sensor.AddObservation(id);

            var value = GetNormalizedValue(observation.x, roomSize.x, enemySpawner.SpawnOffset.x);
            sensor.AddObservation(value);
            value = GetNormalizedValue(observation.z, roomSize.z, enemySpawner.SpawnOffset.z);
            sensor.AddObservation(value);
        }
    }

    private UnitView[] GetNearestUnits(List<UnitView> views, Vector3 position, int count = 3)
    {
        var units = views
            .OrderBy(x => Vector3.Distance(position, x.transform.position));

        return units.Take(count).ToArray();
    }

    private Vector3 GetMoveDirection(ActionSegment<float> continuousActions)
    {
        return new Vector3(continuousActions[0], 0, continuousActions[1]);
    }

    public void Begin(SignalOnRoomReset signal)
    {
        cts = new CancellationTokenSource();
        StartEndEpisodeTimer(cts);
        isEpisodeEnd = false;
        startTime = Time.time;
    }

    public void RoomReset(SignalOnRoomReset signal)
    {
        if (signal.RoomIndex.Equals(presenter.RoomIndex))
        {
            EndCurrentEpisode();
        }
    }

    private void EndCurrentEpisode()
    {
        cts.Cancel();
        isEpisodeEnd = true;
        EndEpisode();

        if (isDebug)
        {
            Debug.Log($"Reward: {cumulative_reward}");
        }

        cumulative_reward = 0;
    }


    private async void StartEndEpisodeTimer(CancellationTokenSource cts)
    {
        await Task.Delay((int)(levelSettings.Duration * 1000));

        if (cts.IsCancellationRequested)
        {
            return;
        }

        if (!isEpisodeEnd)
        {
            isEpisodeEnd = true;
            signalBus.TryFire(new SignalOnTimeRoomReset(presenter.RoomIndex));
        }
    }
}
