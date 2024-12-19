using System;
using System.Collections.Generic;
using BSM.Inputs;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;

namespace BSM.Tutorials
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField]public InputReaderSO InputSO;
        
        private List<TutorialStep> _tutorialStepList = new List<TutorialStep>();
        private int _tutorialCount = 5; //올려줘야함
        private int _currentStepIndex = 0;
        private TutorialStep _currentStep;
        [SerializeField] private TextMeshProUGUI _text;
        public GameObject Destination;
        public GameObject DummyEnemy;
        public GameObject CharactersVisual;
        public GameObject ManaPart;
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
            _currentStep.OnEnter();
            Destination.GetComponent<DestinationCircle>().PlayerArrived += NextTutorial;
            
            SetBackground(true);
            SetCharacterVisual(false);
            DummyEnemy.SetActive(false);
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

        public void CreateDestination()
        {
            Destination.SetActive(true);
        }
        public void CreateDummyEnemy()
        {
            DummyEnemy.SetActive(true);
        }
        public void CreateManaPart()
        {
            ManaPart.SetActive(true);
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
        public void SetCharacterVisual(bool isOpen)
        {
            if (isOpen)
                CharactersVisual.SetActive(true);
            else
                CharactersVisual.SetActive(false);
        }

        public void HighlightOneCharacter(int index)
        {
            for (int i = 0; i < CharactersVisual.transform.childCount; i++)
            {
                CharactersVisual.transform.GetChild(i).transform.localScale = Vector3.one;
            }
            if(index==-1) return;
            CharactersVisual.transform.GetChild(index).transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.5f);
        }
    }
}