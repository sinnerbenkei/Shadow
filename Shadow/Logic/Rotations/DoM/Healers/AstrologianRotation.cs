using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;
using ShadowCR.Settings;

namespace ShadowCR.Rotations
{
    public class AstrologianRotation : Rotation
    {
        AstrologianActions Actions { get; } = new AstrologianActions();
        #region Combat

        public override async Task<bool> Combat()
        {
            if (Shadow.Settings.RotationMode == Modes.Smart)
            {
                if (await Actions.StellarDetonation()) return true;
                if (await Actions.EarthlyStar()) return true;
                if (await Actions.Gravity()) return true;
                if (await Actions.CombustII()) return true;
                if (await Actions.Combust()) return true;
                if (await Actions.MaleficIII()) return true;
                if (await Actions.MaleficII()) return true;
                return await Actions.Malefic();
            }
            if (Shadow.Settings.RotationMode == Modes.Single)
            {
                if (await Actions.CombustII()) return true;
                if (await Actions.Combust()) return true;
                if (await Actions.MaleficIII()) return true;
                if (await Actions.MaleficII()) return true;
                return await Actions.Malefic();
            }
            if (Shadow.Settings.RotationMode == Modes.Multi)
            {
                if (await Actions.StellarDetonation()) return true;
                if (await Actions.EarthlyStar()) return true;
                if (await Actions.Gravity()) return true;
                if (await Actions.CombustII()) return true;
                return await Actions.Combust();
            }
            return false;
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            if (await Actions.CelestialOpposition()) return true;
            if (await Actions.LucidDreaming()) return true;
            if (Shadow.Settings.AstrologianDraw)
            {
                if (await Actions.LordOfCrowns()) return true;
                if (await Actions.SleeveDraw()) return true;
                if (await Actions.Draw()) return true;
                if (await Actions.Redraw()) return true;
                if (await Actions.MinorArcana()) return true;
                if (await Actions.Undraw()) return true;
                if (await Actions.DrawTargetted()) return true;
            }
            return false;
        }

        #endregion

        #region Heal

        public override async Task<bool> Heal()
        {
            if (await Actions.UpdateHealing()) return true;
            if (await Actions.StopCasting()) return true;
            if (await Actions.EssentialDignity()) return true;
            if (await Actions.Lightspeed()) return true;
            if (await Actions.Synastry()) return true;
            if (await Actions.LadyOfCrowns()) return true;
            if (await Actions.AspectedHelios()) return true;
            if (await Actions.Helios()) return true;
            if (await Actions.BeneficII()) return true;
            if (await Actions.Benefic()) return true;
            if (await Actions.AspectedBenefic()) return true;
            if (await Actions.Ascend()) return true;
            if (await Actions.Esuna()) return true;
            return false;
        }

        #endregion

        #region PreCombatBuff

        public override async Task<bool> PreCombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Actions.NocturnalSect()) return true;
            if (await Actions.DiurnalSect()) return true;
            if (Shadow.Settings.AstrologianDraw && Shadow.Settings.AstrologianCardPreCombat)
            {
                if (await Actions.Draw()) return true;
                if (await Actions.Redraw()) return true;
                if (await Actions.MinorArcana()) return true;
                if (await Actions.Undraw()) return true;
            }
            return false;
        }

        #endregion

        #region Pull

        public override async Task<bool> Pull()
        {
            if (await Actions.CombustII()) return true;
            if (await Actions.Combust()) return true;
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
