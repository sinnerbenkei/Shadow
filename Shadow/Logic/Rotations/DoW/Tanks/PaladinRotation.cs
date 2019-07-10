using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;

namespace ShadowCR.Rotations
{
    public class PaladinRoatation : Rotation
    {
        PaladinActions Actions { get; } = new PaladinActions();

        #region Combat

        public override async Task<bool> Combat()
        {
            if (Shadow.Settings.RotationMode == Modes.Smart)
            {
                if (await Actions.TotalEclipse()) return true;
                if (await Actions.HolySpirit()) return true;
                if (await Actions.GoringBlade()) return true;
                if (await Actions.RoyalAuthority()) return true;
                if (await Actions.RiotBlade()) return true;
                return await Actions.FastBlade();
            }
            if (Shadow.Settings.RotationMode == Modes.Single)
            {
                if (await Actions.HolySpirit()) return true;
                if (await Actions.GoringBlade()) return true;
                if (await Actions.RoyalAuthority()) return true;
                if (await Actions.RiotBlade()) return true;
                return await Actions.FastBlade();
            }
            if (Shadow.Settings.RotationMode == Modes.Multi)
            {
                if (await Actions.TotalEclipse()) return true;
            }
            return false;
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            if (await Actions.PassageOfArms()) return true;
            if (await Actions.IronWill()) return true;
            if (await Actions.HallowedGround()) return true;
            if (await Actions.Sentinel()) return true;
            if (await Actions.Rampart()) return true;
            if (await Actions.Reprisal()) return true;
            if (await Actions.Sheltron()) return true;
            if (await Actions.Requiescat()) return true;
            if (await Actions.FightOrFlight()) return true;
            if (await Actions.CircleOfScorn()) return true;
            return await Actions.SpiritsWithin();
        }

        #endregion

        #region Heal

        public override async Task<bool> Heal()
        {
            return await Actions.Clemency();
        }

        #endregion

        #region PreCombatBuff

        public override async Task<bool> PreCombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            return await Actions.IronWill();
        }

        #endregion

        #region Pull

        public override async Task<bool> Pull()
        {
            if (await Actions.ShieldLob()) return true;
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
