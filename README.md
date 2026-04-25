# Game State Manager

## Overview
The **Game State Manager** is a robust, event-driven Finite State Machine (FSM) architecture designed to manage the core flow of a Unity game. It is built to ensure **Zero GC (Garbage Collection)** during runtime state transitions and utilizes a **Singleton pattern** for global accessibility.

## Features
- **Zero GC State Machine**: All 10 core states are pre-instantiated. Switching states generates absolutely no garbage.
- **Event-Driven Architecture**: Uses a static `GameEvents` class to broadcast state changes, keeping UI, Audio, and Gameplay completely decoupled from the FSM.
- **Persistent Singleton**: Automatically survives Scene loads (`DontDestroyOnLoad`).
- **Custom Editor**: Provides an intuitive Inspector interface for quick state debugging and transitions.

## Architecture

### 1. `GameStateManager`
The central controller of the system. It inherits from `PersistentMonoSingleton<GameStateManager>` and owns the `StateMachine`.
- Handles the initialization of the State Machine.
- Holds pre-instantiated, read-only references to all 10 core states.
- Automatically enters the `Boot` state on startup.

### 2. The 10 Core States
The game lifecycle is divided into 10 distinct states. Each state inherits from `IState<GameStateManager>`:
1. **Boot**: Initializes 3rd party SDKs (Firebase, Admob, AppsFlyer, etc.).
2. **Loading**: Handles asynchronous scene loading or heavy asset loading.
3. **MainMenu**: The main menu interface.
4. **Play**: Active gameplay. Automatically sets `Time.timeScale = 1f`.
5. **Pause**: Pauses the game. Automatically sets `Time.timeScale = 0f`.
6. **WaitGameOver**: A buffer state between dying and the Game Over screen (e.g., waiting for death animations to finish).
7. **Revive**: Handles revive logic (e.g., watching an ad to revive).
8. **GameOver**: The player has lost. Shows the lose screen.
9. **WaitGameComplete**: A buffer state for victory animations.
10. **GameComplete**: The player has won. Shows the victory screen.

### 3. `GameEvents`
A static class containing C# `Action` delegates for every state lifecycle. 
External systems should subscribe to these events rather than coupling directly with the `GameStateManager`.

---

## How to Use

### 1. Setup in Scene
1. Create an empty GameObject in your Initial Scene and name it `GameStateManager`.
2. Add the `GameStateManager` component to it.
3. Upon entering Play Mode, it will automatically persist across scenes and switch to the **Boot** state.

### 2. Listening to State Changes
To react to state changes, subscribe to the events in `GameEvents` within your `OnEnable` and `OnDisable` methods:

```csharp
using UnityEngine;
using TheLegends.Base.GameState;

public class UIManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnPlay += ShowJoystick;
        GameEvents.OnPause += ShowPauseMenu;
    }

    private void OnDisable()
    {
        GameEvents.OnPlay -= ShowJoystick;
        GameEvents.OnPause -= ShowPauseMenu;
    }

    private void ShowJoystick() { /* ... */ }
    private void ShowPauseMenu() { /* ... */ }
}
```

### 3. Changing States
To change the current state, access the `StateMachine` via the Singleton instance:

```csharp
using TheLegends.Base.GameState;

public class PauseButton : MonoBehaviour
{
    public void OnPauseClicked()
    {
        var manager = GameStateManager.Instance;
        manager.StateMachine.ChangeState(manager.Pause);
    }
}
```

### 4. Inspector State Debugger
When the game is running, select the `GameStateManager` GameObject in the Hierarchy. You will see a custom **State Debugger** interface that displays the current active state and provides 10 buttons to force state transitions for testing purposes.

### 5. Extending with Custom States
If your specific game requires additional states (e.g., `Tutorial`, `Shop`, `Cutscene`), you can seamlessly expand the system **without modifying the core package**:

**Step 1: Create the State Class**
Implement the `IState<GameStateManager>` interface in your project's scripts:
```csharp
using TheLegends.Base.FSM;
using TheLegends.Base.GameState;
using System;

public class CutsceneState : IState<GameStateManager>
{
    public static Action OnCutsceneEnter;

    public void OnEnter(GameStateManager context) => OnCutsceneEnter?.Invoke();
    public void OnUpdate(GameStateManager context) { }
    public void OnFixedUpdate(GameStateManager context) { }
    public void OnLateUpdate(GameStateManager context) { }
    public void OnExit(GameStateManager context) { }
}
```

**Step 2: Instantiate and Transition**
Hold a pre-instantiated reference in your game-specific manager to maintain Zero GC, then pass it to the FSM:
```csharp
public class GameFlowController : MonoBehaviour
{
    // Pre-instantiate to avoid GC allocations
    public readonly CutsceneState Cutscene = new CutsceneState();

    public void StartCutscene()
    {
        GameStateManager.Instance.StateMachine.ChangeState(Cutscene);
    }
}
```

---

## Dependencies
- `com.thelegends.unity.patterns` (Provides `IState<T>`, `StateMachine<T>`, and `PersistentMonoSingleton<T>`)
