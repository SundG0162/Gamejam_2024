using BSM.Tutorials;
using UnityEngine;

namespace BSM.Tutorials
{
    public class TutorialStep3 : TutorialStep
    {
        public override void OnEnter()
        {
            _tutorialManager.SetText("Shift로 대쉬하세요!");
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit()
        {
        }
    }
}
