using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.RedMage;
using static ShadowCR.Constants;

namespace ShadowCR.Rotations
{
    public class RedMageActions : ICasterActions
    {
        public RedMageSpells Spellbook { get; } = new RedMageSpells();

        #region Damage

        public async Task<bool> Riposte()
        {
            if (!ActionManager.HasSpell(Spellbook.Jolt.Name))
            {
                return await Spellbook.Riposte.Cast();
            }
            return false;
        }

        public async Task<bool> Jolt()
        {
            if (!ActionManager.HasSpell(Spellbook.JoltII.Name))
            {
                return await Spellbook.Jolt.Cast();
            }
            return false;
        }

        public async Task<bool> JoltII()
        {
            return await Spellbook.JoltII.Cast();
        }

        public async Task<bool> Impact()
        {
            return await Spellbook.Impact.Cast();
        }

        public async Task<bool> Verthunder()
        {
            if (Core.Player.HasAura("Dualcast") || Core.Player.HasAura("Swiftcast"))
            {
                return await Spellbook.Verthunder.Cast();
            }
            return false;
        }

        public async Task<bool> Veraero()
        {
            if ((Core.Player.HasAura("Dualcast") || Core.Player.HasAura("Swiftcast")) && BlackMana > WhiteMana)
            {
                return await Spellbook.Veraero.Cast();
            }
            return false;
        }

        public async Task<bool> Verfire()
        {
            return await Spellbook.Verfire.Cast();
        }

        public async Task<bool> Verstone()
        {
            return await Spellbook.Verstone.Cast();
        }

        public async Task<bool> EnchantedRiposte()
        {
            if (WhiteMana >= 80 && BlackMana >= 80 && Core.Player.TargetDistance(5, false))
            {
                return await Spellbook.EnchantedRiposte.Cast();
            }
            return false;
        }

        public async Task<bool> EnchantedZwerchhau()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Riposte.Name && Core.Player.TargetDistance(5, false))
            {
                return await Spellbook.EnchantedZwerchhau.Cast();
            }
            return false;
        }

        public async Task<bool> EnchantedRedoublement()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Zwerchhau.Name && Core.Player.TargetDistance(5, false))
            {
                return await Spellbook.EnchantedRedoublement.Cast();
            }
            return false;
        }

        public async Task<bool> Verflare()
        {
            return await Spellbook.Verflare.Cast();
        }

        public async Task<bool> Verholy()
        {
            if (BlackMana >= WhiteMana)
            {
                return await Spellbook.Verholy.Cast();
            }
            return false;
        }

        #endregion

        #region AoE

        public async Task<bool> Scatter()
        {
            return await Spellbook.Scatter.Cast();
        }

        public async Task<bool> EnchantedMoulinet()
        {
            if (WhiteMana >= 30 && BlackMana >= 30)
            {
                return await Spellbook.EnchantedMoulinet.Cast();
            }
            return false;
        }

        #endregion

        #region Cooldown

        public async Task<bool> CorpsACorps()
        {
            if (Shadow.Settings.RedMageCorpsACorps)
            {
                if (!MovementManager.IsMoving && WhiteMana >= 80 && BlackMana >= 80 && Core.Player.TargetDistance(5))
                {
                    return await Spellbook.CorpsACorps.Cast(null, false);
                }
            }
            return false;
        }

        public async Task<bool> Displacement()
        {
            if (Shadow.Settings.RedMageDisplacement)
            {
                if (ActionManager.LastSpell.Name == Spellbook.EnchantedRedoublement.Name)
                {
                    return await Spellbook.Displacement.Cast(null, false);
                }
            }
            return false;
        }

        public async Task<bool> Fleche()
        {
            if (UseOffGCD)
            {
                return await Spellbook.Fleche.Cast();
            }
            return false;
        }

        public async Task<bool> ContreSixte()
        {
            if (UseOffGCD)
            {
                return await Spellbook.ContreSixte.Cast();
            }
            return false;
        }

        #endregion

        #region Buff

        public async Task<bool> Acceleration()
        {
            if (UseOffGCD)
            {
                var count = Shadow.Settings.CustomAoE ? Shadow.Settings.CustomAoECount : 3;

                if (Shadow.Settings.RotationMode == Modes.Single || Shadow.Settings.RotationMode == Modes.Smart &&
                    Helpers.EnemiesNearTarget(5) < count)
                {
                    return await Spellbook.Acceleration.Cast();
                }
            }
            return false;
        }

        public async Task<bool> Embolden()
        {
            if (Shadow.Settings.RedMageEmbolden && Shadow.LastSpell.Name == Spellbook.EnchantedRiposte.Name)
            {
                return await Spellbook.Embolden.Cast(null, false);
            }
            return false;
        }

        public async Task<bool> Manafication()
        {
            if (Shadow.Settings.RedMageManafication)
            {
                if (UseOffGCD && WhiteMana >= 40 && WhiteMana < 60 && BlackMana >= 40 && BlackMana < 60)
                {
                    return await Spellbook.Manafication.Cast();
                }
            }
            return false;
        }

        #endregion

        #region Heal

        public async Task<bool> UpdateHealing()
        {
            if (Shadow.Settings.RedMageVerraise)
            {
                if (!await Helpers.UpdateHealManager())
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> Vercure()
        {
            if (Shadow.Settings.RedMageVercure && Core.Player.CurrentHealthPercent < Shadow.Settings.RedMageVercurePct)
            {
                var target = Core.Player;

                if (target != null)
                {
                    return await Spellbook.Vercure.Cast(target);
                }
            }
            return false;
        }

        public async Task<bool> Verraise()
        {
            if (Shadow.Settings.RedMageVerraise && Core.Player.CurrentManaPercent > 50)
            {
                if (Core.Player.HasAura("Dualcast") || Core.Player.HasAura("Swiftcast"))
                {
                    var target = Helpers.RessManager.FirstOrDefault(pm => !pm.HasAura("Raise"));

                    if (target != null)
                    {
                        return await Spellbook.Verraise.Cast(target);
                    }
                }
            }
            return false;
        }

        #endregion

        #region Role

        public async Task<bool> LucidDreaming()
        {
            if (Shadow.Settings.RedMageLucidDreaming && Core.Player.CurrentManaPercent < Shadow.Settings.RedMageLucidDreamingPct)
            {
                return await Spellbook.Role.LucidDreaming.Cast();
            }
            return false;
        }

        public async Task<bool> Swiftcast()
        {
            if (Shadow.Settings.RedMageSwiftcast && UseOffGCD && !Core.Player.HasAura("Verfire Ready") &&
                !Core.Player.HasAura("Verstone Ready"))
            {
                return await Spellbook.Role.Swiftcast.Cast();
            }
            return false;
        }

        #endregion


        #region Custom

        public static int WhiteMana => Resource.WhiteMana;
        public static int BlackMana => Resource.BlackMana;

        public static bool UseOffGCD => ActionManager.LastSpell.Name == "Veraero" || ActionManager.LastSpell.Name == "Verthunder" ||
                                         ActionManager.LastSpell.Name == "Scatter";

        #endregion
    }
}
