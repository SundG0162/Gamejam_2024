using BSM.Tutorials;
using UnityEngine;

namespace BSM.Tutorials
{
    public class TutorialStep4 : TutorialStep
    {
        public override void OnEnter()
        {
            _tutorialManager.SetBackground(false);
            _tutorialManager.SetText("좌클릭을 꾹 눌러 차징후 적을 처치하세요!");
            _tutorialManager.CreateDestination();
        }

        public override void OnUpdate()
        {
        }

        public override void OnExit()
        {
        }
    }
}
