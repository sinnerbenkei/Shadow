using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;

namespace ShadowCR.Rotations
{
    public class RedMageRotation : Rotation
    {
        RedMageActions Actions { get; } = new RedMageActions();
        #region Combat

        public override async Task<bool> Combat()
        {
            if (Shadow.Settings.RotationMode == Modes.Smart)
            {
                if (await Actions.EnchantedMoulinet()) return true;
                if (await Actions.Scatter()) return true;
                if (await Actions.Verholy()) return true;
                if (await Actions.Verflare()) return true;
                if (await Actions.EnchantedRiposte()) return true;
                if (await Actions.EnchantedZwerchhau()) return true;
                if (await Actions.EnchantedRedoublement()) return true;
                if (await Actions.Veraero()) return true;
                if (await Actions.Verthunder()) return true;
                if (await Actions.Verstone()) return true;
                if (await Actions.Verfire()) return true;
                if (await Actions.Impact()) return true;
                if (await Actions.JoltII()) return true;
                if (await Actions.Jolt()) return true;
                return await Actions.Riposte();
            }
            if (Shadow.Settings.RotationMode == Modes.Single)
            {
                if (await Actions.Verholy()) return true;
                if (await Actions.Verflare()) return true;
                if (await Actions.EnchantedRiposte()) return true;
                if (await Actions.EnchantedZwerchhau()) return true;
                if (await Actions.EnchantedRedoublement()) return true;
                if (await Actions.Veraero()) return true;
                if (await Actions.Verthunder()) return true;
                if (await Actions.Verstone()) return true;
                if (await Actions.Verfire()) return true;
                if (await Actions.Impact()) return true;
                if (await Actions.JoltII()) return true;
                if (await Actions.Jolt()) return true;
                return await Actions.Riposte();
            }
            if (Shadow.Settings.RotationMode == Modes.Multi)
            {
                if (await Actions.EnchantedMoulinet()) return true;
                if (await Actions.Scatter()) return true;
                return await Actions.Riposte();
            }
            return false;
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            if (await Actions.Embolden()) return true;
            if (await Actions.CorpsACorps()) return true;
            if (await Actions.Displacement()) return true;
            if (await Actions.Manafication()) return true;
            if (await Actions.Fleche()) return true;
            if (await Actions.ContreSixte()) return true;
            if (await Actions.Acceleration()) return true;
            if (await Actions.Swiftcast()) return true;
            return await Actions.LucidDreaming();
        }

        #endregion

        #region Heal

        public override async Task<bool> Heal()
        {
            if (await Actions.UpdateHealing()) return true;
            if (await Actions.Verraise()) return true;
            return await Actions.Vercure();
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
