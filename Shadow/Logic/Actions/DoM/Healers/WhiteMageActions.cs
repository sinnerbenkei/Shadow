using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.WhiteMage;
using static ShadowCR.Constants;
using Buddy.Coroutines;
using ff14bot.Helpers;
using System.Windows.Media;

namespace ShadowCR.Rotations
{
    public class WhiteMageActions : ConjurerActions, IHealerActions
    {
        public WhiteMageSpells Spellbook { get; } = new WhiteMageSpells();

        #region Damage

        public async Task<bool> Stone()
        {
            if (!ActionManager.HasSpell(Spellbook.StoneII.Name) && !StopDamage)
            {
                return await Spellbook.Stone.Cast();
            }
            return false;
        }

        public async Task<bool> StoneII()
        {
            if (!ActionManager.HasSpell(Spellbook.StoneIII.Name) && !StopDamage)
            {
                return await Spellbook.StoneII.Cast();
            }
            return false;
        }

        public async Task<bool> StoneIII()
        {
            if (!ActionManager.HasSpell(Spellbook.StoneIV.Name) && !StopDamage)
            {
                return await Spellbook.StoneIII.Cast();
            }
            return false;
        }

        public async Task<bool> StoneIV()
        {
            if (!StopDamage)
            {
                return await Spellbook.StoneIV.Cast();
            }
            return false;
        }

        #endregion

        #region DoT

        public async Task<bool> Aero()
        {
            if (!ActionManager.HasSpell(Spellbook.AeroII.Name) && !StopDots &&
                !Core.Player.CurrentTarget.HasAura(Spellbook.Aero.Name, true, 3000))
            {
                return await Spellbook.Aero.Cast();
            }
            return false;
        }

        public async Task<bool> AeroII()
        {
            if (!StopDots && !Core.Player.CurrentTarget.HasAura(Spellbook.AeroII.Name, true, 3000))
            {
                return await Spellbook.AeroII.Cast();
            }
            return false;
        }

        #endregion

        #region AoE

        public async Task<bool> Holy()
        {
            var count = Shadow.Settings.CustomAoE ? Shadow.Settings.CustomAoECount : 3;

            if (!MovementManager.IsMoving && (Shadow.Settings.RotationMode == Modes.Multi || Helpers.EnemiesNearPlayer(8) >= count))
            {
                if (Shadow.Settings.WhiteMageThinAir && ActionManager.CanCast(Spellbook.Holy.Name, Core.Player))
                {
                    if (await Spellbook.ThinAir.Cast(null, false))
                    {
                        await Coroutine.Wait(3000, () => Core.Player.HasAura(Spellbook.ThinAir.Name));
                    }
                }
                if (!StopDamage)
                {
                    return await Spellbook.Holy.Cast();
                }
            }
            return false;
        }

        #endregion

        #region Buff

        public async Task<bool> PresenceOfMind()
        {
            if (Shadow.Settings.WhiteMagePartyHeal && Shadow.Settings.WhiteMagePresenceOfMind)
            {
                if (Helpers.HealManager.Count(hm => hm.CurrentHealthPercent < Shadow.Settings.WhiteMagePresenceOfMindPct) >=
                    Shadow.Settings.WhiteMagePresenceOfMindCount)
                {
                    return await Spellbook.PresenceOfMind.Cast(null, false);
                }
            }
            return false;
        }

        #endregion

        #region Heal

        public async Task<bool> UpdateHealing()
        {
            if (Shadow.Settings.WhiteMagePartyHeal && !await Helpers.UpdateHealManager())
            {
                return true;
            }
            return false;
        }

