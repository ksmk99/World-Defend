using Helpers;
using Unit;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "Game Settings Installer", menuName = "Installers/Game Settings Installer")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private MovementSettings playerMS;
        [SerializeField] private EnemyMovementSettings enemyMS;
        [SerializeField] private MobMovementSettings mobMS;
        [SerializeField] private EnemySpawnerSettings spawnerSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(playerMS).IfNotBound();
            Container.BindInstance(enemyMS).IfNotBound();
            Container.BindInstance(mobMS).IfNotBound();
            Container.BindInstance(spawnerSettings).IfNotBound();
        }
    }
}