using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;
using ShadowCR.Spells.Role;

namespace ShadowCR.Rotations
{
    public class DarkKnightRotation : Rotation
    {
        DarkKnightActions Actions { get; } = new DarkKnightActions();

        #region Combat

        public override async Task<bool> Combat()
        {
            if (Shadow.Settings.RotationMode == Modes.Smart)
            {
                if (await Actions.FloodOfShadow()) return true;
                if (await Actions.EdgeOfShadow()) return true;
                if (await Actions.Quietus()) return true;
                if (await Actions.Quietus()) return true;
                if (await Actions.AbyssalDrain()) return true;
                if (await Actions.StalwartSoul()) return true;
                if (await Actions.Unleash()) return true;
                if (await Actions.Bloodspiller()) return true;
                if (await Actions.Souleater()) return true;
                if (await Actions.SyphonStrike()) return true;
                return await Actions.HardSlash();
            }
            if (Shadow.Settings.RotationMode == Modes.Single)
            {
                if (await Actions.EdgeOfShadow()) return true;
                if (await Actions.Bloodspiller()) return true;
                if (await Actions.Souleater()) return true;
                if (await Actions.SyphonStrike()) return true;
                return await Actions.HardSlash();
            }
            if (Shadow.Settings.RotationMode == Modes.Multi)
            {
                if (await Actions.FloodOfShadow()) return true;
                if (await Actions.Quietus()) return true;
                if (await Actions.AbyssalDrain()) return true;
                if (await Actions.StalwartSoul()) return true;
                if (await Actions.Unleash()) return true;
                if (await Actions.Souleater()) return true;
                if (await Actions.SyphonStrike()) return true;
                return await Actions.HardSlash();
            }
            return false;
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            if (await Actions.Grit()) return true;
            if (await Actions.LivingDead()) return true;
            //if (await Actions.ShadowWall()) return true;
            if (await Actions.BlackestNight()) return true;
            //if (await Actions.Delirium()) return true;
            //if (await Actions.Rampart()) return true;
            //if (await Actions.Convalescence()) return true;
            //if (await Actions.Anticipation()) return true;
            //if (await Actions.Awareness()) return true;
            //if (await Actions.Reprisal()) return true;
            if (await Actions.LivingShadow()) return true;
            //if (await Actions.BloodWeapon()) return true;
            if (await Actions.CarveAndSpit()) return true;
            return await Actions.SaltedEarth();
        }

        #endregion

        #region Heal

        public override async Task<bool> Heal()
        {
            return false;
        }

        #endregion

        #region PreCombatBuff

        public override async Task<bool> PreCombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            return await Actions.Grit();
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
