namespace Infrastructure.AppStateMachine.States
{
    public class MainMenuState : IState
    {
        private IStateMachineMover _stateMachineMover;

        public MainMenuState(
            IStateMachineMover stateMachineMover)
        {
            _stateMachineMover = stateMachineMover;
        }

        public void Enter()
        {

        }

        public void Exit() { }
    }
}
