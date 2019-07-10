using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Warrior;
using static ShadowCR.Constants;

namespace ShadowCR.Rotations
{
    public class WarriorActions : MarauderActions, ITankActions
    {
        public WarriorSpells Spellbook { get; } = new WarriorSpells();

        #region Damage

        public async Task<bool> HeavySwing()
        {
            return await Spellbook.HeavySwing.Cast();
        }

        public async Task<bool> Maim()
        {
            if (ActionManager.LastSpell.Name != Spellbook.HeavySwing.Name) return false;

            if (Shadow.Settings.TankMode == TankModes.DPS && ActionManager.HasSpell(Spellbook.StormsPath.Name) ||
                Shadow.Settings.WarriorMaim && !Core.Player.CurrentTarget.HasAura(819, false, 6000) ||
                ActionManager.HasSpell(Spellbook.StormsEye.Name) && UseStormsEye)
            {
                return await Spellbook.Maim.Cast();
            }
            return false;
        }

        public async Task<bool> StormsPath()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Maim.Name)
            {
                return await Spellbook.StormsPath.Cast();
            }
            return false;
        }

        public async Task<bool> StormsEye()
        {
            if (ActionManager.LastSpell.Name != Spellbook.Maim.Name || !UseStormsEye)
                return false;

            return await Spellbook.StormsEye.Cast();
        }

        public async Task<bool> InnerBeast()
        {
            if (Shadow.Settings.WarriorInnerBeast && DefianceStance && Resource.BeastGauge >= 50 && !Core.Player.HasAura(Spellbook.InnerBeast.Name))
            {
                return await Spellbook.InnerBeast.Cast();
            }
            return false;
        }

        public async Task<bool> FellCleave()
        {
            if (!Shadow.Settings.WarriorFellCleave || !DeliveranceStance) return false;

            if (Core.Player.HasAura(1177) || Resource.BeastGauge == 100 && !HeavySwingNext || Spellbook.Infuriate.Cooldown() < 6000 ||
                ActionManager.LastSpell.Name == Spellbook.Maim.Name && Resource.BeastGauge > 80 && !UseStormsEye)
            {
                return await Spellbook.FellCleave.Cast();
            }
            return false;
        }

        #endregion

        #region AoE

        public async Task<bool> Overpower()
        {
            //if (Shadow.Settings.WarriorOverpower && Core.Player.CurrentTPPercent > 30)
            //{
            //    return await Spellbook.Overpower.Cast();
            //}
            return false;
        }

        public async Task<bool> SteelCyclone()
        {
            if (Shadow.Settings.WarriorSteelCyclone && DefianceStance && Resource.BeastGauge >= 50)
            {
                return await Spellbook.SteelCyclone.Cast();
            }
            return false;
        }

        public async Task<bool> Decimate()
        {
            if (Shadow.Settings.WarriorDecimate && DeliveranceStance && Resource.BeastGauge >= 50)
            {
                return await Spellbook.Decimate.Cast();
            }
            return false;
        }

        #endregion

        #region Cooldown

        public async Task<bool> Onslaught()
        {
            if (Shadow.Settings.WarriorOnslaught && Core.Player.TargetDistance(10))
            {
                return await Spellbook.Onslaught.Cast(null, false);
            }
            return false;
        }

        public async Task<bool> Upheaval()
        {
            if (Shadow.Settings.WarriorUpheaval && Core.Player.CurrentHealthPercent > 70 &&
                (Core.Player.HasAura(Spellbook.InnerRelease.Name) || Spellbook.InnerRelease.Cooldown() > 8000) || Core.Player.ClassLevel < 70)
            {
                var count = Shadow.Settings.CustomAoE ? Shadow.Settings.CustomAoECount : 3;

                if (Shadow.Settings.RotationMode == Modes.Single ||
                    Shadow.Settings.RotationMode == Modes.Smart && Helpers.EnemiesNearTarget(5) < count)
                {
                    return await Spellbook.Upheaval.Cast();
                }
            }
            return false;
        }

        #endregion

        #region Buff

        public async Task<bool> Berserk()
        {
            if (Shadow.Settings.WarriorBerserk && Core.Player.ClassLevel < 70)
            {
                return await Spellbook.Berserk.Cast();
            }
            return false;
        }

        public async Task<bool> ThrillOfBattle()
        {
            if (Shadow.Settings.WarriorThrillOfBattle && Core.Player.CurrentHealthPercent < Shadow.Settings.WarriorThrillOfBattlePct)
            {
                return await Spellbook.ThrillOfBattle.Cast();
            }
            return false;
        }

        public async Task<bool> Vengeance()
        {
            if (Shadow.Settings.WarriorVengeance && Core.Player.CurrentHealthPercent < Shadow.Settings.WarriorVengeancePct)
            {
                return await Spellbook.Vengeance.Cast();
            }
            return false;
        }

        public async Task<bool> Infuriate()
        {
            if (Shadow.Settings.WarriorInfuriate && BeastDeficit >= 50)
            {
                return await Spellbook.Infuriate.Cast();
            }
            return false;
        }

        public async Task<bool> EquilibriumTP()
        {
            //if (Shadow.Settings.WarriorEquilibriumTP && DeliveranceStance &&
            //    Core.Player.CurrentTPPercent < Shadow.Settings.WarriorEquilibriumTPPct)
            //{
            //    return await Spellbook.Equilibrium.Cast();
            //}
            return false;
        }

        public async Task<bool> ShakeItOff()
        {
            if (Shadow.Settings.WarriorShakeItOff)
            {
                return await Spellbook.ShakeItOff.Cast(null, false);
            }
            return false;
        }

        public async Task<bool> InnerRelease()
        {
            if (!Shadow.Settings.WarriorInnerRelease || !DeliveranceStance) return false;

            var gcd = DataManager.GetSpellData(31).Cooldown.TotalMilliseconds;

            if (gcd == 0 || gcd > 700) return false;

            return await Spellbook.InnerRelease.Cast();
        }

        #endregion

        #region Heal

        public async Task<bool> Equilibrium()
        {
            if (Shadow.Settings.WarriorEquilibrium && DefianceStance &&
                Core.Player.CurrentHealthPercent < Shadow.Settings.WarriorEquilibriumPct)
            {
                return await Spellbook.Equilibrium.Cast();
            }
            return false;
        }

        #endregion

        #region Stance

        public async Task<bool> Defiance()
        {
                if (!DefianceStance)
                {
                    return await Spellbook.Defiance.Cast();
                }
            return false;
        }

        #endregion

        #region Role

        public async Task<bool> Rampart()
        {
            if (Shadow.Settings.WarriorRampart && Core.Player.CurrentHealthPercent < Shadow.Settings.WarriorRampartPct)
            {
                return await Spellbook.Role.Rampart.Cast();
            }
            return false;
        }

        public async Task<bool> Reprisal()
        {
            if (Shadow.Settings.WarriorReprisal)
            {
                return await Spellbook.Role.Reprisal.Cast();
            }
            return false;
        }

        #endregion

        #region Custom

        public static int BeastDeficit => 100 - Resource.BeastGauge;
        public static bool DefianceStance => Core.Player.HasAura(91);
        public static bool DeliveranceStance => Core.Player.HasAura(729);
        public static bool HeavySwingNext => ActionManager.LastSpellId == 42 || ActionManager.LastSpellId == 45 ||
                                              ActionManager.LastSpellId == 47;

        public int StormEyeTime => (int)Spellbook.InnerRelease.Cooldown() + 17000;
        public bool UseStormsEye => Shadow.Settings.WarriorStormsEye &&
                                     (!Core.Player.HasAura(90, true, 9000) || Core.Player.ClassLevel == 70 &&
                                      Spellbook.InnerRelease.Cooldown() < 10000 && !Core.Player.HasAura(90, true, StormEyeTime));

        #endregion
    }
}
