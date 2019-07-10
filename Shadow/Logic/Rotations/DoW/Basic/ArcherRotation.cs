using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;

namespace ShadowCR.Rotations
{
    public class ArcherRotation : Rotation
    {
        ArcherActions Actions { get; } = new ArcherActions();

        public override async Task<bool> Combat()
        {
            if (Shadow.Settings.RotationMode == Modes.Smart)
            {

            }
            if (Shadow.Settings.RotationMode == Modes.Single)
            {

            }
            if (Shadow.Settings.RotationMode == Modes.Multi)
            {

            }
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
