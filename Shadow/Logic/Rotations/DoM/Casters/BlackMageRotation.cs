using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Settings;

namespace ShadowCR.Rotations
{
    public class BlackMageRotation : Rotation
    {
        BlackMageActions Actions { get; } = new BlackMageActions();

        #region Combat

        public override async Task<bool> Combat()
        {
            if (Shadow.Settings.RotationMode == Modes.Multi || Shadow.Settings.RotationMode == Modes.Smart &&
                Helpers.EnemiesNearTarget(5) > 2)
            {
                return await Multi();
            }
            return await Single();
        }

        private async Task<bool> Single()
        {
            if (await Actions.Transpose()) return true;
            if (await Actions.Triplecast()) return true;
            if (await Actions.Swiftcast()) return true;
            if (await Actions.Sharpcast()) return true;
            if (await Actions.Thundercloud()) return true;
            if (await Actions.Foul()) return true;
            if (await Actions.ThunderIII()) return true;
            if (await Actions.Thunder()) return true;
            if (await Actions.BlizzardIV()) return true;
            if (await Actions.FireIV()) return true;
            if (await Actions.FireIII()) return true;
            if (await Actions.Fire()) return true;
            if (await Actions.BlizzardIII()) return true;
            if (await Actions.Blizzard()) return true;
            return await Actions.Scathe();
        }

        private async Task<bool> Multi()
        {
            if (await Actions.Foul()) return true;
            if (await Actions.ThunderIV()) return true;
            if (await Actions.ThunderII()) return true;
            if (await Actions.BlizzardIV()) return true;
            if (await Actions.FireIIIMulti()) return true;
            if (await Actions.Flare()) return true;
            if (await Actions.FireII()) return true;
            if (await Actions.BlizzardIII()) return true;
            if (await Actions.TransposeMulti()) return true;
            if (await Actions.FireMulti()) return true;
            return await Actions.BlizzardMulti();
        }

        #endregion

        #region CombatBuff

        public override async Task<bool> CombatBuff()
        {
            if (await Shadow.SummonChocobo()) return true;
            if (await Shadow.ChocoboStance()) return true;
            if (await Actions.Convert()) return true;
            if (await Actions.Enochian()) return true;
            if (await Actions.LeyLines()) return true;
            return await Actions.LucidDreaming();
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
