using NUnit.Framework;
using UnityEngine;
using TheLegends.Base.GameState;
using TheLegends.Base.GameState.States;

namespace TheLegends.Base.GameState.Tests
{
    public class GameStateManagerTests
    {
        private GameObject _managerObj;
        private GameStateManager _manager;

        [SetUp]
        public void SetUp()
        {
            // Reset events
            GameEvents.OnBoot = null;
            GameEvents.OnLoading = null;
            GameEvents.OnPlay = null;
            GameEvents.OnPause = null;
            Time.timeScale = 1f;

            _managerObj = new GameObject("GameStateManager");
            _manager = _managerObj.AddComponent<GameStateManager>();
            _manager.InitializeSingleton();
        }

        [TearDown]
        public void TearDown()
        {
            GameStateManager.DestroyInstance();
            if (_managerObj != null)
            {
                Object.DestroyImmediate(_managerObj);
            }
        }

        [Test]
        public void Instance_IsSetOnAwake()
        {
            Assert.IsNotNull(GameStateManager.Instance);
            Assert.AreEqual(_manager, GameStateManager.Instance);
        }

        [Test]
        public void StateMachine_StartsInBootState()
        {
            Assert.IsNotNull(_manager.StateMachine);
            Assert.IsInstanceOf<Boot>(_manager.StateMachine.CurrentState);
        }

        [Test]
        public void ChangeStateToPlay_InvokesOnPlay_And_SetsTimeScale()
        {
            bool isPlayInvoked = false;
            GameEvents.OnPlay += () => isPlayInvoked = true;

            _manager.StateMachine.ChangeState(_manager.Play);

            Assert.IsTrue(isPlayInvoked);
            Assert.AreEqual(1f, Time.timeScale);
            Assert.IsInstanceOf<Play>(_manager.StateMachine.CurrentState);
        }

        [Test]
        public void ChangeStateToPause_InvokesOnPause_And_SetsTimeScale()
        {
            bool isPauseInvoked = false;
            GameEvents.OnPause += () => isPauseInvoked = true;

            _manager.StateMachine.ChangeState(_manager.Pause);

            Assert.IsTrue(isPauseInvoked);
            Assert.AreEqual(0f, Time.timeScale);
            Assert.IsInstanceOf<Pause>(_manager.StateMachine.CurrentState);
        }

        [Test]
        public void ExitPauseState_RestoresTimeScale()
        {
            _manager.StateMachine.ChangeState(_manager.Pause);
            Assert.AreEqual(0f, Time.timeScale);

            // Change to another state, e.g. Play
            _manager.StateMachine.ChangeState(_manager.Play);
            Assert.AreEqual(1f, Time.timeScale);
        }
    }
}
