using BSM.Tutorials;
using UnityEngine;

namespace BSM.Tutorials
{
    
    // 캐릭터 설명하는 튜토리얼
    public class TutorialStep3 : TutorialStep
    {
        private int textIndex = 0;

        public void SetIndexText()
        {
            Debug.Log(textIndex);
            switch (textIndex)
            {
                case 0:
                    _tutorialManager.SetText("<b>태그!<b>에는 총 3개의 캐릭터가 있습니다.");
                    _tutorialManager.SetCharacterVisual(true);
                    break;
                case 1:
                    _tutorialManager.SetText("무제한 방어력을 가진 캐릭터.\n 피해를 받지 않습니다. 5초후 자동 태그됩니다.");
                    _tutorialManager.HighlightOneCharacter(0);
                    break;
                case 2:
                    _tutorialManager.SetText("무제한 공격력을 가진 캐릭터. 모든 적을 한번에 죽입니다.\n한번에 10마리 이상 처치시 체력을 회복합니다.");
                    _tutorialManager.HighlightOneCharacter(1);
                    break;
                case 3:
                    _tutorialManager.SetText("<무제한 공격속도를 가진 캐릭터.\n 빠른 공격으로 적을 밀쳐내 처치합니다.");
                    _tutorialManager.HighlightOneCharacter(2);
                    break;
                case 4:
                    _tutorialManager.SetText("마나가 충분할때 숫자 1과 2를 눌러 캐릭터를 태그 할 수 있습니다.");
                    _tutorialManager.HighlightOneCharacter(-1);
                    break;
                case 5:
                    _tutorialManager.SetCharacterVisual(false);
                    _tutorialManager.NextTutorial();
                    break;
            }

            textIndex++;
        }
        public override void OnEnter()
        {
            _tutorialManager.SetBackground(true);
            _tutorialManager.SetCharacterVisual(true);
            _tutorialManager.InputSO.OnMouseClickEvent += SetIndexText;
            SetIndexText();
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit()
        {
            _tutorialManager.SetBackground(false);
            _tutorialManager.InputSO.OnMouseClickEvent -= SetIndexText;
        }
    }
}