        public async Task<bool> StopCasting()
        {
            if (Shadow.Settings.WhiteMageInterruptOverheal && Core.Player.IsCasting)
            {
                var target = GameObjectManager.GetObjectByObjectId(Core.Player.SpellCastInfo.TargetId);
                var spellName = Core.Player.SpellCastInfo.Name;
                var freeCure = Core.Player.HasAura(155) ? Shadow.Settings.WhiteMageCurePct : Shadow.Settings.WhiteMageCureIIPct;

                if (target != null)
                {
                    if (spellName == Spellbook.Cure.Name && target.CurrentHealthPercent >= Shadow.Settings.WhiteMageCurePct + 10 ||
                        spellName == Spellbook.CureII.Name && target.CurrentHealthPercent >= freeCure + 10)
                    {
                        var debugSetting = spellName == Spellbook.Cure.Name ? Shadow.Settings.WhiteMageCurePct
                            : Shadow.Settings.WhiteMageCureIIPct;
                        Helpers.Debug($@"Target HP: {target.CurrentHealthPercent}, Setting: {debugSetting}, Adjusted: {debugSetting + 10}");

                        Logging.Write(Colors.Yellow, $@"[Shadow] Interrupting >>> {spellName}");
                        ActionManager.StopCasting();
                        await Coroutine.Wait(500, () => !Core.Player.IsCasting);
                    }
                }
            }
            return false;
        }

        public async Task<bool> Cure()
        {
            if (Shadow.Settings.WhiteMageCure)
            {
                var target = Shadow.Settings.WhiteMagePartyHeal
                    ? Helpers.HealManager.FirstOrDefault(hm => hm.CurrentHealthPercent < Shadow.Settings.WhiteMageCurePct)
                    : Core.Player.CurrentHealthPercent < Shadow.Settings.WhiteMageCurePct ? Core.Player : null;

                if (target != null)
                {
                    if (Shadow.Settings.WhiteMageCureII && Core.Player.HasAura(155))
                    {
                        return await Spellbook.CureII.Cast(target);
                    }
                    return await Spellbook.Cure.Cast(target);
                }
            }
            return false;
        }

        public async Task<bool> CureII()
        {
            if (Shadow.Settings.WhiteMageCureII)
            {
                var target = Shadow.Settings.WhiteMagePartyHeal
                    ? Helpers.HealManager.FirstOrDefault(hm => hm.CurrentHealthPercent < Shadow.Settings.WhiteMageCureIIPct)
                    : Core.Player.CurrentHealthPercent < Shadow.Settings.WhiteMageCureIIPct ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.CureII.Cast(target);
                }
            }
            return false;
        }

