using BSM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopRange", story: "if [Player] and [Self] in [Range] Stop [Mover]", category: "Action", id: "fbd125a415a1135b8fb692b12929dd53")]
public partial class StopRangeAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Player;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> Range;
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;

    protected override Status OnStart()
    {
        //Check Distance
        float distance = Vector3.Distance(Self.Value.transform.position, Player.Value.position);
        bool isInRange = distance < Range.Value;

        //return Status according to distance Value
        return isInRange ? Status.Success : Status.Failure;
    }
}

