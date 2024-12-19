using UnityEngine;

namespace BSM.Players.AttackSpeedPlayer
{
    public class AttackSpeedPlayer : Player
    {
        private AttackSpeedPlayerWeapon _weapon;

        protected override void Awake()
        {
            base.Awake();
            _weapon = GetEntityComponent<AttackSpeedPlayerWeapon>();
        }
    }
}
