using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Summoner;
using static ShadowCR.Constants;
using Buddy.Coroutines;
using ff14bot.Helpers;
using System.Windows.Media;

namespace ShadowCR.Rotations
{
    public class SummonerActions : ArcanistActions, ICasterActions
    {
        public SummonerSpells Spellbook { get; } = new SummonerSpells();

        #region Damage

        public async Task<bool> Ruin()
        {
            if (!ActionManager.HasSpell(Spellbook.RuinIII.Name))
            {
                return await Spellbook.Ruin.Cast();
            }
            return false;
        }

        public async Task<bool> RuinII()
        {
            if (Resource.DreadwyrmTrance && !Core.Player.HasAura("Further Ruin") && !RecentBahamut)
                return false;

            if (MovementManager.IsMoving ||
                Core.Player.HasAura("Further Ruin") ||
                RecentBahamut ||
                UseBane ||
                UseFester ||
                UsePainflare ||
                UseAddle ||
                UsePet ||
                UseTriDisaster ||
                ActionManager.CanCast(Spellbook.SummonBahamut.Name, Core.Player) ||
                ActionManager.CanCast(Spellbook.DreadwyrmTrance.Name, Core.Player))
            {
                return await Spellbook.RuinII.Cast();
            }
            return false;
        }

        public async Task<bool> RuinIII()
        {
            return await Spellbook.RuinIII.Cast();
        }

        #endregion

        #region DoT

        public async Task<bool> Bio()
        {
            if (!ActionManager.HasSpell(Spellbook.BioII.Name) && !Core.Player.CurrentTarget.HasAura(Spellbook.Bio.Name, true, 3000))
            {
                return await Spellbook.Bio.Cast();
            }
            return false;
        }

        public async Task<bool> BioII()
        {
            if (!ActionManager.HasSpell(Spellbook.BioIII.Name) && !RecentDoT &&
                !Core.Player.CurrentTarget.HasAura(Spellbook.BioII.Name, true, 3000))
            {
                if (!ActionManager.HasSpell(Spellbook.TriDisaster.Name) || Spellbook.TriDisaster.Cooldown() > 5000)
                {
                    return await Spellbook.BioII.Cast();
                }
            }
            return false;
        }

        public async Task<bool> BioIII()
        {
            if (RecentDoT ||
                Core.Player.CurrentTarget.HasAura(Spellbook.BioIII.Name, true, 3000) ||
                RecentBahamut ||
                Spellbook.TriDisaster.Cooldown() < 5000)
            {
                return false;
            }

            return await Spellbook.BioIII.Cast();
        }

        public async Task<bool> Miasma()
        {
            if (!ActionManager.HasSpell(Spellbook.MiasmaIII.Name) && !RecentDoT &&
                !Core.Player.CurrentTarget.HasAura(Spellbook.Miasma.Name, true, 5000))
            {
                if (!ActionManager.HasSpell(Spellbook.TriDisaster.Name) || Spellbook.TriDisaster.Cooldown() > 5000)
                {
                    return await Spellbook.Miasma.Cast();
                }
            }
            return false;
        }

        public async Task<bool> MiasmaIII()
        {
            if (RecentDoT ||
                Core.Player.CurrentTarget.HasAura(Spellbook.MiasmaIII.Name, true, 5000) ||
                RecentBahamut ||
                Spellbook.TriDisaster.Cooldown() < 5000)
            {
                return false;
            }

            return await Spellbook.MiasmaIII.Cast();
        }

        #endregion

        #region AoE

        public async Task<bool> Bane()
        {
            if (UseBane)
            {
                return await Spellbook.Bane.Cast();
            }
            return false;
        }

        public async Task<bool> Painflare()
        {
            if (UsePainflare)
            {
                return await Spellbook.Painflare.Cast();
            }
            return false;
        }


        #endregion

        #region Cooldown


        public async Task<bool> Fester()
        {
            if (UseFester)
            {
                return await Spellbook.Fester.Cast();
            }
            return false;
        }


        public async Task<bool> Enkindle()
        {
            if (Shadow.Settings.SummonerEnkindle && PetExists)
            {
                return await Spellbook.Enkindle.Cast();
            }
            return false;
        }

        public async Task<bool> TriDisaster()
        {
            if (!UseTriDisaster) return false;

            return await Spellbook.TriDisaster.Cast();
        }

        public async Task<bool> Deathflare()
        {
            if (Resource.DreadwyrmTrance && Resource.Timer.TotalMilliseconds < 2000)
            {
                return await Spellbook.Deathflare.Cast();
            }
            return false;
        }

        public async Task<bool> EnkindleBahamut()
        {
            if (Shadow.Settings.SummonerEnkindleBahamut)
            {
                return await Spellbook.EnkindleBahamut.Cast();
            }
            return false;
        }

