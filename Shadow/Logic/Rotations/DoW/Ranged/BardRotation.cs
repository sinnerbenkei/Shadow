using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;

namespace ShadowCR.Rotations
{
    public class BardRotation : Rotation
    {
        BardActions Actions { get; } = new BardActions();

        #region Combat

        public override async Task<bool> Combat()
        {
            if (await Actions.BarrageActive()) return true;
            if (await Actions.DotSnapshot()) return true;
            if (await Actions.IronJaws()) return true;
            if (await Actions.RefulgentArrow()) return true;
            if (await Actions.StraightShotBuff()) return true;
            if (await Actions.QuickNock()) return true;
            if (await Actions.Windbite()) return true;
            if (await Actions.VenomousBite()) return true;
            if (await Actions.StraightShot()) return true;
            return await Actions.HeavyShot();
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            // Songs
            if (await Actions.WanderersMinuet()) return true;
            if (await Actions.MagesBallad()) return true;
            if (await Actions.ArmysPaeon()) return true;
            // Buffs
            if (await Actions.BattleVoice()) return true;
            if (await Actions.RagingStrikes()) return true;
            if (await Actions.Barrage()) return true;
            // Off-GCDs
            if (await Actions.PitchPerfect()) return true;
            if (await Actions.RainOfDeath()) return true;
            if (await Actions.Bloodletter()) return true;
            if (await Actions.EmpyrealArrow()) return true;
            if (await Actions.Sidewinder()) return true;
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
