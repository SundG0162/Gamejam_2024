using BSM.Tutorials;
using UnityEngine;

namespace BSM.Tutorials
{
    
    // 캐릭터 설명하는 튜토리얼
    public class TutorialStep3 : TutorialStep
    {
        public override void OnEnter()
        {
            _tutorialManager.SetBackground(false);
            _tutorialManager.SetText("게임에는 총 3가지 캐릭터가 있습니다.");
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit()
        {
        }
    }
}
