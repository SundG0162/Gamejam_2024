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
            Debug.Log(textIndex);
            switch (textIndex)
            {
                case 0:
                    _tutorialManager.SetText("<b>태그!</b>는 스탯중 하나가 무제한인 캐릭터들을 바꿔가며 싸우는 게임입니다.");
                    break;
                case 1:
                    _tutorialManager.SetText("지금부터 튜토리얼을 시작하겠습니다.");
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
            _tutorialManager.SetCharacterVisual(false);
            SetIndexText();
            
            _tutorialManager.InputSO.OnMouseClickEvent += SetIndexText;
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit()
        {
            _tutorialManager.InputSO.OnMouseClickEvent -= SetIndexText;
            
        }
    }
}
