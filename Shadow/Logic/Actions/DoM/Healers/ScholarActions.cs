using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Scholar;
using static ShadowCR.Constants;
using Buddy.Coroutines;
using ff14bot.Helpers;
using System.Windows.Media;

namespace ShadowCR.Rotations
{
    public class ScholarActions : ArcanistActions, IHealerActions
    {
        public ScholarSpells Spellbook { get; } = new ScholarSpells();

        #region Damage

        public async Task<bool> Ruin()
        {
            if (!ActionManager.HasSpell(Spellbook.Broil.Name) && !StopDamage)
            {
                return await Spellbook.Ruin.Cast();
            }
            return false;
        }

        public async Task<bool> Broil()
        {
            if (!ActionManager.HasSpell(Spellbook.BroilII.Name) && !StopDamage)
            {
                return await Spellbook.Broil.Cast();
            }
            return false;
        }

        public async Task<bool> BroilII()
        {
            if (!StopDamage)
            {
                return await Spellbook.BroilII.Cast();
            }
            return false;
        }

        #endregion

        #region DoT

        public async Task<bool> Bio()
        {
            if (!ActionManager.HasSpell(Spellbook.BioII.Name) && !StopDots &&
                !Core.Player.CurrentTarget.HasAura(Spellbook.Bio.Name, true, 3000))
            {
                return await Spellbook.Bio.Cast();
            }
            return false;
        }

        public async Task<bool> BioII()
        {
            if (!StopDots && !Core.Player.CurrentTarget.HasAura(Spellbook.BioII.Name, true, 3000))
            {
                return await Spellbook.BioII.Cast();
            }
            return false;
        }

        public async Task<bool> Miasma()
        {
            if (!StopDots && !Core.Player.CurrentTarget.HasAura(Spellbook.Miasma.Name, true, 4000))
            {
                return await Spellbook.Miasma.Cast();
            }
            return false;
        }

        #endregion

        #region AoE

        public async Task<bool> Bane()
        {
            if (Shadow.Settings.RotationMode != Modes.Single && Shadow.Settings.ScholarBane &&
                Core.Player.CurrentTarget.HasAura(BioDebuff, true, 20000) &&
                Core.Player.CurrentTarget.HasAura(Spellbook.Miasma.Name, true, 14000))
            {
                return await Spellbook.Bane.Cast(null, false);
            }
            return false;
        }


        #endregion

        #region Cooldown


        public async Task<bool> ChainStrategem()
        {
            if (Shadow.Settings.ScholarChainStrategem)
            {
                return await Spellbook.ChainStrategem.Cast(null, false);
            }
            return false;
        }

        #endregion

        #region Buff


        #endregion

        #region Heal

        public async Task<bool> UpdateHealing()
        {
            if (Shadow.Settings.ScholarPartyHeal && !await Helpers.UpdateHealManager())
            {
                return true;
            }
            return false;
        }

        public async Task<bool> StopCasting()
        {
            if (Shadow.Settings.ScholarInterruptOverheal && Core.Player.IsCasting)
            {
                var target = GameObjectManager.GetObjectByObjectId(Core.Player.SpellCastInfo.TargetId);
                var spellName = Core.Player.SpellCastInfo.Name;

                if (target != null)
                {
                    if (spellName == Spellbook.Physick.Name && target.CurrentHealthPercent >= Shadow.Settings.ScholarPhysickPct + 10 ||
                        spellName == Spellbook.Adloquium.Name && target.CurrentHealthPercent >= Shadow.Settings.ScholarAdloquiumPct + 10)
                    {
                        var debugSetting = spellName == Spellbook.Physick.Name ? Shadow.Settings.ScholarPhysickPct
                            : Shadow.Settings.ScholarAdloquiumPct;
                        Helpers.Debug($@"Target HP: {target.CurrentHealthPercent}, Setting: {debugSetting}, Adjusted: {debugSetting + 10}");

                        Logging.Write(Colors.Yellow, $@"[Shadow] Interrupting >>> {spellName}");
                        ActionManager.StopCasting();
                        await Coroutine.Wait(500, () => !Core.Player.IsCasting);
                    }
                }
            }
            return false;
        }

