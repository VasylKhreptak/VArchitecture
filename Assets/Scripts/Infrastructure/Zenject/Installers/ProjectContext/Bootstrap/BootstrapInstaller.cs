using DebuggerOptions;
using Infrastructure.Coroutines.Runner;
using Infrastructure.Data.Models.Persistent;
using Infrastructure.Data.Models.Static;
using Infrastructure.Data.Models.Static.Core;
using Infrastructure.Data.SaveLoad;
using Infrastructure.Observers.Screen;
using Infrastructure.Services.Asset;
using Infrastructure.Services.AsyncJson;
using Infrastructure.Services.AsyncSaveLoad;
using Infrastructure.Services.AsyncScene;
using Infrastructure.Services.Framerate;
using Infrastructure.Services.ID;
using Infrastructure.Services.Json;
using Infrastructure.Services.Log;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.Scene;
using Infrastructure.Services.ToastMessage;
using Infrastructure.Services.ToastMessage.Core;
using Infrastructure.StateMachine.Game;
using Infrastructure.StateMachine.Game.Factory;
using Infrastructure.StateMachine.Game.States;
using Infrastructure.StateMachine.Game.States.Core;
using Infrastructure.StateMachine.Main.Core;
using Infrastructure.Tools;
using Infrastructure.UI.TransitionScreen;
using UnityEngine;
using Zenject;

namespace Infrastructure.Zenject.Installers.ProjectContext.Bootstrap
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        [Header("References")]
        [SerializeField] private GameObject _coroutineRunnerPrefab;
        [SerializeField] private GameObject _loadingCurtainPrefab;
        [SerializeField] private GameObject _transitionScreenPrefab;

        public override void InstallBindings()
        {
            BindDataModels();
            BindMonoServices();
            BindServices();
            BindScreenObserver();
            BindGameStateMachine();
            BindApplicationPauseDataSaver();
            InitializeDebugger();
            MakeInitializable();
        }

        public void Initialize() => BootstrapGame();

        private void BindMonoServices()
        {
            Container.BindInterfacesTo<CoroutineRunner>().FromComponentInNewPrefab(_coroutineRunnerPrefab).AsSingle();
            Container.BindInterfacesTo<LoadingScreen.LoadingScreen>().FromComponentInNewPrefab(_loadingCurtainPrefab).AsSingle();
            Container.BindInterfacesTo<TransitionScreen>().FromComponentInNewPrefab(_transitionScreenPrefab).AsSingle();
        }

        private void BindDataModels()
        {
            Container.BindInterfacesTo<StaticDataModel>().AsSingle();
            Container.Resolve<IStaticDataModel>().Load();
            Container.BindInterfacesTo<PersistentDataModel>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<SceneService>().AsSingle();
            Container.BindInterfacesTo<AsyncSceneService>().AsSingle();
            Container.BindInterfacesTo<JsonService>().AsSingle();
            Container.BindInterfacesTo<AsyncJsonService>().AsSingle();
            Container.BindInterfacesTo<IDService>().AsSingle();
            Container.BindInterfacesTo<LogService>().AsSingle();
            Container.BindInterfacesTo<FramerateService>().AsSingle();
            Container.BindInterfacesTo<SaveLoadService>().AsSingle();
            Container.BindInterfacesTo<AsyncSaveLoadService>().AsSingle();
            Container.Bind<IToastMessageService>().FromMethod(GetToastMessageServiceImpl).AsSingle();
            Container.BindInterfacesTo<AssetService>().AsSingle();
        }

        private IToastMessageService GetToastMessageServiceImpl(InjectContext context) =>
            InstallerTools
                .SelectImplementation<IToastMessageService, AndroidToastMessageService, IOSToastMessageService, DefaultToastMessageService>(context);

        private void BindScreenObserver() => Container.BindInterfacesAndSelfTo<ScreenObserver>().AsSingle();

        private void BindGameStateMachine()
        {
            BindGameStates();
            Container.Bind<GameStateFactory>().AsSingle();
            Container.BindInterfacesTo<GameStateMachine>().AsSingle();
        }

        private void BindGameStates()
        {
            //chained
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadDataState>().AsSingle();
            Container.Bind<SetupApplicationState>().AsSingle();
            Container.Bind<BootstrapAnalyticsState>().AsSingle();
            Container.Bind<FinalizeBootstrapState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();

            //other
            Container.Bind<ReloadState>().AsSingle();
            Container.Bind<LoadSceneAsyncState>().AsSingle();
            Container.Bind<SaveDataState>().AsSingle();
            Container.Bind<LoadSceneWithTransitionAsyncState>().AsSingle();
        }

        private void BindApplicationPauseDataSaver() => Container.BindInterfacesTo<ApplicationPauseDataSaver>().AsSingle();

        private void BootstrapGame() => Container.Resolve<IStateMachine<IGameState>>().Enter<BootstrapState>();

        private void InitializeDebugger()
        {
            SRDebug.Init();
            Container.BindInterfacesTo<GameOptions>().AsSingle();
        }

        private void MakeInitializable() => Container.Bind<IInitializable>().FromInstance(this).AsSingle();    }
}