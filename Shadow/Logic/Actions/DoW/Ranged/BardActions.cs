using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Bard;
using static ShadowCR.Constants;
using Buddy.Coroutines;

namespace ShadowCR.Rotations
{
    public class BardActions : ArcherActions, IRangedActions
    {
        public BardSpells Spellbook { get; } = new BardSpells();

        #region Damage

        public async Task<bool> HeavyShot()
        {
            return await Spellbook.HeavyShot.Cast();
        }

        public async Task<bool> StraightShotBuff()
        {
            if (!Core.Player.HasAura(Spellbook.StraightShot.Name, true, 6000))
            {
                return await Spellbook.StraightShot.Cast();
            }
            return false;
        }

        public async Task<bool> StraightShot()
        {
            if (Core.Player.HasAura("Straighter Shot") && !ActionManager.HasSpell(Spellbook.RefulgentArrow.Name))
            {
                return await Spellbook.StraightShot.Cast();
            }
            return false;
        }

        public async Task<bool> Bloodletter()
        {
            return await Spellbook.Bloodletter.Cast();
        }

        public async Task<bool> PitchPerfect()
        {
            if (!Shadow.Settings.BardPitchPerfect) return false;

            var critBonus = DotManager.Check(Target, true);

            if (NumRepertoire >= Shadow.Settings.BardRepertoireCount || MinuetActive && SongTimer < 2000 ||
                critBonus >= 20 && NumRepertoire >= 2)
            {
                return await Spellbook.PitchPerfect.Cast();
            }
            return false;
        }

        public async Task<bool> RefulgentArrow()
        {
            if (Core.Player.HasAura(122) && (!Shadow.Settings.BardBarrage || Spellbook.Barrage.Cooldown() > 7000 ||
                                             !Core.Player.HasAura(Spellbook.StraightShot.Name, true, 6000)))
            {
                return await Spellbook.RefulgentArrow.Cast();
            }
            return false;
        }

        public async Task<bool> BarrageActive()
        {
            if (Core.Player.HasAura(Spellbook.Barrage.Name))
            {
                if (await Spellbook.RefulgentArrow.Cast())
                {
                    return true;
                }
                if (ActionManager.LastSpell.Name == Spellbook.HeavyShot.Name)
                {
                    await Coroutine.Wait(1000, () => Core.Player.HasAura(122));
                }
                if (!Core.Player.HasAura(122))
                {
                    return await Spellbook.EmpyrealArrow.Cast(null, false);
                }
            }
            return false;
        }

        #endregion

        #region DoT

        public async Task<bool> VenomousBite()
        {
            if (!Shadow.Settings.BardUseDots || Target.HasAura(VenomDebuff, true, 4000) || !await Spellbook.VenomousBite.Cast())
                return false;

            DotManager.Add(Target);
            return true;
        }

        public async Task<bool> Windbite()
        {
            if (!Shadow.Settings.BardUseDots || Target.HasAura(WindDebuff, true, 4000) || !await Spellbook.Windbite.Cast())
                return false;

            DotManager.Add(Target);
            return true;
        }

        public async Task<bool> IronJaws()
        {
            if (!Target.HasAura(VenomDebuff, true) || !Target.HasAura(WindDebuff, true) ||
                Target.HasAura(VenomDebuff, true, 5000) && Target.HasAura(WindDebuff, true, 5000) || !await Spellbook.IronJaws.Cast())
            {
                return false;
            }

            DotManager.Add(Target);
            return true;
        }

