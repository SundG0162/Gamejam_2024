using System;
using System.Collections.Generic;
using BSM.Inputs;
using TMPro;
using UnityEngine;

namespace BSM.Tutorials
{
    public class TutorialManager : MonoBehaviour
    {
        public InputReaderSO input;
        
        private List<TutorialStep> _tutorialStepList = new List<TutorialStep>();
        private int _tutorialCount = 2; //올려줘야함
        private int _currentStepIndex = 1;
        private TutorialStep _currentStep;
        [SerializeField] private TextMeshProUGUI _text;
        public GameObject destination;
        public SpriteRenderer blackBackground;
        private void Awake()
        {
            for (int i = 1; i <= _tutorialCount; i++)
            {
                Type type = Type.GetType($"BSM.Tutorials.TutorialStep{i}");
                TutorialStep step = Activator.CreateInstance(type) as TutorialStep;
                print(step);
                _tutorialStepList.Add(step);
            }
            _tutorialStepList.ForEach(step => step.Initialize(this));
            _currentStep = _tutorialStepList[0];
            destination.GetComponent<DestinationCircle>().PlayerArrived += NextTutorial;
        }

        private void Update()
        {
            _currentStep.OnUpdate();
        }

        public void NextTutorial()
        {
            _currentStep.OnExit();
            _currentStep = _tutorialStepList[++_currentStepIndex];
            _currentStep.OnEnter();
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void CreateDestiantion()
        {
            destination.SetActive(true);
        }

        public void SetBackground(bool isBlack)
        {
            Color color = blackBackground.color;
            if (isBlack)
                color.a = 0.5f;
            else
                color.a = 0f;
            blackBackground.color = color;
        }
    }
}