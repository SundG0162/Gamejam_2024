using System.Xml;
using BSM.Entities;
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
            _tutorialManager.CreateDummyEnemy();
        }

        public override void OnUpdate()
        {
            if(_tutorialManager.DummyEnemy==null) _tutorialManager.NextTutorial();
        }

        public override void OnExit()
        {
        }
    }
}