        public async Task<bool> DotSnapshot()
        {
            if (!Shadow.Settings.BardDotSnapshot ||
                !Core.Player.CurrentTarget.HasAura(VenomDebuff, true) ||
                !Core.Player.CurrentTarget.HasAura(WindDebuff, true) ||
                DotManager.Recent(Target))
            {
                return false;
            }

            var crit = DotManager.Difference(Target, true);
            var damage = crit + DotManager.Difference(Target);

            // Prioritise 30% crit buff
            if (crit >= 30 || crit >= 0 && damage >= 0 && (DotManager.CritExpiring || DotManager.DamageExpiring))
            {
                if (await Spellbook.IronJaws.Cast())
                {
                    DotManager.Add(Target);
                    return true;
                }
            }

            if (DotManager.Check(Target, true) >= 30) return false;

            // Refresh during damage buffs
            if (damage >= 20 || damage >= 10 && Target.AuraExpiring(WindDebuff, true, 10000) || damage >= 0 && DotManager.BuffExpiring)
            {
                if (await Spellbook.IronJaws.Cast())
                {
                    DotManager.Add(Target);
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region AoE

        public async Task<bool> QuickNock()
        {
            if (Shadow.Settings.BardUseDotsAoe && (!Core.Player.CurrentTarget.HasAura(VenomDebuff, true, 4000) ||
                                                   !Core.Player.CurrentTarget.HasAura(WindDebuff, true, 4000)))
            {
                return false;
            }

            //if (Core.Player.CurrentTPPercent > 40)
            //{
            //    var count = Shadow.Settings.CustomAoE ? Shadow.Settings.CustomAoECount : 3;

            //    if (Shadow.Settings.RotationMode == Modes.Multi || Shadow.Settings.RotationMode == Modes.Smart &&
            //        Helpers.EnemiesNearTarget(5) >= count)
            //    {
            //        return await Spellbook.QuickNock.Cast();
            //    }
            //}
            return false;
        }

        public async Task<bool> RainOfDeath()
        {
            if (Shadow.Settings.RotationMode == Modes.Multi || Shadow.Settings.RotationMode == Modes.Smart &&
                Helpers.EnemiesNearTarget(5) > 1)
            {
                return await Spellbook.RainOfDeath.Cast();
            }
            return false;
        }

        #endregion

        #region Cooldown

        public async Task<bool> MagesBallad()
        {
            if (Shadow.Settings.BardSongs &&
                !RecentSong &&
                (NoSong ||
                 PaeonActive && DataManager.GetSpellData(3559).Cooldown.TotalSeconds < 30 ||
                 MinuetActive && SongTimer < 2000 && Spellbook.PitchPerfect.Cooldown() > 0))
            {
                return await Spellbook.MagesBallad.Cast();
            }
            return false;
        }

        public async Task<bool> ArmysPaeon()
        {
            if (Shadow.Settings.BardSongs && !RecentSong && NoSong)
            {
                return await Spellbook.ArmysPaeon.Cast();
            }
            return false;
        }

        public async Task<bool> WanderersMinuet()
        {
            if (Shadow.Settings.BardSongs && !RecentSong &&
                (NoSong || PaeonActive && DataManager.GetSpellData(114).Cooldown.TotalSeconds < 30))
            {
                return await Spellbook.WanderersMinuet.Cast();
            }
            return false;
        }

        public async Task<bool> EmpyrealArrow()
        {
            if (Shadow.Settings.BardEmpyrealArrow)
            {
                return await Spellbook.EmpyrealArrow.Cast();
            }
            return false;
        }

        public async Task<bool> Sidewinder()
        {
            if (Shadow.Settings.BardSidewinder && Core.Player.CurrentTarget.HasAura(VenomDebuff, true) &&
                Core.Player.CurrentTarget.HasAura(WindDebuff, true))
            {
                return await Spellbook.Sidewinder.Cast();
            }
            return false;
        }

        #endregion

        #region Buff

        public async Task<bool> RagingStrikes()
        {
            if (Shadow.Settings.BardRagingStrikes)
            {
                if (MinuetActive || !ActionManager.HasSpell(Spellbook.WanderersMinuet.ID))
                {
                    return await Spellbook.RagingStrikes.Cast();
                }
            }
            return false;
        }

        public async Task<bool> Barrage()
        {
            if (Shadow.Settings.BardBarrage && Core.Player.HasAura(Spellbook.RagingStrikes.Name))
            {
                if (Spellbook.EmpyrealArrow.Cooldown() < 500 ||
                    Core.Player.HasAura(122) && ActionManager.HasSpell(Spellbook.RefulgentArrow.Name) ||
                    !ActionManager.HasSpell(Spellbook.EmpyrealArrow.Name))
                {
                    if (await Spellbook.Barrage.Cast())
                    {
                        return await Coroutine.Wait(2000, () => Core.Player.HasAura(Spellbook.Barrage.Name));
                    }
                }
            }
            return false;
        }

        public async Task<bool> BattleVoice()
        {
            if (Shadow.Settings.BardBattleVoice)
            {
                return await Spellbook.BattleVoice.Cast();
            }
            return false;
        }

        #endregion

        #region Role

        public async Task<bool> SecondWind()
        {
            if (Shadow.Settings.BardSecondWind && Core.Player.CurrentHealthPercent < Shadow.Settings.BardSecondWindPct)
            {
                return await Spellbook.Role.SecondWind.Cast();
            }
            return false;
        }

        public async Task<bool> Peloton()
        {
            if (Shadow.Settings.BardPeloton && !Core.Player.HasAura(Spellbook.Role.Peloton.Name) && !Core.Player.HasTarget &&
                (MovementManager.IsMoving || BotManager.Current.EnglishName == "DeepDive"))
            {
                return await Spellbook.Role.Peloton.Cast(null, false);
            }
            return false;
        }

        #endregion

        #region Custom

        public static string VenomDebuff => Core.Player.ClassLevel < 64 ? "Venomous Bite" : "Caustic Bite";
        public static string WindDebuff => Core.Player.ClassLevel < 64 ? "Windbite" : "Stormbite";
        public static double SongTimer => Resource.Timer.TotalMilliseconds;
        public static int NumRepertoire => Resource.Repertoire;
        public static bool NoSong => Resource.ActiveSong == Resource.BardSong.None;
        public static bool MinuetActive => Resource.ActiveSong == Resource.BardSong.WanderersMinuet;
        public static bool PaeonActive => Resource.ActiveSong == Resource.BardSong.ArmysPaeon;

        public static bool RecentSong
        {
            get { return Spell.RecentSpell.Keys.Any(rs => rs.Contains("Minuet") || rs.Contains("Ballad") || rs.Contains("Paeon")); }
        }

        #endregion
    }
}
