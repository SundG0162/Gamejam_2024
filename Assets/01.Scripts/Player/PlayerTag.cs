using AYellowpaper.SerializedCollections;
using BSM.Core.Cameras;
using BSM.Entities;
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
        [SerializedDictionary("Player Type", "Player Prefab")]
        private SerializedDictionary<EPlayerType, Player> _playerDictionary;

        public Player CurrentPlayer { get; private set; }

        public event Action<Player> OnPlayerChangeEvent;

        private void Awake()
        {
            foreach (EPlayerType type in Enum.GetValues(typeof(EPlayerType)))
            {
                _playerDictionary[type] = Instantiate(_playerDictionary[type], transform);
                Player player = _playerDictionary[type];
                player.gameObject.SetActive(false);
                player.transform.localPosition = Vector3.zero;
                player.GetEntityComponent<EntityRenderer>().Disappear(0);
                player.Initialize(this);
            }

            CurrentPlayer = _playerDictionary[EPlayerType.Damage];
            CurrentPlayer.gameObject.SetActive(true);
            CurrentPlayer.GetEntityComponent<EntityRenderer>().Appear(0);
            CameraManager.Instance.ChangeTarget(CurrentPlayer.transform);
            OnPlayerChangeEvent?.Invoke(CurrentPlayer);
        }

        private void Update()
        {
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

        public void TagPlayer(EPlayerType type)
        {
            Vector3 pos = Vector3.zero;
            if (CurrentPlayer != null)
            {
                CurrentPlayer.GetEntityComponent<EntityMover>().StopImmediately();
                CurrentPlayer.Quit();
                CurrentPlayer.gameObject.SetActive(false);
                pos = CurrentPlayer.transform.position;
            }
            CurrentPlayer = _playerDictionary[type];
            CurrentPlayer.transform.position = pos;
            CurrentPlayer.gameObject.SetActive(true);
            CurrentPlayer.GetEntityComponent<EntityMover>().StopImmediately();
            CurrentPlayer.Join();
            CameraManager.Instance.ChangeTarget(CurrentPlayer.transform);
            OnPlayerChangeEvent?.Invoke(CurrentPlayer);
        }
    }
}
