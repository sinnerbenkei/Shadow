using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;

namespace ShadowCR.Rotations
{
    public class DragoonRotation : Rotation
    {
        DragoonActions Actions { get; } = new DragoonActions();

        #region Combat

        public override async Task<bool> Combat()
        {
            if (Shadow.Settings.RotationMode == Modes.Smart)
            {
                if (await Actions.SonicThrust()) return true;
                if (await Actions.DoomSpike()) return true;
                if (await Actions.WheelingThrust()) return true;
                if (await Actions.FangAndClaw()) return true;
                if (await Actions.ChaosThrust()) return true;
                if (await Actions.Disembowel()) return true;
                if (await Actions.VorpalThrust()) return true;
                return await Actions.TrueThrust();
            }
            if (Shadow.Settings.RotationMode == Modes.Single)
            {
                if (await Actions.WheelingThrust()) return true;
                if (await Actions.FangAndClaw()) return true;
                if (await Actions.ChaosThrust()) return true;
                if (await Actions.Disembowel()) return true;
                if (await Actions.VorpalThrust()) return true;
                return await Actions.TrueThrust();
            }
            if (Shadow.Settings.RotationMode == Modes.Multi)
            {
                if (await Actions.SonicThrust()) return true;
                if (await Actions.DoomSpike()) return true;
                return false;
            }
            return false;
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            if (await Actions.BloodOfTheDragon()) return true;
            if (await Actions.DragonSight()) return true;
            if (await Actions.TrueNorth()) return true;
            if (await Actions.BattleLitany()) return true;
            if (await Actions.LifeSurge()) return true;
            if (await Actions.Nastrond()) return true;
            if (await Actions.MirageDive()) return true;
            if (await Actions.Geirskogul()) return true;
            if (await Actions.DragonfireDive()) return true;
            if (await Actions.SpineshatterDive()) return true;
            if (await Actions.Jump()) return true;
            await Helpers.UpdateParty();
            return false;
        }

        #endregion

        #region Heal

        public override async Task<bool> Heal()
        {
            if (await Actions.SecondWind()) return true;
            return await Actions.Bloodbath();
        }

        #endregion

        #region PreCombatBuff

        public override async Task<bool> PreCombatBuff()
        {
            return await Shadow.SummonChocobo();
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
