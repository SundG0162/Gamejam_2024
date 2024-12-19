using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetColliderTrigger", story: "[Self] [Collider] isTrigger [SetTrigger]", category: "Action", id: "dcc739e88d0563bdf33cdf074d6e15f9")]
public partial class SetColliderTriggerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<BoxCollider2D> Collider;
    [SerializeReference] public BlackboardVariable<bool> SetTrigger;

    protected override Status OnStart()
    {
        Collider.Value.isTrigger = SetTrigger;
        return Status.Success;
    }
}

