using BSM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using BSM.Enemies;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetVariable", story: "Get variable from [Entity]", category: "Action", id: "afe33d9466240d9810732da503b68e05")]
public partial class GetVariableAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Entity;

    protected override Status OnStart()
    {
        BTEnemy enemy = Entity.Value;
        GameObject playerObject = GameObject.Find("Player");

        enemy.SetVariable("Mover", enemy.GetEntityComponent<EntityMover>());
        enemy.SetVariable("Renderer", enemy.GetEntityComponent<EntityRenderer>());
        enemy.SetVariable("Coll", enemy.GetComponent<BoxCollider2D>());
        enemy.SetVariable("PlayerTrm", playerObject.transform);
        //enemy.SetVariable("AnimTriggrier", enemy.GetEntityComponent<EntityAnimatorTrigger>());

        return Status.Success;
    }
}

