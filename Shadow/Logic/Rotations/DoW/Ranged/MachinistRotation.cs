using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;

namespace ShadowCR.Rotations
{
    public class MachinistRotation : Rotation
    {
        MachinistActions Actions { get; } = new MachinistActions();

        #region Combat

        public override async Task<bool> Combat()
        {
            if (await Actions.HotShot()) return true;
            if (await Actions.Flamethrower()) return true;
            if (await Actions.SpreadShot()) return true;
            if (await Actions.CleanShot()) return true;
            if (await Actions.SlugShot()) return true;
            return await Actions.SplitShot();
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            if (await Actions.FlamethrowerBuff()) return true;
            if (await Actions.BarrelStabilizer()) return true;
            if (await Actions.RookAutoturret()) return true;
            if (await Actions.RookOverdrive()) return true;
            if (await Actions.Hypercharge()) return true;
            if (await Actions.GaussRound()) return true;
            if (await Actions.Wildfire()) return true;
            if (await Actions.Reassemble()) return true;
            if (await Actions.Ricochet()) return true;
            // Role
            await Helpers.UpdateParty();
            return false;
        }

        #endregion

        #region Heal

        public override async Task<bool> Heal()
        {
            return await Actions.SecondWind();
        }

        #endregion

        #region PreCombatBuff

        public override async Task<bool> PreCombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            return await Actions.Peloton();
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