        public async Task<bool> Tetragrammaton()
        {
            if (Shadow.Settings.WhiteMageTetragrammaton)
            {
                var target = Shadow.Settings.WhiteMagePartyHeal
                    ? Helpers.HealManager.FirstOrDefault(hm => hm.CurrentHealthPercent < Shadow.Settings.WhiteMageTetragrammatonPct)
                    : Core.Player.CurrentHealthPercent < Shadow.Settings.WhiteMageTetragrammatonPct ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.Tetragrammaton.Cast(target, false);
                }
            }
            return false;
        }

        public async Task<bool> Benediction()
        {
            if (Shadow.Settings.WhiteMageBenediction)
            {
                var target = Shadow.Settings.WhiteMagePartyHeal
                    ? Helpers.HealManager.FirstOrDefault(hm => hm.CurrentHealthPercent < Shadow.Settings.WhiteMageBenedictionPct)
                    : Core.Player.CurrentHealthPercent < Shadow.Settings.WhiteMageBenedictionPct ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.Benediction.Cast(target, false);
                }
            }
            return false;
        }

        public async Task<bool> Regen()
        {
            if (Shadow.Settings.WhiteMageRegen)
            {
                var target = Shadow.Settings.WhiteMagePartyHeal
                    ? Helpers.HealManager.FirstOrDefault(hm => hm.CurrentHealthPercent < Shadow.Settings.WhiteMageRegenPct &&
                                                               !hm.HasAura(Spellbook.Regen.Name))
                    : Core.Player.CurrentHealthPercent < Shadow.Settings.WhiteMageRegenPct && !Core.Player.HasAura(Spellbook.Regen.Name)
                        ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.Regen.Cast(target);
                }
            }
            return false;
        }

        public async Task<bool> Medica()
        {
            if (Shadow.Settings.WhiteMageMedica && Shadow.Settings.WhiteMagePartyHeal && UseAoEHeals)
            {
                var count = Helpers.FriendsNearPlayer(Shadow.Settings.WhiteMageMedicaPct);

                if (count > 2)
                {
                    return await Spellbook.Medica.Cast();
                }
            }
            return false;
        }

        public async Task<bool> MedicaII()
        {
            if (Shadow.Settings.WhiteMageMedicaII && Shadow.Settings.WhiteMagePartyHeal && UseAoEHeals &&
                !Core.Player.HasAura(Spellbook.MedicaII.Name, true))
            {
                var count = Helpers.FriendsNearPlayer(Shadow.Settings.WhiteMageMedicaIIPct);

                if (count > 2)
                {
                    return await Spellbook.MedicaII.Cast();
                }
            }
            return false;
        }

        public async Task<bool> Assize()
        {
            if (Shadow.Settings.WhiteMageAssize && Shadow.Settings.WhiteMagePartyHeal && UseAoEHeals && Core.Player.CurrentManaPercent < 85)
            {
                var count = Helpers.FriendsNearPlayer(Shadow.Settings.WhiteMageAssizePct);

                if (count > 2)
                {
                    return await Spellbook.Assize.Cast(null, false);
                }
            }
            return false;
        }

        public async Task<bool> PlenaryIndulgence()
        {
            if (Shadow.Settings.WhiteMagePlenary && Shadow.Settings.WhiteMagePartyHeal && UseAoEHeals)
            {
                var count = Helpers.FriendsNearPlayer(Shadow.Settings.WhiteMagePlenaryPct);

                if (count > 2)
                {
                    return await Spellbook.PlenaryIndulgence.Cast(null, false);
                }
            }
            return false;
        }

        public async Task<bool> Raise()
        {
            if (Shadow.Settings.WhiteMageRaise &&
                (Shadow.Settings.WhiteMageSwiftcast && ActionManager.CanCast(Spellbook.Role.Swiftcast.Name, Core.Player) ||
                 !Helpers.HealManager.Any(hm => hm.CurrentHealthPercent < Shadow.Settings.WhiteMageCurePct)))
            {
                var target = Helpers.RessManager.FirstOrDefault(pm => !pm.HasAura("Raise"));

                if (target != null)
                {
                    if (Shadow.Settings.WhiteMageSwiftcast && ActionManager.CanCast(Spellbook.Raise.Name, target))
                    {
                        if (await Spellbook.Role.Swiftcast.Cast(null, false))
                        {
                            await Coroutine.Wait(3000, () => Core.Player.HasAura(Spellbook.Role.Swiftcast.Name));
                        }
                    }
                    return await Spellbook.Raise.Cast(target);
                }
            }
            return false;
        }

        #endregion

        #region Role

        public async Task<bool> Esuna()
        {
            if (Shadow.Settings.WhiteMageEsuna)
            {
                var target = Shadow.Settings.WhiteMagePartyHeal ? Helpers.HealManager.FirstOrDefault(hm => hm.HasDispellable())
                    : Core.Player.HasDispellable() ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.Role.Esuna.Cast(target);
                }
            }
            return false;
        }

        public async Task<bool> LucidDreaming()
        {
            if (Shadow.Settings.WhiteMageLucidDreaming && Core.Player.CurrentManaPercent < Shadow.Settings.WhiteMageLucidDreamingPct)
            {
                return await Spellbook.Role.LucidDreaming.Cast(null, false);
            }
            return false;
        }


        #endregion

        #region Custom

        public bool StopDamage => Shadow.Settings.WhiteMageStopDamage && !Core.Player.HasAura(Spellbook.ThinAir.Name) &&
                                   Core.Player.CurrentManaPercent <= Shadow.Settings.WhiteMageStopDamagePct;

        public bool StopDots => Shadow.Settings.WhiteMageStopDots && !Core.Player.HasAura(Spellbook.ThinAir.Name) &&
                                 Core.Player.CurrentManaPercent <= Shadow.Settings.WhiteMageStopDotsPct;

        public bool UseAoEHeals => Shadow.LastSpell.Name != Spellbook.Medica.Name && Shadow.LastSpell.Name != Spellbook.MedicaII.Name &&
                                    Shadow.LastSpell.Name != Spellbook.Assize.Name &&
                                    Shadow.LastSpell.Name != Spellbook.PlenaryIndulgence.Name;

        #endregion
    }
}
