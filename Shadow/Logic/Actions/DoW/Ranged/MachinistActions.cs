using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Machinist;
using static ShadowCR.Constants;
using Buddy.Coroutines;

namespace ShadowCR.Rotations
{
    public class MachinistActions : IRangedActions
    {
        public MachinistSpells Spellbook { get; } = new MachinistSpells();

        #region Damage

        public async Task<bool> SplitShot()
        {
            return await Spellbook.SplitShot.Cast();
        }

        public async Task<bool> SlugShot()
        {
            if (Shadow.Settings.MachinistSyncWildfire && !Core.Player.HasAura("Cleaner Shot") && WildfireCooldown <= 8000)
            {
                return await Spellbook.SlugShot.Cast();
            }
            if (Core.Player.HasAura("Enhanced Slug Shot") && (!Shadow.Settings.MachinistSyncWildfire ||
                                                              Core.Player.CurrentTarget.HasAura(Spellbook.Wildfire.Name, true) ||
                                                              WildfireCooldown > 8000))
            {
                return await Spellbook.SlugShot.Cast();
            }
            return false;
        }

        public async Task<bool> CleanShot()
        {
            if (Core.Player.HasAura("Cleaner Shot") && (!Shadow.Settings.MachinistSyncWildfire ||
                                                        Core.Player.CurrentTarget.HasAura(Spellbook.Wildfire.Name, true) ||
                                                        WildfireCooldown > 8000))
            {
                return await Spellbook.CleanShot.Cast();
            }
            return false;
        }

        public async Task<bool> HotShot()
        {
            if (!Core.Player.HasAura(Spellbook.HotShot.Name, true, 6000) ||
                !Core.Player.HasAura(Spellbook.HotShot.Name, true, 55000) && Resource.Heat == 0 &&
                Resource.OverheatRemaining < TimeSpan.FromMilliseconds(3000))
            {
                return await Spellbook.HotShot.Cast();
            }
            return false;
        }

        #endregion

        #region AoE

        public async Task<bool> SpreadShot()
        {
            //if (Core.Player.CurrentTPPercent > 40)
            //{
            //    if (Shadow.Settings.RotationMode == Modes.Multi || Shadow.Settings.RotationMode == Modes.Smart &&
            //        Helpers.EnemiesNearTarget(5) >= AoECount)
            //    {
            //        return await Spellbook.SpreadShot.Cast();
            //    }
            //}
            return false;
        }

        #endregion

        #region Cooldown

        public async Task<bool> Wildfire()
        {
            if (UseWildfire && (!Shadow.Settings.MachinistSyncOverheat || Overheated))
            {
                return await Spellbook.Wildfire.Cast();
            }
            return false;
        }

        public async Task<bool> GaussRound()
        {
            if (!Shadow.Settings.MachinistSyncWildfire || Spellbook.Wildfire.Cooldown() > 15000)
            {
                return await Spellbook.GaussRound.Cast();
            }
            return false;
        }

        public async Task<bool> Ricochet()
        {
            if (Shadow.Settings.MachinistRicochet)
            {
                if (!Shadow.Settings.MachinistSyncWildfire || Core.Player.CurrentTarget.HasAura(Spellbook.Wildfire.Name, true))
                {
                    return await Spellbook.Ricochet.Cast();
                }
            }
            return false;
        }

        public async Task<bool> Flamethrower()
        {
            if (Shadow.Settings.MachinistFlamethrower && Resource.Heat < 100 && !MovementManager.IsMoving)
            {
                if (BarrelCooldown < 30000 && UseWildfire && WildfireCooldown < 3000 || UseFlamethrower)
                {
                    if (await Spellbook.Flamethrower.Cast())
                    {
                        return await Coroutine.Wait(3000, () => Core.Player.HasAura(Spellbook.Flamethrower.Name));
                    }
                }
            }
            return false;
        }

