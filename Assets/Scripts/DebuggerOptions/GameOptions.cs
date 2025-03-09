using System.ComponentModel;
using DebuggerOptions.Core;
using Infrastructure.Services.ToastMessage.Core;
using Infrastructure.StateMachine.Game.States;
using Infrastructure.StateMachine.Game.States.Core;
using Infrastructure.StateMachine.Main.Core;
using Infrastructure.UI.TransitionScreen.Core;

namespace DebuggerOptions
{
    public class GameOptions : BaseOptions
    {
        private const string Category = "Game";

        private readonly IStateMachine<IGameState> _stateMachine;
        private readonly ITransitionScreen _transitionScreen;
        private readonly IToastMessageService _toastMessageService;

        public GameOptions(IStateMachine<IGameState> stateMachine, ITransitionScreen transitionScreen, IToastMessageService toastMessageService)
        {
            _stateMachine = stateMachine;
            _transitionScreen = transitionScreen;
            _toastMessageService = toastMessageService;
        }

        [Category(Category)]
        public void Reload() => _stateMachine.Enter<ReloadState>();

        [Category(Category)]
        public void ShowTransitionScreen() => _transitionScreen.Show();

        [Category(Category)]
        public void HideTransitionScreen() => _transitionScreen.Hide();

        [Category(Category)]
        public void SendTestToastMessage() => _toastMessageService.Send("Test toast message");
    }
}