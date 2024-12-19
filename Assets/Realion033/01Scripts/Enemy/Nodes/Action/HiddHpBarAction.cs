using BSM.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "HiddHPBar", story: "Hide [Enemy] Bar [isHide]", category: "Action", id: "007502830dcae0d891dfd42bac5153e8")]
public partial class HiddHpBarAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Enemy;
    [SerializeReference] public BlackboardVariable<bool> isHide;

    protected override Status OnStart()
    {
        Enemy.Value._hpBar.SetActive(isHide);
        return Status.Success;
    }
}

