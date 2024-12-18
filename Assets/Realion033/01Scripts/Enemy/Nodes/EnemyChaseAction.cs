using BSM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EnemyChase", story: "[Mover] to [Player] at [Speed]", category: "Action", id: "fd430ef521fb9b152e8a5b3db5459683")]
public partial class EnemyChaseAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;
    [SerializeReference] public BlackboardVariable<Transform> Player;
    [SerializeReference] public BlackboardVariable<float> Speed;

    private float _lastChaseTime;

    protected override Status OnStart()
    {
        _lastChaseTime = Time.time;
        //Mover.Value._moveSpeed = Speed;
        return base.OnStart();
    }

    protected override Status OnUpdate()
    {
        if (Player.Value == null)
        {
            return Status.Failure;
        }

        if (_lastChaseTime + 0.1f > Time.time)
        {
            return Status.Running;
        }

        _lastChaseTime = Time.time;

        // Vector2로 방향 벡터를 계산
        Vector2 dir = (Vector2)(Player.Value.transform.position - Mover.Value.transform.position);
        Vector2 normalizedDir = dir.normalized;

        // 이동 방향의 x와 y 값을 설정
        Mover.Value.SetMovement(normalizedDir);
        // Renderer.Value.FlipController(normalizedDir.x);
        return Status.Running;

    }
}

