using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;

namespace ShadowCR.Rotations
{
    public class WhiteMageRotation : Rotation
    {
        WhiteMageActions Actions { get; } = new WhiteMageActions();

        #region Combat

        public override async Task<bool> Combat()
        {
            if (Shadow.Settings.RotationMode == Modes.Smart)
            {
                if (await Actions.Holy()) return true;
                if (await Actions.AeroII()) return true;
                if (await Actions.Aero()) return true;
                if (await Actions.StoneIV()) return true;
                if (await Actions.StoneIII()) return true;
                if (await Actions.StoneII()) return true;
                return await Actions.Stone();
            }
            if (Shadow.Settings.RotationMode == Modes.Single)
            {
                if (await Actions.AeroII()) return true;
                if (await Actions.Aero()) return true;
                if (await Actions.StoneIV()) return true;
                if (await Actions.StoneIII()) return true;
                if (await Actions.StoneII()) return true;
                return await Actions.Stone();
            }
            if (Shadow.Settings.RotationMode == Modes.Multi)
            {
                return await Actions.Holy();
            }
            return false;
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            if (await Actions.LucidDreaming()) return true;
            return false;
        }

        #endregion

        #region Heal

        public override async Task<bool> Heal()
        {
            if (await Actions.UpdateHealing()) return true;
            if (await Actions.StopCasting()) return true;
            if (await Actions.Benediction()) return true;
            if (await Actions.Tetragrammaton()) return true;
            if (await Actions.PresenceOfMind()) return true;
            if (await Actions.PlenaryIndulgence()) return true;
            if (await Actions.Assize()) return true;
            if (await Actions.MedicaII()) return true;
            if (await Actions.Medica()) return true;
            if (await Actions.CureII()) return true;
            if (await Actions.Cure()) return true;
            if (await Actions.Regen()) return true;
            if (await Actions.Raise()) return true;
            if (await Actions.Esuna()) return true;
            return false;
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
            if (await Actions.AeroII()) return true;
            if (await Actions.Aero()) return true;
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
