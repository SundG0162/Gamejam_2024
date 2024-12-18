using BSM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FilpTargetDir", story: "[Mover] Filp [Target] [Renderer]", category: "Action", id: "099dcf9312648ea7a6340f701da02960")]
public partial class FilpTargetDirAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<EntityRenderer> Renderer;

    protected override Status OnStart()
    {
        Vector3 dir = Target.Value.position - Mover.Value.transform.position;
        float movementX = Mathf.Sign(dir.x);
        Renderer.Value.Flip(movementX);

        return Status.Success;
    }
}

