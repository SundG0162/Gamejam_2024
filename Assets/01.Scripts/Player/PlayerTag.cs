using AYellowpaper.SerializedCollections;
using BSM.Core.Cameras;
using BSM.Entities;
using BSM.Inputs;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace BSM.Players
{
    public enum EPlayerType
    {
        Damage,
        AttackSpeed,
        Armor
    }
    public class PlayerTag : MonoBehaviour
    {
        [SerializeField]
        private InputReaderSO _inputReader;

        [SerializeField]
        [SerializedDictionary("Player Type", "Player Prefab")]
        private SerializedDictionary<EPlayerType, Player> _playerDictionary;

        public Player CurrentPlayer { get; private set; }
        public EPlayerType CurrentPlayerEnum { get; private set; }

        [field: SerializeField]
        public float MaxMana { get; private set; }
        private float _currentMana = 0;
        public delegate void ManaChangeEvent(float prevValue, float currentValue);

        public event Action<Player> OnPlayerChangeEvent;
        public event ManaChangeEvent OnManaChangeEvent;

        private EPlayerType[] _tagPlayers = new EPlayerType[3];

        private void Awake()
        {
            int index = 0;
            foreach (EPlayerType type in Enum.GetValues(typeof(EPlayerType)))
            {
                _playerDictionary[type] = Instantiate(_playerDictionary[type], transform);
                Player player = _playerDictionary[type];
                player.gameObject.SetActive(false);
                player.transform.localPosition = Vector3.zero;
                player.GetEntityComponent<EntityRenderer>().Disappear(0);
                player.Initialize(this);
                _tagPlayers[index++] = type;
            }

            CurrentPlayer = _playerDictionary[EPlayerType.Damage];
            CurrentPlayerEnum = EPlayerType.Damage;
            CurrentPlayer.gameObject.SetActive(true);
            CurrentPlayer.GetEntityComponent<EntityRenderer>().Appear(0);
            CameraManager.Instance.ChangeTarget(CurrentPlayer.transform);
            OnPlayerChangeEvent?.Invoke(CurrentPlayer);
            _inputReader.OnTagEvent += HandleOnTagEvent;
        }

        private void HandleOnTagEvent(int index)
        {
        }

        private void Update()
        {
            ModifyMana(Time.deltaTime);
            if (Keyboard.current.gKey.wasPressedThisFrame)
            {
                TagPlayer(EPlayerType.AttackSpeed);
            }
            if (Keyboard.current.hKey.wasPressedThisFrame)
            {
                TagPlayer(EPlayerType.Damage);
            }
            if (Keyboard.current.jKey.wasPressedThisFrame)
            {
                TagPlayer(EPlayerType.Armor);
            }
        }

        public void TagPlayer(EPlayerType type, bool withoutMana = false)
        {
            if (!withoutMana)
            {
                if (_currentMana < MaxMana / 2)
                    return;
                ModifyMana(-MaxMana / 2);
            }
            Vector3 pos = Vector3.zero;
            if (CurrentPlayer != null)
            {
                CurrentPlayer.GetEntityComponent<EntityMover>().StopImmediately();
                CurrentPlayer.Quit();
                CurrentPlayer.gameObject.SetActive(false);
                pos = CurrentPlayer.transform.position;
            }
            CurrentPlayerEnum = type;
            CurrentPlayer = _playerDictionary[type];
            CurrentPlayer.transform.position = pos;
            CurrentPlayer.gameObject.SetActive(true);
            CurrentPlayer.GetEntityComponent<EntityMover>().StopImmediately();
            CurrentPlayer.Join();
            CameraManager.Instance.ChangeTarget(CurrentPlayer.transform);
            OnPlayerChangeEvent?.Invoke(CurrentPlayer);
        }

        public void ModifyMana(float value)
        {
            float prevMana = _currentMana;
            _currentMana += value;
            _currentMana = Mathf.Clamp(_currentMana, 0, MaxMana);
            OnManaChangeEvent?.Invoke(prevMana, _currentMana);
        }
    }
}
