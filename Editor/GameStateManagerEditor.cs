using UnityEditor;
using UnityEngine;
using TheLegends.Base.GameState;

namespace TheLegends.Base.GameState.Editor
{
    [CustomEditor(typeof(GameStateManager))]
    public class GameStateManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw default inspector
            base.OnInspectorGUI();

            GameStateManager manager = (GameStateManager)target;

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("State Debugger", EditorStyles.boldLabel);

            if (!Application.isPlaying)
            {
                EditorGUILayout.HelpBox("State transitions are only available during Play Mode.", MessageType.Info);
                return;
            }

            if (manager.StateMachine == null)
            {
                EditorGUILayout.HelpBox("StateMachine is not initialized yet.", MessageType.Warning);
                return;
            }

            // Display current state
            string currentStateName = manager.StateMachine.CurrentState != null
                ? manager.StateMachine.CurrentState.GetType().Name
                : "None";

            GUIStyle stateStyle = new GUIStyle(GUI.skin.box);
            stateStyle.fontSize = 20;
            stateStyle.fontStyle = FontStyle.Bold;
            stateStyle.alignment = TextAnchor.MiddleCenter;
            stateStyle.normal.textColor = Color.green;

            EditorGUILayout.LabelField($"► {currentStateName.ToUpper()} ◄", stateStyle, GUILayout.Height(50));
            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("Quick Transitions", EditorStyles.label);

            // Group buttons in rows for better layout
            DrawButtonRow(manager, "Boot", manager.Boot, "Loading", manager.Loading);
            DrawButtonRow(manager, "MainMenu", manager.MainMenu, "Play", manager.Play);
            DrawButtonRow(manager, "Pause", manager.Pause, "WaitGameOver", manager.WaitGameOver);
            DrawButtonRow(manager, "Revive", manager.Revive, "GameOver", manager.GameOver);
            DrawButtonRow(manager, "WaitGameComplete", manager.WaitGameComplete, "GameComplete", manager.GameComplete);
        }

        private void DrawButtonRow(GameStateManager manager, string label1, TheLegends.Base.FSM.IState<GameStateManager> state1, string label2, TheLegends.Base.FSM.IState<GameStateManager> state2)
        {
            Rect rect = EditorGUILayout.GetControlRect(false, 30);

            // Calculate equal width for two buttons with a small 4px gap in between
            float halfWidth = (rect.width - 4) / 2f;

            Rect rect1 = new Rect(rect.x, rect.y, halfWidth, rect.height);
            Rect rect2 = new Rect(rect.x + halfWidth + 4, rect.y, halfWidth, rect.height);

            if (GUI.Button(rect1, label1))
            {
                manager.StateMachine.ChangeState(state1);
            }

            if (GUI.Button(rect2, label2))
            {
                manager.StateMachine.ChangeState(state2);
            }

            // Add a little bit of vertical spacing after each row
            EditorGUILayout.Space(2);
        }
    }
}
