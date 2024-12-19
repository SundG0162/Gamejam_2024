using BSM.Tutorials;
using UnityEngine;

namespace BSM.Tutorials
{
    public class TutorialStep2 : TutorialStep
    {
        public override void OnEnter()
        {
            _tutorialManager.SetBackground(false);
            _tutorialManager.SetText("WASD로 목표 지점까지 이동하세요!");
            _tutorialManager.CreateDestination();
        }

        public override void OnUpdate()
        {
            Debug.Log("step2");
        }

        public override void OnExit()
        {
        }
    }
}
