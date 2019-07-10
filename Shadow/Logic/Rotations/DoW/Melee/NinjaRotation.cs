using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;

namespace ShadowCR.Rotations
{
    public class NinjaRotation : Rotation
    {
        NinjaActions Actions { get; } = new NinjaActions();

        #region Combat
        public override async Task<bool> Combat()
        {
            if (Shadow.Settings.RotationMode == Modes.Smart)
            {
                if (await Actions.TenChiJinBuff()) return true;
                if (await Actions.Huton()) return true;
                if (await Actions.Doton()) return true;
                if (await Actions.Katon()) return true;
                if (await Actions.Suiton()) return true;
                if (await Actions.FumaShuriken()) return true;
                if (await Actions.DeathBlossom()) return true;
                if (await Actions.DualityActive()) return true;
                if (await Actions.ShadowFang()) return true;
                if (await Actions.ArmorCrush()) return true;
                if (await Actions.AeolianEdge()) return true;
                if (await Actions.GustSlash()) return true;
                return await Actions.SpinningEdge();
            }
            if (Shadow.Settings.RotationMode == Modes.Single)
            {
                if (await Actions.TenChiJinBuff()) return true;
                if (await Actions.Huton()) return true;
                if (await Actions.Suiton()) return true;
                if (await Actions.FumaShuriken()) return true;
                if (await Actions.DualityActive()) return true;
                if (await Actions.ShadowFang()) return true;
                if (await Actions.ArmorCrush()) return true;
                if (await Actions.AeolianEdge()) return true;
                if (await Actions.GustSlash()) return true;
                return await Actions.SpinningEdge();
            }
            if (Shadow.Settings.RotationMode == Modes.Multi)
            {
                if (await Actions.TenChiJinBuff()) return true;
                if (await Actions.Huton()) return true;
                if (await Actions.Doton()) return true;
                if (await Actions.Katon()) return true;
                if (await Actions.FumaShuriken()) return true;
                if (await Actions.DeathBlossom()) return true;
                if (await Actions.DualityActive()) return true;
                if (await Actions.ShadowFang()) return true;
                if (await Actions.ArmorCrush()) return true;
                if (await Actions.AeolianEdge()) return true;
                if (await Actions.GustSlash()) return true;
                return await Actions.SpinningEdge();
            }
            return false;
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            if (await Actions.ShadeShift()) return true;
            if (await Actions.Shukuchi()) return true;
            if (await Actions.Assassinate()) return true;
            if (await Actions.Mug()) return true;
            if (await Actions.Kassatsu()) return true;
            if (await Actions.TrickAttack()) return true;
            if (await Actions.DreamWithinADream()) return true;
            if (await Actions.HellfrogMedium()) return true;
            if (await Actions.Bhavacakra()) return true;
            if (await Actions.TenChiJin()) return true;
            if (await Actions.TrueNorth()) return true;
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
            if (await Actions.Huton()) return true;
            return false;
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
