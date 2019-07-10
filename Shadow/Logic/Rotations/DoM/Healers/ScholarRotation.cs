using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;

namespace ShadowCR.Rotations
{
    public class ScholarRotation : Rotation
    {
        ScholarActions Actions { get; } = new ScholarActions();

        #region Combat

        public override async Task<bool> Combat()
        {
            if (await Actions.BioII()) return true;
            if (await Actions.Miasma()) return true;
            if (await Actions.Bio()) return true;
            if (await Actions.BroilII()) return true;
            if (await Actions.Broil()) return true;
            return await Actions.Ruin();
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            if (await Actions.SummonII()) return true;
            if (await Actions.Summon()) return true;
            if (await Actions.LucidDreaming()) return true;
            if (await Actions.ChainStrategem()) return true;
            if (await Actions.Bane()) return true;
            return false;
        }

        #endregion

        #region Heal

        public override async Task<bool> Heal()
        {
            if (await Actions.UpdateHealing()) return true;
            if (await Actions.StopCasting()) return true;
            if (await Actions.Lustrate()) return true;
            if (await Actions.Aetherpact()) return true;
            if (await Actions.Indomitability()) return true;
            if (await Actions.Succor()) return true;
            if (await Actions.Adloquium()) return true;
            if (await Actions.Excogitation()) return true;
            if (await Actions.Physick()) return true;
            if (await Actions.Resurrection()) return true;
            if (await Actions.Esuna()) return true;
            return false;
        }

        #endregion

        #region PreCombatBuff

        public override async Task<bool> PreCombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Actions.SummonII()) return true;
            return await Actions.Summon();
        }

        #endregion

        #region Pull

        public override async Task<bool> Pull()
        {
            if (await Actions.BioII()) return true;
            if (await Actions.Bio()) return true;
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
