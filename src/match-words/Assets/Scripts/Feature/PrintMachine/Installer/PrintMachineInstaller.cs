using Feature.PrintMachine.View.List.Factory;
using Infrastructure.DIContainer.Extensions;
using Zenject;

namespace Feature.PrintMachine.Installer
{
    public class PrintMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindService<IListWordFactory, ListWordFactory>();
        }
    }
}
