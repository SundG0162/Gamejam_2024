using BSM.Animators;
using BSM.Core.Cameras;
using BSM.Core.StatSystem;
using BSM.Entities;
using BSM.Inputs;
using Crogen.CrogenPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BSM.Players
{
    public abstract class Player : Entity
    {
        [field: SerializeField]
        public InputReaderSO InputReader { get; private set; }
        protected EntityMover _entityMover;
        protected EntityRenderer _entityRenderer;
        protected EntityStat _entityStat;
        public PlayerTag PlayerTag => _playerTag;
        protected PlayerTag _playerTag;

        [SerializeField]
        private AnimatorParameterSO _idleParameter;
        [SerializeField]
        private StatElementSO _dashCooltimeElement;


        public event Action OnJoinEvent;
        public event Action OnQuitEvent;

        public bool StopFlip { get; set; } = false;

        private float _dashCooltime;
        private float _dashTimer = 0f;

        protected override void Awake()
        {
            base.Awake();
            _entityMover = GetEntityComponent<EntityMover>();
            _entityRenderer = GetEntityComponent<EntityRenderer>();
            _entityStat = GetEntityComponent<EntityStat>();
            _dashCooltimeElement = _entityStat.GetStatElement(_dashCooltimeElement);
            _dashCooltime = _dashCooltimeElement.Value;
            //todo : OnValueChanged 만들기. 할 일이 생길지는 모르겠음. 언젠간 할걸?
        }

        public void Initialize(PlayerTag tag)
        {
            _playerTag = tag;
        }

        protected virtual void OnEnable()
        {
            InputReader.OnMovementEvent += HandleOnMovementEvent;
            InputReader.OnDashEvent += HandleOnDashEvent;
        }

        protected virtual void OnDisable()
        {
            InputReader.OnMovementEvent -= HandleOnMovementEvent;
            InputReader.OnDashEvent -= HandleOnDashEvent;
        }

        protected virtual void Update()
        {
            FlipToMouseCursor();
            _dashTimer += Time.deltaTime;
        }

        private void FlipToMouseCursor()
        {
            if (StopFlip) return;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(InputReader.MousePosition);
            Vector3 direction = transform.position - mousePos;
            float crossValue = Vector3.Cross(direction.normalized, Vector3.up).z;
            _entityRenderer.Flip(new Vector2(-crossValue, 0).normalized.x);
        }

        protected virtual void HandleOnMovementEvent(Vector2 movement)
        {
            if (movement == Vector2.zero)
                _entityRenderer.SetParameter(_idleParameter, true);
            else
                _entityRenderer.SetParameter(_idleParameter, false);
            _entityMover.SetMovement(movement);
        }

        private void HandleOnDashEvent()
        {
            if (InputReader.Movement == Vector2.zero || _dashTimer < _dashCooltime) return;
            _dashTimer = 0f;
            _entityMover.Dash(InputReader.Movement);
        }

        public virtual void Join()
        {
            _entityRenderer.Appear(0.15f);
            OnJoinEvent?.Invoke();
        }

        public virtual void Quit()
        {
            _entityRenderer.Disappear(0.15f);
            OnQuitEvent?.Invoke();
        }
    }
}
