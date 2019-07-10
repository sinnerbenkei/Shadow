using ShadowCR.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowCR.Rotations
{
    public class WarriorRotation : Rotation
    {
        WarriorActions Actions { get; } = new WarriorActions();

        #region Combat

        public override async Task<bool> Combat()
        {
            if (Shadow.Settings.RotationMode == Modes.Smart)
            {
                if (await Actions.Decimate()) return true;
                if (await Actions.SteelCyclone()) return true;
                if (await Actions.FellCleave()) return true;
                if (await Actions.InnerBeast()) return true;
                if (await Actions.Overpower()) return true;
                if (await Actions.StormsEye()) return true;
                if (await Actions.StormsPath()) return true;
                if (await Actions.Maim()) return true;
                return await Actions.HeavySwing();
            }
            if (Shadow.Settings.RotationMode == Modes.Single)
            {
                if (await Actions.FellCleave()) return true;
                if (await Actions.InnerBeast()) return true;
                if (await Actions.StormsEye()) return true;
                if (await Actions.StormsPath()) return true;
                if (await Actions.Maim()) return true;
                return await Actions.HeavySwing();
            }
            if (Shadow.Settings.RotationMode == Modes.Multi)
            {
                if (await Actions.Decimate()) return true;
                if (await Actions.SteelCyclone()) return true;
                if (await Actions.Overpower()) return true;
                if (await Actions.StormsEye()) return true;
                if (await Actions.StormsPath()) return true;
                if (await Actions.Maim()) return true;
                return await Actions.HeavySwing();
            }
            return false;
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            if (await Actions.Defiance()) return true;
            if (await Actions.ThrillOfBattle()) return true;
            if (await Actions.Vengeance()) return true;
            if (await Actions.ShakeItOff()) return true;
            if (await Actions.Rampart()) return true;
            if (await Actions.Reprisal()) return true;
            if (await Actions.Onslaught()) return true;
            if (await Actions.EquilibriumTP()) return true;
            if (await Actions.InnerRelease()) return true;
            if (await Actions.Berserk()) return true;
            if (await Actions.Upheaval()) return true;
            return await Actions.Infuriate();
        }

        #endregion

        #region Heal

        public override async Task<bool> Heal()
        {
            return await Actions.Equilibrium();
        }

        #endregion

        #region PreCombatBuff

        public override async Task<bool> PreCombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            return await Actions.Defiance();
        }

        #endregion

        #region Pull

        public override async Task<bool> Pull()
        {
            return await Combat();
        }

        #endregion

        #region CombatPVP

        public override async Task<bool> CombatPVP()
        {
            return false;
        }

        #endregion
    }
}
