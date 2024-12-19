using System;
using System.Collections.Generic;
using UnityEngine;

namespace BSM.Tutorials
{
    public class TutorialManager : MonoBehaviour
    {
        private List<TutorialStep> _tutorialStepList;
        private int _tutorialCount = 0; //올려줘야함
        private int _currentStepIndex = 0;
        private TutorialStep _currentStep;

        private void Awake()
        {
            for (int i = 0; i < _tutorialCount; i++)
            {
                Type type = Type.GetType($"TutorialStep{i}");
                TutorialStep step = Activator.CreateInstance(type) as TutorialStep;
                _tutorialStepList.Add(step);
            }
            _tutorialStepList.ForEach(step => step.Initialize(this));
            _currentStep = _tutorialStepList[0];
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
    }
}