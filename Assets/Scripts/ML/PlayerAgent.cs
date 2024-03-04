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

    private IInputService inputService;

    private MobSpawner mobSpawner;
    private EnemySpawner enemySpawner;
    private PlayerPresenter presenter;

    private bool isEpisodeEnd;
    private CancellationTokenSource cts = new CancellationTokenSource();

    [Inject]
    public void Init(IInputService inputService, MobSpawner mobSpawner, EnemySpawner enemySpawner, PlayerPresenter presenter)
    {
        this.inputService = inputService;
        this.mobSpawner = mobSpawner;
        this.enemySpawner = enemySpawner;
        this.presenter = presenter;
    }

    /// <summary>
    /// Called every step of the engine. Here the agent takes an action.
    /// </summary>
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 direction = GetMoveDirection(actionBuffers.ContinuousActions);
        inputService.SetMoveDirection(direction);

        AddReward(-1f / MaxStep);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position.x);
        sensor.AddObservation(transform.position.z);

        AddUnitsObservation(sensor, mobSpawner.ActiveUnits, transform.position);
        AddUnitsObservation(sensor, enemySpawner.ActiveUnits, transform.position);
    }

    public void EnemyDeath(SignalOnEnemyDeath signal)
    {
        AddReward(20f);
    }

    public void TouchBorder(SignalOnObstacleTouch signal)
    {
        if (signal.Presenter.Equals(presenter))
        {
            AddReward(-20f);
        }
    }

    public void UnitDamage(SignalOnDamage signal)
    {
        if (signal.Team == Team.Enemy)
        {
            AddReward(5f);
        }
        else
        {
            AddReward(-5);
        }
    }

    public void MobActivate(SignalOnMobActivate signal)
    {
        AddReward(15f);
    }



    private void AddUnitsObservation(VectorSensor sensor, List<UnitView> views, Vector3 position)
    {
        var enemyUnits = GetNearestUnits(views, position);
        for (int i = 0; i < 3; i++)
        {
            var observation = Vector3.zero;
            if (enemyUnits.Length > i)
            {
                observation = enemyUnits[i];
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

    public override void OnEpisodeBegin()
    {
        cts = new CancellationTokenSource();
        StartEndEpisodeTimer(cts);
        isEpisodeEnd = false;
    }

    public void RoomReset(SignalOnRoomReset signal)
    {
        if (signal.RoomIndex.Equals(presenter.RoomIndex) && !isEpisodeEnd)
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
            presenter.Death();
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
