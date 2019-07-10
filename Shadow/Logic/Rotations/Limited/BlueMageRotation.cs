using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowCR.Rotations
{
    public class BlueMageRotation : Rotation
    {
        public override async Task<bool> Combat()
        {
            return false;
        }

        public override async Task<bool> CombatBuff()
        {
            return false;
        }

        public override async Task<bool> Heal()
        {
            return false;
        }

        public override async Task<bool> PreCombatBuff()
        {
            return false;
        }

        public override async Task<bool> Pull()
        {
            return false;
        }

        public override async Task<bool> CombatPVP()
        {
            return false;
        }
    }
}
