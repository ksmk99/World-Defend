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
        [SerializeField] private EnemySpawnerSettings spawnerSettings;

        public override void InstallBindings()
        {
            BindSignals();
            Container
                .BindInstance(playerMS)
                .IfNotBound();
            Container
                .BindInstance(spawnerSettings)
                .IfNotBound();

            BindLevelProgression();
        }

        private void BindLevelProgression()
        {
            Container
                .Bind<LevelProgressionService>()
                .AsSingle();
            Container
                .BindSignal<SignalOnPlayerDeath>()
                .ToMethod<LevelProgressionService>(x => x.PlayerDeath)
                .FromResolve();
            Container
                .BindSignal<SignalOnEnemyDeath>()
                .ToMethod<LevelProgressionService>(x => x.EnemyDeath)
                .FromResolve();
        }

        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignalWithInterfaces<SignalOnUnitDamage>();
            Container.DeclareSignalWithInterfaces<SignalOnUnitHeal>();

            Container.DeclareSignal<SignalOnUnitDeath>();
            Container.DeclareSignal<SignalOnEnemyDeath>();
            Container.DeclareSignal<SignalOnPlayerDeath>();

            Container.DeclareSignal<SignalOnMove>();
            Container.DeclareSignal<SignalOnAttack>();
        }
    }
}