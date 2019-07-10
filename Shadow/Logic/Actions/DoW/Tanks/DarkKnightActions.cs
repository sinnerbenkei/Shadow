using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.DarkKnight;

namespace ShadowCR.Rotations
{
    public class DarkKnightActions : ITankActions
    {
        public DarkKnightSpells Spellbook { get; } = new DarkKnightSpells();

        #region Damage

        public async Task<bool> HardSlash()
        {
            return await Spellbook.HardSlash.Cast();
        }

        public async Task<bool> SyphonStrike()
        {
            if (ActionManager.LastSpell.Name == Spellbook.HardSlash.Name)
            {
                return await Spellbook.SyphonStrike.Cast();
            }
            return false;
        }

        public async Task<bool> StalwartSoul()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Unleash.Name && ActionManager.HasSpell(Spellbook.StalwartSoul.Name))
            {
                return await Spellbook.StalwartSoul.Cast();
            }
            return false;
        }

        public async Task<bool> Souleater()
        {
            if (ActionManager.LastSpell.Name == Spellbook.SyphonStrike.Name)
            {
                return await Spellbook.Souleater.Cast();
            }
            return false;
        }

        public async Task<bool> Bloodspiller()
        {
            if (BloodValue >= BloodCostBloodspiller)
            {
                return await Spellbook.Bloodspiller.Cast();
            }
            return false;
        }

        #endregion

        #region AoE

        public async Task<bool> Unleash()
        {
            return await Spellbook.Unleash.Cast();
        }

        public async Task<bool> AbyssalDrain()
        {
            return await Spellbook.AbyssalDrain.Cast();
        }

        public async Task<bool> Quietus()
        {
            if (BloodValue >= BloodCostQuietus || Core.Player.HasAura(Spellbook.Delirium.Name))
            {
                return await Spellbook.Quietus.Cast();
            }
            return false;
        }

        public async Task<bool> FloodOfShadow()
        {
            if (Core.Player.CurrentMana > 6000 || HasDarkArts())
            {
                if (ActionManager.HasSpell(Spellbook.FloodOfShadow.Name))
                {
                    return await Spellbook.FloodOfShadow.Cast();
                }
                return await Spellbook.FloodOfDarkness.Cast();
            }
            return false;
        }

        public async Task<bool> EdgeOfShadow()
        {
            if (Core.Player.CurrentMana > 6000 || HasDarkArts())
            {
                if (ActionManager.HasSpell(Spellbook.EdgeOfShadow.Name))
                {
                    return await Spellbook.EdgeOfShadow.Cast();
                }
                return await Spellbook.EdgeOfDarkness.Cast();
            }
            return false;
        }

        private bool HasDarkArts()
        {
            return ActionResourceManager.CostTypesStruct.offset_C == 1;
        }

        #endregion

        #region Cooldown

        public async Task<bool> SaltedEarth()
        {
            if (!MovementManager.IsMoving)
            {
                return await Spellbook.SaltedEarth.Cast();
            }
            return false;
        }


        public async Task<bool> CarveAndSpit()
        {
            if (Core.Player.CurrentManaPercent < Shadow.Settings.DarkKnightCarveAndSpitPct)
            {
                return await Spellbook.CarveAndSpit.Cast();
            }
            return false;
        }

        #endregion

        #region Buff

        //public async Task<bool> BloodWeapon()
        //{
        //    return await Spellbook.BloodWeapon.Cast();
        //}

        public async Task<bool> LivingShadow()
        {
            if (BloodValue >= BloodCostLivingShadow)
            {
                return await Spellbook.LivingShadow.Cast();
            }
            return false;
        }

        //public async Task<bool> ShadowWall()
        //{
        //    if (Core.Player.CurrentHealthPercent < Shadow.Settings.DarkKnightShadowWallPct)
        //    {
        //        return await Spellbook.ShadowWall.Cast();
        //    }
        //    return false;
        //}

        public async Task<bool> LivingDead()
        {
            if (Core.Player.CurrentHealthPercent < Shadow.Settings.DarkKnightLivingDeadPct)
            {
                return await Spellbook.LivingDead.Cast(null, false);
            }
            return false;
        }

        //public async Task<bool> Delirium()
        //{
        //    if (Core.Player.CurrentManaPercent > 90)
        //        return false;

        //    return await Spellbook.Delirium.Cast();
        //}

        public async Task<bool> BlackestNight()
        {
            if (Core.Player.CurrentHealthPercent < Shadow.Settings.DarkKnightBlackestNightPct)
            {
                return await Spellbook.BlackestNight.Cast();
            }
            return false;
        }

        #endregion

        #region Aura

        public async Task<bool> Grit()
        {
            if (!Core.Player.HasAura(Spellbook.Grit.Name))
            {
                return await Spellbook.Grit.Cast();
            }
            return false;
        }

        #endregion

        #region Role

        //public async Task<bool> Rampart()
        //{
        //    return false;
        //}

        //public async Task<bool> Convalescence()
        //{
        //    return false;
        //}

        //public async Task<bool> Anticipation()
        //{
        //    return false;
        //}

        //public async Task<bool> Reprisal()
        //{
        //    return false;
        //}

        //public async Task<bool> Awareness()
        //{
        //    return false;
        //}

        #endregion

        #region Custom

        public static int BloodValue => Resource.BlackBlood;
        public static int BloodCostLivingShadow = 50;
        public static int BloodCostQuietus = 50;
        public static int BloodCostBloodspiller = 50;

        #endregion
    }
}
