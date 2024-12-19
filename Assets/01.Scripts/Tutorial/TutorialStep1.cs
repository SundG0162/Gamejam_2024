using BSM.Tutorials;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BSM.Tutorials
{
    public class TutorialStep1 : TutorialStep
    {
        private int textIndex = 0;

        public void SetIndexText()
        {
            switch (textIndex)
            {
                case 0:
                    _tutorialManager.SetText("태그!는 스탯중 하나가 무제한인 캐릭터들을 바꿔가며 싸우는 게임입니다.");
                    break;
                case 1:
                    _tutorialManager.SetText("지금부터 튜토리얼을 알려드리겠습니다.");
                    break;
                case 2:
                    _tutorialManager.NextTutorial();
                    break;
            }

            textIndex++;
        }
        
        public override void OnEnter()
        {
            _tutorialManager.SetBackground(true);
            SetIndexText();
        }

        public override void OnUpdate()
        {
            _tutorialManager.input.OnMouseClickEvent += SetIndexText;
                
        }

        public override void OnExit()
        {
            _tutorialManager.input.OnMouseClickEvent -= SetIndexText;
            
        }
    }
}
