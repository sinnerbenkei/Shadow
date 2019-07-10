using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;

namespace ShadowCR.Rotations
{
    public class SummonerRotation : Rotation
    {
        SummonerActions Actions { get; } = new SummonerActions();

        #region Combat

        public override async Task<bool> Combat()
        {
            if (await Actions.MiasmaIII()) return true;
            if (await Actions.Miasma()) return true;
            if (await Actions.BioIII()) return true;
            if (await Actions.BioII()) return true;
            if (await Actions.Bio()) return true;
            if (await Actions.RuinII()) return true;
            if (await Actions.RuinIII()) return true;
            return await Actions.Ruin();
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            if (await Actions.Sic()) return true;
            if (await Actions.SummonIII()) return true;
            if (await Actions.SummonII()) return true;
            if (await Actions.Summon()) return true;
            if (await Actions.EnkindleBahamut()) return true;
            if (await Actions.SummonBahamut()) return true;
            if (await Actions.Deathflare()) return true;
            if (await Actions.DreadwyrmTrance()) return true;
            if (await Actions.TriDisaster()) return true;
            if (await Actions.Bane()) return true;
            if (await Actions.Painflare()) return true;
            if (await Actions.Fester()) return true;
            if (await Actions.Enkindle()) return true;
            if (await Actions.Aetherpact()) return true;
            if (await Actions.Addle()) return true;
            return await Actions.LucidDreaming();
        }

        #endregion

        #region Heal

        public override async Task<bool> Heal()
        {
            if (await Actions.UpdateHealing()) return true;
            if (await Actions.Resurrection()) return true;
            return await Actions.Physick();
        }

        #endregion

        #region PreCombatBuff

        public override async Task<bool> PreCombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Actions.SummonIII()) return true;
            if (await Actions.SummonII()) return true;
            if (await Actions.Summon()) return true;
            return await Actions.Obey();
        }

        #endregion

        #region Pull

        public override async Task<bool> Pull()
        {
            if (await Actions.TriDisaster()) return true;
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
