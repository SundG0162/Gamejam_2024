using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Crogen.CrogenPooling;
using BSM.Projectile;
using BSM.Entities;
using BSM.Core.StatSystem;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ShotToTarget", story: "[Self] Shot the [Target] by [Bullet] and [Stat]", category: "Action", id: "ca73d8716fae7bfd60ffe804f4a6384a")]
public partial class ShotToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<GameObject> Bullet;
    [SerializeReference] public BlackboardVariable<StatElementSO> Stat;

    protected override Status OnStart()
    {
        Vector3 direction = (Target.Value.position - Self.Value.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

        Bullet.Value.Pop(PoolType.EnemyBullet, Self.Value.transform.position, rotation);
        Stat.Value = Self.Value.GetComponent<EntityStat>().GetStatElement(Stat);
        
        Stat.Value = Self.Value.GetComponent<EntityStat>().GetStatElement(Stat);
        //UnityEngine.Object.Instantiate(Bullet.Value, Self.Value.transform.position, rotation);

        return Status.Success; // 성공 상태 반환
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

