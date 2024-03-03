using Gameplay;
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

        public override void InstallBindings()
        {
            BindSignals();
            Container
                .BindInstance(playerMS)
                .IfNotBound();
        }

        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignalWithInterfaces<SignalOnUnitDamage>();
            Container.DeclareSignalWithInterfaces<SignalOnUnitHeal>();

            Container.DeclareSignal<SignalOnDamage>();
            Container.DeclareSignal<SignalOnMobActivate>();
            Container.DeclareSignal<SignalOnObstacleTouch>();

            Container.DeclareSignal<SignalOnMobDeath>();
            Container.DeclareSignal<SignalOnEnemyDeath>();
            Container.DeclareSignal<SignalOnPlayerDeath>();

            Container.DeclareSignal<SignalOnPlayerReset>();
            Container.DeclareSignal<SignalOnEnemyReset>();
            Container.DeclareSignal<SignalOnMobReset>();

            Container.DeclareSignal<SignalOnMove>();
            Container.DeclareSignal<SignalOnAttack>();

            Container.DeclareSignal<SignalOnProgressionChange>();
            Container.DeclareSignal<SignalOnRoomReset>();
        }
    }
}