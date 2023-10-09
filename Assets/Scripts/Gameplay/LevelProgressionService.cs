using GameplayState;
using Helpers;
using Unit;
using UnityEngine;
using Zenject;

public class LevelProgressionService 
{
    private readonly EnemySpawnerSettings spawnerSettings;
    private readonly GameplayStateMachine stateMachine;
    private readonly SignalBus signalBus;

    private int killCount;

    public LevelProgressionService(GameplayStateMachine stateMachine, SignalBus signalBus, EnemySpawnerSettings spawnerSettings)
    {
        this.stateMachine = stateMachine;
        this.signalBus = signalBus;
        this.spawnerSettings = spawnerSettings;
    }

    public void EnemyDeath(SignalOnEnemyDeath signal)
    {
        killCount++;
        Debug.Log("Enemy Defeated " + killCount);
        if (killCount >= spawnerSettings.Count)
        {
            stateMachine.TransitionTo(new WinState());
        }
    }

    public void PlayerDeath(SignalOnPlayerDeath signal)
    {
        stateMachine.TransitionTo(new LooseState());
    }
}
