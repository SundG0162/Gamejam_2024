using BSM.Tutorials;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BSM.Tutorials
{
    public class TutorialStep5 : TutorialStep
    {
        private int textIndex = 0;

        public void SetIndexText()
        {
            Debug.Log(textIndex);
            switch (textIndex)
            {
                case 0:
                    _tutorialManager.SetText("이건 마나조각입니다. 마나조각을 모아 태그를 할 수 있습니다..");
                    _tutorialManager.CreateManaPart();
                    break;
                case 1:
                    _tutorialManager.SetText("설명은 여기까지입니다. 다양한 적들이 기다리고 있습니다.\n 이제 직접 게임을 플레이해 보세요!");
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
            _tutorialManager.InputSO.OnMouseClickEvent += SetIndexText;
            SetIndexText();
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit()
        {
            _tutorialManager.InputSO.OnMouseClickEvent -= SetIndexText;
            _tutorialManager.SetBackground(false);
            SceneManager.LoadScene("StartScene");
        }
    }
}
