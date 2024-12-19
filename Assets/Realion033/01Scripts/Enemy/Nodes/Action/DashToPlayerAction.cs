using BSM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using BSM.Enemies;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DashToPlayer", story: "[Mover] and [Self] Dash to [Player] power is [dashForceMultiplier] distance is [stopDistanceThreshold]", category: "Action", id: "abb560ea8927600a789ced00ac9aa17e")]
public partial class DashToPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Player;

    [SerializeReference] public BlackboardVariable<float> dashForceMultiplier; // 대시 힘
    [SerializeReference] public BlackboardVariable<float> stopDistanceThreshold; // 플레이어 근처에서 멈출 거리

    private float currentTime = 0;

    protected override Status OnStart()
    {
        if (Mover.Value == null || Self.Value == null || Player.Value == null)
        {
            Debug.LogWarning("DashToPlayerAction: Required variables are null.");
            return Status.Failure;
        }

        Vector3 direction = (Player.Value.position - Self.Value.transform.position).normalized; // 플레이어 방향 계산
        Vector2 dashForce = direction * dashForceMultiplier; // 대시 힘 계산

        // AddForce로 대시 시작
        Mover.Value.CanManualMove = false;
        Mover.Value.AddForce(dashForce, ForceMode2D.Impulse);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        currentTime += Time.deltaTime;
        // 플레이어와의 거리를 계산
        float distanceToPlayer = Vector3.Distance(Self.Value.transform.position, Player.Value.position);

        // 플레이어 근처에 도달하면 멈춤
        if (distanceToPlayer <= stopDistanceThreshold || currentTime >= 0.6f)
        {
            Mover.Value.CanManualMove = true;
            Mover.Value.StopImmediately(); // 이동 정지
            Self.Value.GetComponent<Entity>().GetEntityComponent<DamageCast>().CastDamage();
            return Status.Success;
        }

        return Status.Running; // 아직 플레이어에 도달하지 않음
    }
}