        #endregion

        #region Buff

        public async Task<bool> DreadwyrmTrance()
        {
            if (Shadow.Settings.SummonerDreadwyrmTrance)
            {
                return await Spellbook.DreadwyrmTrance.Cast();
            }
            return false;
        }

        public async Task<bool> Aetherpact()
        {
            if (Shadow.Settings.SummonerAetherpact && PetExists)
            {
                return await Spellbook.Aetherpact.Cast();
            }
            return false;
        }

        public async Task<bool> SummonBahamut()
        {
            if (Shadow.Settings.SummonerSummonBahamut)
            {
                if (await Spellbook.SummonBahamut.Cast())
                {
                    Spell.RecentSpell.Add("Summon Bahamut", DateTime.UtcNow + TimeSpan.FromMilliseconds(22000));
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Heal

        public async Task<bool> UpdateHealing()
        {
            if (Shadow.Settings.SummonerResurrection && Shadow.Settings.SummonerSwiftcast)
            {
                if (!await Helpers.UpdateHealManager())
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> Physick()
        {
            if (Shadow.Settings.SummonerPhysick && Core.Player.CurrentHealthPercent < Shadow.Settings.SummonerPhysickPct)
            {
                var target = Core.Player;

                if (target != null)
                {
                    return await Spellbook.Physick.Cast(target);
                }
            }
            return false;
        }

        public async Task<bool> Resurrection()
        {
            if (Shadow.Settings.SummonerResurrection && Shadow.Settings.SummonerSwiftcast && Core.Player.CurrentManaPercent > 50 &&
                ActionManager.CanCast(Spellbook.Role.Swiftcast.Name, Core.Player))
            {
                var target = Helpers.RessManager.FirstOrDefault(pm => !pm.HasAura("Raise"));

                if (target != null)
                {
                    if (await Spellbook.Role.Swiftcast.Cast(null, false))
                    {
                        await Coroutine.Wait(3000, () => Core.Player.HasAura(Spellbook.Role.Swiftcast.Name));
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
            if (!Shadow.Settings.SummonerOpener || !Shadow.Settings.SummonerOpenerGaruda || Helpers.OpenerFinished)
            {
                if (Shadow.Settings.SummonerPet == SummonerPets.None ||
                    Shadow.Settings.SummonerPet == SummonerPets.Titan && ActionManager.HasSpell(Spellbook.SummonII.Name) ||
                    Shadow.Settings.SummonerPet == SummonerPets.Ifrit && ActionManager.HasSpell(Spellbook.SummonIII.Name))
                {
                    return false;
                }
            }

            if (PetManager.ActivePetType != PetType.Emerald_Carbuncle && PetManager.ActivePetType != PetType.Garuda_Egi && !RecentBahamut)
            {
                if (Shadow.Settings.SummonerSwiftcast && !Shadow.Settings.SummonerResurrection &&
                    ActionManager.CanCast(Spellbook.Summon.Name, Core.Player) &&
                    (!Shadow.Settings.SummonerOpener || !Shadow.Settings.SummonerOpenerGaruda || Helpers.OpenerFinished))
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
            if (Shadow.Settings.SummonerOpener && Shadow.Settings.SummonerOpenerGaruda && !Helpers.OpenerFinished)
            {
                return false;
            }

            if (Shadow.Settings.SummonerPet == SummonerPets.Titan && PetManager.ActivePetType != PetType.Topaz_Carbuncle &&
                PetManager.ActivePetType != PetType.Titan_Egi && !RecentBahamut)
            {
                if (Shadow.Settings.SummonerSwiftcast && !Shadow.Settings.SummonerResurrection &&
                    ActionManager.CanCast(Spellbook.SummonII.Name, Core.Player))
                {
                    if (await Spellbook.Role.Swiftcast.Cast(null, false))
                    {
                        await Coroutine.Wait(3000, () => ActionManager.CanCast(Spellbook.SummonII.Name, Core.Player));
                    }
                }
                return await Spellbook.SummonII.Cast();
            }
            return false;
        }

        public async Task<bool> SummonIII()
        {
            if (Shadow.Settings.SummonerOpener && Shadow.Settings.SummonerOpenerGaruda && !Helpers.OpenerFinished)
            {
                return false;
            }

            if (Shadow.Settings.SummonerPet == SummonerPets.Ifrit && PetManager.ActivePetType != PetType.Ifrit_Egi && !RecentBahamut)
            {
                if (Shadow.Settings.SummonerSwiftcast && !Shadow.Settings.SummonerResurrection &&
                    ActionManager.CanCast(Spellbook.SummonIII.Name, Core.Player))
                {
                    if (await Spellbook.Role.Swiftcast.Cast(null, false))
                    {
                        await Coroutine.Wait(3000, () => ActionManager.CanCast(Spellbook.SummonIII.Name, Core.Player));
                    }
                }
                return await Spellbook.SummonIII.Cast();
            }
            return false;
        }

        public async Task<bool> Sic()
        {
            if (PetManager.ActivePetType == PetType.Ifrit_Egi && PetManager.PetMode != PetMode.Sic)
            {
                if (await Coroutine.Wait(1000, () => PetManager.DoAction("Sic", Core.Player)))
                {
                    Logging.Write(Colors.GreenYellow, @"[Shadow] Casting >>> Sic");
                    return await Coroutine.Wait(3000, () => PetManager.PetMode == PetMode.Sic);
                }
            }
            return false;
        }

        public async Task<bool> Obey()
        {
            if (PetManager.ActivePetType == PetType.Garuda_Egi && PetManager.PetMode != PetMode.Obey)
            {
                if (await Coroutine.Wait(1000, () => PetManager.DoAction("Obey", Core.Player)))
                {
                    Logging.Write(Colors.GreenYellow, @"[Shadow] Casting >>> Obey");
                    return await Coroutine.Wait(3000, () => PetManager.PetMode == PetMode.Obey);
                }
            }
            return false;
        }

        #endregion

        #region Role

        public async Task<bool> Addle()
        {
            if (UseAddle)
            {
                return await Spellbook.Role.Addle.Cast();
            }
            return false;
        }


        public async Task<bool> LucidDreaming()
        {
            if (Shadow.Settings.SummonerLucidDreaming && Core.Player.CurrentManaPercent < Shadow.Settings.SummonerLucidDreamingPct)
            {
                return await Spellbook.Role.LucidDreaming.Cast();
            }
            return false;
        }

        #endregion

        #region Custom

        public static int AoECount => Shadow.Settings.CustomAoE ? Shadow.Settings.CustomAoECount : 2;
        public static string BioDebuff => Core.Player.ClassLevel >= 66 ? "Bio III" : Core.Player.ClassLevel >= 26 ? "Bio II" : "Bio";
        public static string MiasmaDebuff => Core.Player.ClassLevel >= 66 ? "Miasma III" : "Miasma";
        public static bool RecentDoT { get { return Spell.RecentSpell.Keys.Any(key => key.Contains("Tri-disaster")); } }
        public static bool RecentBahamut => Spell.RecentSpell.ContainsKey("Summon Bahamut") || (int)PetManager.ActivePetType == 10;
        public static bool PetExists => Core.Player.Pet != null;

        public static bool UseTriDisaster => Shadow.Settings.SummonerTriDisaster &&
                                              (!Core.Player.CurrentTarget.HasAura(BioDebuff, true, 3000) ||
                                               !Core.Player.CurrentTarget.HasAura(MiasmaDebuff, true, 3000));

        public bool UseBane => Shadow.Settings.RotationMode != Modes.Single && Shadow.Settings.SummonerBane &&
                                ActionManager.CanCast(Spellbook.Bane.Name, Core.Player.CurrentTarget) &&
                                (Shadow.Settings.RotationMode == Modes.Multi || Helpers.EnemiesNearTarget(5) >= AoECount) &&
                                Core.Player.CurrentTarget.HasAura(BioDebuff, true, 20000) &&
                                Core.Player.CurrentTarget.HasAura(MiasmaDebuff, true, 14000);

        public bool UseFester => (Shadow.Settings.RotationMode == Modes.Single ||
                                   Shadow.Settings.RotationMode == Modes.Smart && Helpers.EnemiesNearTarget(5) < AoECount) &&
                                  ActionManager.CanCast(Spellbook.Fester.Name, Core.Player.CurrentTarget) &&
                                  Core.Player.CurrentTarget.HasAura(BioDebuff, true) &&
                                  Core.Player.CurrentTarget.HasAura(MiasmaDebuff, true);

        public bool UsePainflare => Shadow.Settings.RotationMode != Modes.Single &&
                                     (Shadow.Settings.RotationMode == Modes.Multi || Helpers.EnemiesNearTarget(5) >= AoECount) &&
                                     ActionManager.CanCast(Spellbook.Painflare.Name, Core.Player.CurrentTarget);

        public bool UsePet => PetExists && (Shadow.Settings.SummonerEnkindle &&
                                             ActionManager.CanCast(Spellbook.Enkindle.Name, Core.Player.CurrentTarget));

        public bool UseAddle => Shadow.Settings.SummonerAddle && RecentBahamut &&
                                 ActionManager.CanCast(Spellbook.Role.Addle.Name, Core.Player.CurrentTarget);

        #endregion
    }
}
