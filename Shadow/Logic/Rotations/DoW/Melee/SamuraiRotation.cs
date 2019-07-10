using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;

namespace ShadowCR.Rotations
{
    public class SamuraiRotation : Rotation
    {
        SamuraiActions Actions { get; } = new SamuraiActions();

        #region Combat

        public override async Task<bool> Combat()
        {
            if (Shadow.Settings.RotationMode == Modes.Smart)
            {
                if (await Actions.MidareSetsugekka()) return true;
                if (await Actions.TenkaGoken()) return true;
                if (await Actions.Higanbana()) return true;
                //if (await Actions.Kaiten()) return true;
                if (await Actions.Meikyo()) return true;
                if (await Actions.Kasha()) return true;
                if (await Actions.Gekko()) return true;
                if (await Actions.ShifuBuff()) return true;
                if (await Actions.JinpuBuff()) return true;
                if (await Actions.Mangetsu()) return true;
                if (await Actions.Oka()) return true;
                if (await Actions.Fuga()) return true;
                if (await Actions.YukikazeDebuff()) return true;
                if (await Actions.Shifu()) return true;
                if (await Actions.Jinpu()) return true;
                if (await Actions.Yukikaze()) return true;
                if (await Actions.Enpi()) return true;
                return await Actions.Hakaze();
            }
            if (Shadow.Settings.RotationMode == Modes.Single)
            {
                if (await Actions.MidareSetsugekka()) return true;
                if (await Actions.Higanbana()) return true;
                //if (await Actions.Kaiten()) return true;
                if (await Actions.Meikyo()) return true;
                if (await Actions.Kasha()) return true;
                if (await Actions.Gekko()) return true;
                if (await Actions.ShifuBuff()) return true;
                if (await Actions.JinpuBuff()) return true;
                if (await Actions.YukikazeDebuff()) return true;
                if (await Actions.Shifu()) return true;
                if (await Actions.Jinpu()) return true;
                if (await Actions.Yukikaze()) return true;
                if (await Actions.Enpi()) return true;
                return await Actions.Hakaze();
            }
            if (Shadow.Settings.RotationMode == Modes.Multi)
            {
                if (await Actions.MidareSetsugekka()) return true;
                if (await Actions.TenkaGoken()) return true;
                //if (await Actions.Kaiten()) return true;
                if (await Actions.Meikyo()) return true;
                if (await Actions.Kasha()) return true;
                if (await Actions.Gekko()) return true;
                if (await Actions.ShifuBuff()) return true;
                if (await Actions.JinpuBuff()) return true;
                if (await Actions.Mangetsu()) return true;
                if (await Actions.Oka()) return true;
                if (await Actions.Fuga()) return true;
                if (await Actions.Enpi()) return true;
                return await Actions.Hakaze();
            }
            return false;
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            if (await Actions.Meditate()) return true;
            if (await Actions.HissatsuKaiten()) return true;
            if (await Actions.HissatsuGyoten()) return true;
            if (await Actions.TrueNorth()) return true;
            if (await Actions.MeikyoShisui()) return true;
            if (await Actions.HissatsuGuren()) return true;
            if (await Actions.HissatsuKyuten()) return true;
            if (await Actions.HissatsuSeigan()) return true;
            if (await Actions.HissatsuShinten()) return true;
            await Helpers.UpdateParty();
            return false;
        }

        #endregion

        #region Heal

        public override async Task<bool> Heal()
        {
            if (await Actions.SecondWind()) return true;
            if (await Actions.MercifulEyes()) return true;
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
            if (await Actions.HissatsuGyoten()) return true;
            if (await Actions.Enpi()) return true;
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