        public async Task<bool> Physick()
        {
            if (Shadow.Settings.ScholarPhysick)
            {
                var target = Shadow.Settings.ScholarPartyHeal
                    ? Helpers.HealManager.FirstOrDefault(hm => hm.CurrentHealthPercent < Shadow.Settings.ScholarPhysickPct)
                    : Core.Player.CurrentHealthPercent < Shadow.Settings.ScholarPhysickPct ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.Physick.Cast(target);
                }
            }
            return false;
        }

        public async Task<bool> Adloquium()
        {
            if (Shadow.Settings.ScholarAdloquium)
            {
                var target = Shadow.Settings.ScholarPartyHeal
                    ? Helpers.HealManager.FirstOrDefault(hm => hm.CurrentHealthPercent < Shadow.Settings.ScholarAdloquiumPct &&
                                                               !hm.HasAura("Galvanize"))
                    : Core.Player.CurrentHealthPercent < Shadow.Settings.ScholarAdloquiumPct && !Core.Player.HasAura("Galvanize")
                        ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.Adloquium.Cast(target);
                }
            }
            return false;
        }

        public async Task<bool> Aetherpact()
        {
            if (Shadow.Settings.ScholarAetherpact && Resource.FaerieGauge > 30)
            {
                var target = Shadow.Settings.ScholarPartyHeal
                    ? Helpers.HealManager.FirstOrDefault(hm => hm.IsTank() && !hm.HasAura("Fey Union") &&
                                                               hm.CurrentHealthPercent < Shadow.Settings.ScholarAetherpactPct)
                    : Core.Player.CurrentHealthPercent < Shadow.Settings.ScholarAetherpactPct && !Core.Player.HasAura("Fey Union")
                        ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.Aetherpact.Cast(target, false);
                }
            }
            return false;
        }

        public async Task<bool> Lustrate()
        {
            if (Shadow.Settings.ScholarLustrate)
            {
                var target = Shadow.Settings.ScholarPartyHeal
                    ? Helpers.HealManager.FirstOrDefault(hm => hm.CurrentHealthPercent < Shadow.Settings.ScholarLustratePct)
                    : Core.Player.CurrentHealthPercent < Shadow.Settings.ScholarLustratePct ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.Lustrate.Cast(target, false);
                }
            }
            return false;
        }

        public async Task<bool> Excogitation()
        {
            if (Shadow.Settings.ScholarExcogitation)
            {
                var target = Shadow.Settings.ScholarPartyHeal
                    ? Helpers.HealManager.FirstOrDefault(hm => hm.IsTank() &&
                                                               hm.CurrentHealthPercent < Shadow.Settings.ScholarExcogitationPct &&
                                                               !hm.HasAura(Spellbook.Excogitation.Name, true)) : null;

                if (target != null)
                {
                    return await Spellbook.Excogitation.Cast(target, false);
                }
            }
            return false;
        }

        public async Task<bool> Succor()
        {
            if (Shadow.Settings.ScholarSuccor && Shadow.Settings.ScholarPartyHeal && UseAoEHeals)
            {
                var count = Helpers.FriendsNearPlayer(Shadow.Settings.ScholarSuccorPct);
                var emergencyTactics = Shadow.Settings.ScholarEmergencyTactics &&
                                       ActionManager.CanCast(Spellbook.EmergencyTactics.Name, Core.Player);

                if (count > 2 && (emergencyTactics || !Core.Player.HasAura("Galvanize")))
                {
                    if (Shadow.Settings.ScholarEmergencyTactics && ActionManager.CanCast(Spellbook.Succor.Name, Core.Player))
                    {
                        if (await Spellbook.EmergencyTactics.Cast(null, false))
                        {
                            await Coroutine.Wait(3000, () => Core.Player.HasAura(Spellbook.EmergencyTactics.Name));
                        }
                    }
                    return await Spellbook.Succor.Cast();
                }
            }
            return false;
        }

        public async Task<bool> Indomitability()
        {
            if (Shadow.Settings.ScholarIndomitability && Shadow.Settings.ScholarPartyHeal && UseAoEHeals)
            {
                var count = Helpers.FriendsNearPlayer(Shadow.Settings.ScholarIndomitabilityPct);

                if (count > 2)
                {
                    return await Spellbook.Indomitability.Cast(null, false);
                }
            }
            return false;
        }

        public async Task<bool> Resurrection()
        {
            if (Shadow.Settings.ScholarResurrection &&
                (Shadow.Settings.ScholarSwiftcast && ActionManager.CanCast(Spellbook.Role.Swiftcast.Name, Core.Player) ||
                 !Helpers.HealManager.Any(hm => hm.CurrentHealthPercent < Shadow.Settings.ScholarPhysickPct)))
            {
                var target = Helpers.RessManager.FirstOrDefault(pm => !pm.HasAura("Raise"));

                if (target != null)
                {
                    if (Shadow.Settings.ScholarSwiftcast && ActionManager.CanCast(Spellbook.Resurrection.Name, target))
                    {
                        if (await Spellbook.Role.Swiftcast.Cast(null, false))
                        {
                            await Coroutine.Wait(3000, () => Core.Player.HasAura(Spellbook.Role.Swiftcast.Name));
                        }
                    }
                    return await Spellbook.Resurrection.Cast(target);
                }
            }
            return false;
        }

        #endregion

        #region Pet

        public async Task<bool> Summon()
        {
            if (Shadow.Settings.ScholarPet == ScholarPets.None || Shadow.Settings.ScholarPet == ScholarPets.Selene &&
                ActionManager.HasSpell(Spellbook.SummonII.Name))
            {
                return false;
            }

            if (PetManager.ActivePetType != PetType.Eos)
            {
                if (Shadow.Settings.ScholarSwiftcast && ActionManager.CanCast(Spellbook.Summon.Name, Core.Player))
                {
                    if (await Spellbook.Role.Swiftcast.Cast(null, false))
                    {
                        await Coroutine.Wait(3000, () => Core.Player.HasAura(Spellbook.Role.Swiftcast.Name));
                    }
                }
                return await Spellbook.Summon.Cast();
            }
            return false;
        }

        public async Task<bool> SummonII()
        {
            if (Shadow.Settings.ScholarPet == ScholarPets.Selene && PetManager.ActivePetType != PetType.Selene)
            {
                if (Shadow.Settings.ScholarSwiftcast && ActionManager.CanCast(Spellbook.SummonII.Name, Core.Player))
                {
                    if (await Spellbook.Role.Swiftcast.Cast(null, false))
                    {
                        await Coroutine.Wait(3000, () => Core.Player.HasAura(Spellbook.Role.Swiftcast.Name));
                    }
                }
                return await Spellbook.SummonII.Cast();
            }
            return false;
        }

        #endregion

        #region Role


        public async Task<bool> Esuna()
        {
            if (Shadow.Settings.ScholarEsuna)
            {
                var target = Shadow.Settings.ScholarPartyHeal ? Helpers.HealManager.FirstOrDefault(hm => hm.HasDispellable())
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
            if (Shadow.Settings.ScholarLucidDreaming && Core.Player.CurrentManaPercent < Shadow.Settings.ScholarLucidDreamingPct)
            {
                return await Spellbook.Role.LucidDreaming.Cast(null, false);
            }
            return false;
        }


        #endregion

        #region Custom

        public static bool StopDamage => Shadow.Settings.ScholarStopDamage && Core.Player.CurrentManaPercent <= Shadow.Settings.ScholarStopDamagePct;
        public static bool StopDots => Shadow.Settings.ScholarStopDots && Core.Player.CurrentManaPercent <= Shadow.Settings.ScholarStopDotsPct;

        public static string BioDebuff => Core.Player.ClassLevel >= 26 ? "Bio II" : "Bio";
        public static bool PetExists => Core.Player.Pet != null;

        public bool UseAoEHeals => Shadow.LastSpell.Name != Spellbook.Succor.Name && Shadow.LastSpell.Name != Spellbook.Indomitability.Name;

        #endregion
    }
}
