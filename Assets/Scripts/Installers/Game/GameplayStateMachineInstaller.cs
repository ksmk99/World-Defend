using GameplayState;
using Zenject;

namespace Installers
{
    public class GameplayStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<GameplayStateMachine>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<Bootstrap>()
                .AsSingle();
        }
    }
}