        public async Task<bool> FlamethrowerBuff()
        {
            if (Core.Player.HasAura(Spellbook.Flamethrower.Name) &&
                (!Shadow.Settings.MachinistFlamethrower || Resource.Heat < 100 || UseFlamethrower))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Buff


        public async Task<bool> Reassemble()
        {
            if (Shadow.Settings.MachinistReassemble)
            {
                if (!Shadow.Settings.MachinistSyncWildfire || Core.Player.CurrentTarget.HasAura(Spellbook.Wildfire.Name, true))
                {
                    if (Core.Player.HasAura("Cleaner Shot") && Shadow.LastSpell.Name != Spellbook.CleanShot.Name ||
                        !ActionManager.HasSpell(Spellbook.CleanShot.Name) && Core.Player.HasAura("Enhanced Slug Shot") &&
                        Shadow.LastSpell.Name != Spellbook.SlugShot.Name)
                    {
                        return await Spellbook.Reassemble.Cast();
                    }
                }
            }
            return false;
        }


        public async Task<bool> Hypercharge()
        {
            if (Shadow.Settings.MachinistHypercharge && TurretExists)
            {
                return await Spellbook.Hypercharge.Cast();
            }
            return false;
        }

        public async Task<bool> BarrelStabilizer()
        {
            if (Shadow.Settings.MachinistBarrelStabilizer)
            {
                if (Resource.Heat < 30 && (!Shadow.Settings.MachinistFlamethrower || !ActionManager.HasSpell(Spellbook.Flamethrower.Name) ||
                                           FlamethrowerCooldown > 3000))
                {
                    return await Spellbook.BarrelStabilizer.Cast();
                }
            }
            return false;
        }

        public async Task<bool> RookOverdrive()
        {
            if (Shadow.Settings.MachinistRookOverdrive && TurretExists && PetManager.ActivePetType == PetType.Rook_Autoturret)
            {
                if (Core.Player.CurrentTarget.HasAura(Spellbook.Wildfire.Name, true) &&
                    !Core.Player.CurrentTarget.HasAura(Spellbook.Wildfire.Name, true, 4000))
                {
                    return await Spellbook.RookOverdrive.Cast();
                }
            }
            return false;
        }


        #endregion

        #region Turret

        public async Task<bool> RookAutoturret()
        {
            if (!Core.Player.HasAura("Turret Reset"))
            {
                if (PetManager.ActivePetType != PetType.Rook_Autoturret || TurretDistance > 23)
                {
                    var castLocation = Shadow.Settings.MachinistTurretLocation == CastLocations.Self ? Core.Player
                        : Core.Player.CurrentTarget;

                    return await Spellbook.RookAutoturret.Cast(castLocation);
                }
            }
            return false;
        }

        #endregion

        #region Role

        public async Task<bool> SecondWind()
        {
            if (Shadow.Settings.MachinistSecondWind && Core.Player.CurrentHealthPercent < Shadow.Settings.MachinistSecondWindPct)
            {
                return await Spellbook.Role.SecondWind.Cast();
            }
            return false;
        }

        public async Task<bool> Peloton()
        {
            if (Shadow.Settings.MachinistPeloton && !Core.Player.HasAura(Spellbook.Role.Peloton.Name) && !Core.Player.HasTarget &&
                (MovementManager.IsMoving || BotManager.Current.EnglishName == "DeepDive"))
            {
                return await Spellbook.Role.Peloton.Cast(null, false);
            }
            return false;
        }

        #endregion

        #region Custom

        public static int AoECount => Shadow.Settings.CustomAoE ? Shadow.Settings.CustomAoECount : 3;
        public static double FlamethrowerCooldown => DataManager.GetSpellData(7418).Cooldown.TotalMilliseconds;
        public static double WildfireCooldown => DataManager.GetSpellData(2878).Cooldown.TotalMilliseconds;
        public static double BarrelCooldown => DataManager.GetSpellData(7414).Cooldown.TotalMilliseconds;
        public static bool Overheated => Resource.Heat == 100 && Resource.OverheatRemaining.TotalMilliseconds > 0;
        public static bool UseFlamethrower => Shadow.Settings.RotationMode == Modes.Multi ||
                                               Shadow.Settings.RotationMode == Modes.Smart && Helpers.EnemiesNearTarget(5) >= AoECount;

        public static bool UseWildfire => Shadow.Settings.MachinistWildfire &&
                                           (Core.Player.CurrentTarget.IsBoss() ||
                                            Core.Player.CurrentTarget.CurrentHealth > Shadow.Settings.MachinistWildfireHP);

        public static bool TurretExists => Core.Player.Pet != null;
        public static float TurretDistance => TurretExists && Core.Player.HasTarget && Core.Player.CurrentTarget.CanAttack
            ? Core.Player.Pet.Distance2D(Core.Player.CurrentTarget) - Core.Player.CurrentTarget.CombatReach : 0;

        #endregion
    }
}
