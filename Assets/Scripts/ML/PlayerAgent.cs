using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;
using Zenject;
using Unity.MLAgents.Sensors;
using System.Collections.Generic;
using System.Linq;
using Unit;
using Unity.Barracuda;
using System;
using System.Threading.Tasks;
using Helpers;
using System.Threading;

public class PlayerAgent : Agent
{
    [SerializeField] private float episodeDuration = 60f;
    [Space]
    [SerializeField] private float timeReward = 2f;
    [SerializeField] private AnimationCurve timeRewardCurve;
    [SerializeField] private float damageReward = 25f;
    [SerializeField] private AnimationCurve damageRewardCurve;
    [SerializeField] private float killReward = 100f;
    [SerializeField] private AnimationCurve killRewardCurve;

    private IInputService inputService;

    private MobSpawner mobSpawner;
    private EnemySpawner enemySpawner;
    private PlayerPresenter presenter;
    private SignalBus signalBus;

    private bool isEpisodeEnd;
    private float startTime;
    private CancellationTokenSource cts = new CancellationTokenSource();

    [Inject]
    public void Init(IInputService inputService, EnemySpawner enemySpawner, PlayerPresenter presenter, SignalBus signalBus)
    {
        this.inputService = inputService;
        //this.mobSpawner = mobSpawner;
        this.enemySpawner = enemySpawner;
        this.presenter = presenter;
        this.signalBus = signalBus;
    }

    /// <summary>
    /// Called every step of the engine. Here the agent takes an action.
    /// </summary>
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 direction = GetMoveDirection(actionBuffers.ContinuousActions);
        inputService.SetMoveDirection(direction);

        var reward = GetReward(timeRewardCurve, timeReward) * Time.deltaTime;
        AddReward(-reward);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position.x - enemySpawner.SpawnOffset.x);
        sensor.AddObservation(transform.position.z - enemySpawner.SpawnOffset.z);

        //AddUnitsObservation(sensor, mobSpawner.ActiveUnits, transform.position);
        AddUnitsObservation(sensor, enemySpawner.ActiveUnits, transform.position, enemySpawner.SpawnOffset);
    }

    public void EnemyDeath(SignalOnEnemyDeath signal)
    {
        float reward = GetReward(killRewardCurve, killReward);
        AddReward(reward);
    }

    public void TouchBorder(SignalOnObstacleTouch signal)
    {
        if (signal.Presenter.Equals(presenter))
        {
            AddReward(-1);
        }
    }

    public void UnitDamage(SignalOnDamage signal)
    {
        float reward = GetReward(damageRewardCurve, damageReward);
        if (signal.Team == Team.Enemy)
        {
            AddReward(reward);
        }
        //else
        //{
        //    AddReward(-reward);
        //}
    }

    private float GetReward(AnimationCurve rewardCurve, float rewardIndex)
    {
        var time = (Time.time - startTime) / episodeDuration;
        var reward = rewardCurve.Evaluate(time) * rewardIndex;
        return reward;
    }

    public void MobActivate(SignalOnMobActivate signal)
    {
        AddReward(100f);
    }

    private void AddUnitsObservation(VectorSensor sensor, List<UnitView> views, Vector3 position, Vector3 offset)
    {
        var enemyUnits = GetNearestUnits(views, position);
        for (int i = 0; i < 3; i++)
        {
            var observation = Vector3.zero;
            if (enemyUnits.Length > i)
            {
                observation = enemyUnits[i];
                observation -= offset;
            }

            sensor.AddObservation(observation.x);
            sensor.AddObservation(observation.z);
        }
    }

    private Vector3[] GetNearestUnits(List<UnitView> views, Vector3 position)
    {
        var units = views
            .OrderBy(x => Vector3.Distance(position, x.transform.position))
            .Select(x => x.transform.position);

        return units.Take(3).ToArray();
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
            cts.Cancel();
            isEpisodeEnd = true;
            EndEpisode();
        }
    }


    private async void StartEndEpisodeTimer(CancellationTokenSource cts)
    {
        await Task.Delay((int)(episodeDuration * 1000));

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

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
