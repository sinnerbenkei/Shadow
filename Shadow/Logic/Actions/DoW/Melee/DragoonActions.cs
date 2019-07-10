using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Dragoon;
using static ShadowCR.Constants;

namespace ShadowCR.Rotations
{
    public class DragoonActions : LancerActions, IMeleeActions
    {
        public DragoonSpells Spellbook { get; } = new DragoonSpells();

        #region Damage

        public async Task<bool> TrueThrust()
        {
            return await Spellbook.TrueThrust.Cast();
        }

        public async Task<bool> VorpalThrust()
        {
            if (ActionManager.LastSpell.Name == Spellbook.TrueThrust.Name)
            {
                return await Spellbook.VorpalThrust.Cast();
            }
            return false;
        }

        public async Task<bool> Disembowel()
        {
            return await Spellbook.Disembowel.Cast();
        }

        public async Task<bool> ChaosThrust()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Disembowel.Name)
            {
                return await Spellbook.ChaosThrust.Cast();
            }
            return false;
        }

        public async Task<bool> FangAndClaw()
        {
            return await Spellbook.FangAndClaw.Cast();
        }

        public async Task<bool> WheelingThrust()
        {
            return await Spellbook.WheelingThrust.Cast();
        }

        #endregion

        #region AoE

        public async Task<bool> DoomSpike()
        {
            //if (Core.Player.CurrentTPPercent > 30 && Core.Player.HasAura(Spellbook.HeavyThrust.Name))
            //{
            //    var count = Shadow.Settings.CustomAoE ? Shadow.Settings.CustomAoECount : 3;

            //    if (Shadow.Settings.RotationMode == Modes.Multi || Helpers.EnemiesNearTarget(5) >= count)
            //    {
            //        return await Spellbook.DoomSpike.Cast();
            //    }
            //}
            return false;
        }

        public async Task<bool> SonicThrust()
        {
            //if (ActionManager.LastSpell.Name == Spellbook.DoomSpike.Name && Core.Player.CurrentTPPercent > 30 &&
            //    Core.Player.HasAura(Spellbook.HeavyThrust.Name))
            //{
            //    var count = Shadow.Settings.CustomAoE ? Shadow.Settings.CustomAoECount : 3;

            //    if (Shadow.Settings.RotationMode == Modes.Multi || Helpers.EnemiesNearTarget(5) >= count)
            //    {
            //        return await Spellbook.SonicThrust.Cast();
            //    }
            //}
            return false;
        }

        #endregion

        #region Cooldown

        public async Task<bool> Jump()
        {
            if (Shadow.Settings.DragoonJump && !MovementManager.IsMoving && !RecentJump && UseJump)
            {
                return await Spellbook.Jump.Cast();
            }
            return false;
        }

        public async Task<bool> SpineshatterDive()
        {
            if (Shadow.Settings.DragoonSpineshatter && !MovementManager.IsMoving && !RecentJump && UseJump)
            {
                return await Spellbook.SpineshatterDive.Cast();
            }
            return false;
        }

        public async Task<bool> DragonfireDive()
        {
            if (Shadow.Settings.DragoonDragonfire && !MovementManager.IsMoving && !RecentJump)
            {
                return await Spellbook.DragonfireDive.Cast();
            }
            return false;
        }

        public async Task<bool> Geirskogul()
        {
            if (Shadow.Settings.DragoonGeirskogul)
            {
                if (Resource.DragonGaze == 3 || !RecentJump && !Core.Player.HasAura(1243) && JumpCooldown > 25 && SpineCooldown > 25 ||
                    Core.Player.ClassLevel < 70)
                {
                    return await Spellbook.Geirskogul.Cast();
                }
            }
            return false;
        }

        public async Task<bool> Nastrond()
        {
            if (Shadow.Settings.DragoonGeirskogul)
            {
                return await Spellbook.Nastrond.Cast();
            }
            return false;
        }

        public async Task<bool> MirageDive()
        {
            if (Shadow.Settings.DragoonMirage && !MovementManager.IsMoving && !RecentJump)
            {
                return await Spellbook.MirageDive.Cast();
            }
            return false;
        }

        #endregion

        #region Buff

        public async Task<bool> LifeSurge()
        {
            if (Shadow.Settings.DragoonLifeSurge && ActionManager.LastSpell.Name == Spellbook.VorpalThrust.Name)
            {
                return await Spellbook.LifeSurge.Cast();
            }
            return false;
        }

        public async Task<bool> BattleLitany()
        {
            if (Shadow.Settings.DragoonBattleLitany)
            {
                return await Spellbook.BattleLitany.Cast();
            }
            return false;
        }

        public async Task<bool> BloodOfTheDragon()
        {
            if (Shadow.Settings.DragoonBloodOfTheDragon && !BloodActive &&
                (ActionManager.LastSpell.Name == Spellbook.VorpalThrust.Name || ActionManager.LastSpell.Name == Spellbook.Disembowel.Name))
            {
                return await Spellbook.BloodOfTheDragon.Cast();
            }
            return false;
        }

        public async Task<bool> DragonSight()
        {
            if (!Shadow.Settings.DragoonDragonSight) return false;

            var target = !PartyManager.IsInParty && ChocoboManager.Summoned ? ChocoboManager.Object
                : Managers.DragonSight.FirstOrDefault();

            if (target == null) return false;

            return await Spellbook.DragonSight.Cast(target);
        }

        #endregion

        #region Role

        public async Task<bool> SecondWind()
        {
            if (Shadow.Settings.DragoonSecondWind && Core.Player.CurrentHealthPercent < Shadow.Settings.DragoonSecondWindPct)
            {
                return await Spellbook.Role.SecondWind.Cast();
            }
            return false;
        }

        public async Task<bool> Bloodbath()
        {
            if (Shadow.Settings.DragoonBloodbath && Core.Player.CurrentHealthPercent < Shadow.Settings.DragoonBloodbathPct)
            {
                return await Spellbook.Role.Bloodbath.Cast();
            }
            return false;
        }

        public async Task<bool> TrueNorth()
        {
            if (Shadow.Settings.DragoonTrueNorth)
            {
                return await Spellbook.Role.TrueNorth.Cast();
            }
            return false;
        }

        #endregion

        #region Custom

        public static bool RecentJump { get { return Spell.RecentSpell.Keys.Any(rs => rs.Contains("Dive") || rs.Contains("Jump")); } }
        public static bool BloodActive => Resource.Timer != TimeSpan.Zero;
        public static double JumpCooldown => DataManager.GetSpellData(92).Cooldown.TotalSeconds;
        public static double SpineCooldown => DataManager.GetSpellData(95).Cooldown.TotalSeconds;
        public bool UseJump => BloodActive || !ActionManager.HasSpell(Spellbook.BloodOfTheDragon.Name);

        #endregion
    }
}
