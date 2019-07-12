using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;
using ShadowCR.Spells.Role;

namespace ShadowCR.Rotations
{
    public class GunbreakerRotation : Rotation
    {
        GunbreakerActions Actions { get; } = new GunbreakerActions();

        #region Combat

        public override async Task<bool> Combat()
        {
            if (Shadow.Settings.RotationMode == Modes.Smart)
            {
                if (await Actions.BowShock()) return true;
                if (await Actions.DemonSlaughter()) return true;
                if (await Actions.DemonSlice()) return true;

                if (await Actions.Continuation()) return true;
                if (await Actions.JugularRip()) return true;
                if (await Actions.AbdomenTear()) return true;
                if (await Actions.EyeGouge()) return true;

                if (await Actions.SonicBreak()) return true;
                if (await Actions.WickedTalon()) return true;
                if (await Actions.SavageClaw()) return true;
                if (await Actions.GnashingFang()) return true;
                if (await Actions.BurstStrike()) return true;
                if (await Actions.SolidBarrel()) return true;
                if (await Actions.BrutalShell()) return true;
                if (await Actions.DangerZone()) return true;
                return await Actions.KeenEdge();
            }
            if (Shadow.Settings.RotationMode == Modes.Single)
            {
                if (await Actions.Continuation()) return true;
                if (await Actions.JugularRip()) return true;
                if (await Actions.AbdomenTear()) return true;
                if (await Actions.EyeGouge()) return true;
                if (await Actions.BowShock()) return true;

                if (await Actions.SonicBreak()) return true;
                if (await Actions.WickedTalon()) return true;
                if (await Actions.SavageClaw()) return true;
                if (await Actions.GnashingFang()) return true;
                if (await Actions.BurstStrike()) return true;
                if (await Actions.SolidBarrel()) return true;
                if (await Actions.BrutalShell()) return true;
                if (await Actions.DangerZone()) return true;
                return await Actions.KeenEdge();
            }
            if (Shadow.Settings.RotationMode == Modes.Multi)
            {
                if (await Actions.BowShock()) return true;
                if (await Actions.DemonSlaughter()) return true;
                if (await Actions.DemonSlice()) return true;
                return await Actions.KeenEdge();
            }
            return false;
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            //if (await Actions.Superbolide()) return true;
            //if (await Actions.Camouflage()) return true;
            //if (await Actions.Nebula()) return true;
            //if (await Actions.HeartOfLight()) return true;
            //if (await Actions.HeartOfStone()) return true;
            if (await Actions.Rampart()) return true;
            if (await Actions.Convalescence()) return true;
            if (await Actions.Anticipation()) return true;
            if (await Actions.Awareness()) return true;
            if (await Actions.Reprisal()) return true;
            //if (await Actions.Aurora()) return true;
            return await Actions.RoyalGuard();
        }

        #endregion

        #region Heal

        public override async Task<bool> Heal()
        {
            return false;
        }

        #endregion

        #region PreCombatBuff

        public override async Task<bool> PreCombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            return await Actions.RoyalGuard();
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
