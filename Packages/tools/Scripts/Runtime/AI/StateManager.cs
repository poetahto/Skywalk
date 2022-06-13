using JetBrains.Annotations;
using UnityEngine;
using Display = UnityTemplateProjects.UI.Display;

namespace Game.Enemy
{
    public class StateManager : MonoBehaviour
    {
        [Header("Settings")] 
        
        [SerializeField]
        [Tooltip("The starting state for this enemy.")]
        private State defaultState;

        [SerializeField] 
        [Tooltip("The display used to show debug information.")]
        private Optional<Display> debugDisplay;
        
        public State DefaultState => defaultState;
        public State CurrentState { get; private set; }
        public bool IsRunning { get; private set; }

        public virtual void Run()
        {
            IsRunning = true;
            ChangeState(DefaultState);
        }

        public virtual void End()
        {
            IsRunning = false;
            ChangeState(null);
        }
        
        protected virtual void Update()
        {
            if (!IsRunning)
                return;
            
            CurrentState.OnStateUpdate(this);
            
            if (debugDisplay.shouldBeUsed)
                debugDisplay.value.UpdateText(CurrentState.StateName);
        }

        [PublicAPI]
        public void ChangeState(State state)
        {
            if (CurrentState != null)
                CurrentState.OnStateExit(this);
            
            CurrentState = state;
            
            if (state != null) 
                state.OnStateEnter(this);
        }
    }
}