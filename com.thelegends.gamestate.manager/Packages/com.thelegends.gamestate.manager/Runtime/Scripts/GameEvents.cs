using System;

namespace TheLegends.Base.GameState
{
    public static class GameEvents
    {
        // Lifecycle Events
        public static Action OnBoot;
        public static Action OnLoading;
        public static Action OnMainMenu;
        public static Action OnPlay;
        public static Action OnPause;
        public static Action OnWaitGameOver;
        public static Action OnRevive;
        public static Action OnGameOver;
        public static Action OnWaitGameComplete;
        public static Action OnGameComplete;
    }
}
