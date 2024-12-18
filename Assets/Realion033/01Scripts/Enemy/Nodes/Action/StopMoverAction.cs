using BSM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopMover", story: "[Mover] is stop", category: "Action", id: "edd2e2aa115d5ad0e6fa480cad563cd8")]
public partial class StopMoverAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;

    protected override Status OnStart()
    {
        Mover.Value.StopImmediately();
        return Status.Success;
    }
}

