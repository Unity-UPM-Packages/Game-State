using UnityEngine;
using TheLegends.Base.FSM;

namespace TheLegends.Base.GameState.States
{
    public class Pause : IState<GameStateManager>
    {
        public virtual void OnEnter(GameStateManager context)
        {
            Time.timeScale = 0f; // Pause the game
            GameEvents.OnPause?.Invoke();
        }

        public virtual void OnUpdate(GameStateManager context) { }
        public virtual void OnFixedUpdate(GameStateManager context) { }
        public virtual void OnLateUpdate(GameStateManager context) { }

        public virtual void OnExit(GameStateManager context)
        {
            Time.timeScale = 1f; // Restore time scale when exiting Pause
        }
    }
}
