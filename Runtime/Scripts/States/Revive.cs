using UnityEngine;
using TheLegends.Base.FSM;

namespace TheLegends.Base.GameState.States
{
    public class Revive : IState<GameStateManager>
    {
        public virtual void OnEnter(GameStateManager context)
        {
            GameEvents.OnRevive?.Invoke();
        }

        public virtual void OnUpdate(GameStateManager context) { }
        public virtual void OnFixedUpdate(GameStateManager context) { }
        public virtual void OnLateUpdate(GameStateManager context) { }

        public virtual void OnExit(GameStateManager context) { }
    }
}
