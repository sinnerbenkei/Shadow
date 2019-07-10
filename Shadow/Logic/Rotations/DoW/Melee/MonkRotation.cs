using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;

namespace ShadowCR.Rotations
{
    public class MonkRotation : Rotation
    {
        MonkActions Actions { get; } = new MonkActions();

        #region Combat
        public override async Task<bool> Combat()
        {
            if (Shadow.Settings.RotationMode == Modes.Smart)
            {
                if (await Actions.Rockbreaker()) return true;
                if (await Actions.Demolish()) return true;
                if (await Actions.SnapPunch()) return true;
                if (await Actions.DragonKick()) return true;
                if (await Actions.TwinSnakes()) return true;
                if (await Actions.TrueStrike()) return true;
                return await Actions.Bootshine();
            }
            if (Shadow.Settings.RotationMode == Modes.Single)
            {
                if (await Actions.Demolish()) return true;
                if (await Actions.SnapPunch()) return true;
                if (await Actions.DragonKick()) return true;
                if (await Actions.TwinSnakes()) return true;
                if (await Actions.TrueStrike()) return true;
                return await Actions.Bootshine();
            }
            if (Shadow.Settings.RotationMode == Modes.Multi)
            {
                if (await Actions.Rockbreaker()) return true;
                if (await Actions.TwinSnakes()) return true;
                if (await Actions.TrueStrike()) return true;
                if (await Actions.DragonKick()) return true;
                return await Actions.Bootshine();
            }
            return false;
        }

#endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            if (await Actions.FistsOfFire()) return true;
            if (await Actions.FistsOfWind()) return true;
            if (await Actions.FistsOfEarth()) return true;
            if (await Actions.ShoulderTackle()) return true;
            if (await Actions.PerfectBalance()) return true;
            if (await Actions.TrueNorth()) return true;
            if (await Actions.RiddleOfFire()) return true;
            if (await Actions.ForbiddenChakra()) return true;
            if (await Actions.Brotherhood()) return true;
            if (await Actions.ElixirField()) return true;
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
            if (await Shadow.SummonChocobo()) return true;
            if (await Actions.Meditation()) return true;
            if (await Actions.FormShift()) return true;
            if (await Actions.FistsOfFire()) return true;
            if (await Actions.FistsOfWind()) return true;
            return await Actions.FistsOfEarth();
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
