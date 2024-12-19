using UnityEngine;

namespace BSM.Tutorials
{
    public abstract class TutorialStep
    {
        protected TutorialManager _tutorialManager;
        public virtual void Initialize(TutorialManager tutorialManager) 
        {
            _tutorialManager = tutorialManager;
        }
        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }
}
