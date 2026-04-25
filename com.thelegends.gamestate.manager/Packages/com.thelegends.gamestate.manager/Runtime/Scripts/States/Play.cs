using UnityEngine;
using TheLegends.Base.FSM;

namespace TheLegends.Base.GameState.States
{
    public class Play : IState<GameStateManager>
    {
        public virtual void OnEnter(GameStateManager context)
        {
            Time.timeScale = 1f; // Ensure normal time scale
            GameEvents.OnPlay?.Invoke(); // Notify Player to enable input, UI to show Joystick
        }

        public virtual void OnUpdate(GameStateManager context) { }
        public virtual void OnFixedUpdate(GameStateManager context) { }
        public virtual void OnLateUpdate(GameStateManager context) { }

        public virtual void OnExit(GameStateManager context) { }
    }
}
