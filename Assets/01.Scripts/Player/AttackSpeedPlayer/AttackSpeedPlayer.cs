using UnityEngine;

namespace BSM.Players.AttackSpeedPlayer
{
    public class AttackSpeedPlayer : Player
    {
        private AttackSpeedWeapon _weapon;

        protected override void Awake()
        {
            base.Awake();
            _weapon = GetEntityComponent<AttackSpeedWeapon>();
        }
    }
}
