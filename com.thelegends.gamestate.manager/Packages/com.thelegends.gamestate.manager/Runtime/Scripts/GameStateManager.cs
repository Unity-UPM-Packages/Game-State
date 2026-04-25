using UnityEngine;
using TheLegends.Base.FSM;
using TheLegends.Base.GameState.States;
using TheLegends.Base.UnitySingleton;

namespace TheLegends.Base.GameState
{
    public class GameStateManager : PersistentMonoSingleton<GameStateManager>
    {
        public StateMachine<GameStateManager> StateMachine { get; private set; }

        // Pre-initialize states to avoid GC allocations (Zero GC)
        public readonly Boot Boot = new Boot();
        public readonly Loading Loading = new Loading();
        public readonly MainMenu MainMenu = new MainMenu();
        public readonly Play Play = new Play();
        public readonly Pause Pause = new Pause();
        public readonly WaitGameOver WaitGameOver = new WaitGameOver();
        public readonly Revive Revive = new Revive();
        public readonly GameOver GameOver = new GameOver();
        public readonly WaitGameComplete WaitGameComplete = new WaitGameComplete();
        public readonly GameComplete GameComplete = new GameComplete();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            
            StateMachine = new StateMachine<GameStateManager>(this);
            
            // Always start with Boot state when the app launches
            StateMachine.ChangeState(Boot); 
        }

        private void Update() => StateMachine?.Update();
        private void FixedUpdate() => StateMachine?.FixedUpdate();
        private void LateUpdate() => StateMachine?.LateUpdate();
    }
}
