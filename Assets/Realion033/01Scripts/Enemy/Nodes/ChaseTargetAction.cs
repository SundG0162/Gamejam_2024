using BSM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.AppUI.Core;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChaseTarget", story: "[Mover] chase [Target] in [self]", category: "Action", id: "ec446077be2a671c8237b561aa495956")]
public partial class ChaseTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    protected override Status OnStart()
    {
        Vector3 direction = (Target.Value.position - Self.Value.transform.position).normalized;
        Mover.Value.CanManualMove = true;
        Mover.Value.SetMovement(direction);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

