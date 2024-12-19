using BSM.Tutorials;
using UnityEngine;

namespace BSM.Tutorials
{
    public class TutorialStep2 : TutorialStep
    {
        public override void OnEnter()
        {
            _tutorialManager.SetText("WASD로 목표 지점까지 이동하세요!");
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit()
        {
        }
    }
}
