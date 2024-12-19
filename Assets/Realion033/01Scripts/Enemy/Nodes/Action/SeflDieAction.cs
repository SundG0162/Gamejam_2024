using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using BSM.Enemies;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Sefl Die", story: "[Enemy] is Die", category: "Action", id: "485b72f1510f10fb989581435a65df70")]
public partial class SeflDieAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Enemy;

    protected override Status OnStart()
    {
        Enemy.Value.HandleDeadEvt();
        return Status.Success;
    }
}

