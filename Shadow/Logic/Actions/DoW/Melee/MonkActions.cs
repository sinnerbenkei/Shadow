using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Monk;
using static ShadowCR.Constants;

namespace ShadowCR.Rotations
{
    public class MonkActions : PugilistActions, IMeleeActions
    {
        public MonkSpells Spellbook { get; } = new MonkSpells();

        #region Damage

        public async Task<bool> Bootshine()
        {
            return await Spellbook.Bootshine.Cast();
        }

        public async Task<bool> TrueStrike()
        {
            if (RaptorForm || BalanceActive)
            {
                return await Spellbook.TrueStrike.Cast();
            }
            return false;
        }

        public async Task<bool> SnapPunch()
        {
            if (CoeurlForm || BalanceActive && (Resource.GreasedLightning < 3 || Resource.Timer < TimeSpan.FromMilliseconds(6000)))
            {
                return await Spellbook.SnapPunch.Cast();
            }
            return false;
        }

        public async Task<bool> TwinSnakes()
        {
            if ((RaptorForm || BalanceActive) && !Core.Player.HasAura(Spellbook.TwinSnakes.Name, true, 6000))
            {
                return await Spellbook.TwinSnakes.Cast();
            }
            return false;
        }

        public async Task<bool> DragonKick()
        {
            if ((OpoOpoForm || BalanceActive) && !Core.Player.CurrentTarget.HasAura(821, false, 6000))
            {
                return await Spellbook.DragonKick.Cast();
            }
            return false;
        }

        #endregion

        #region DoT

        public async Task<bool> Demolish()
        {
            if (Shadow.Settings.MonkDemolish && (Core.Player.CurrentTarget.IsBoss() ||
                                                 Core.Player.CurrentTarget.CurrentHealth > Shadow.Settings.MonkDemolishHP))
            {
                if ((CoeurlForm || BalanceActive) && !Core.Player.CurrentTarget.HasAura(Spellbook.Demolish.Name, true, 6000))
                {
                    return await Spellbook.Demolish.Cast();
                }
            }
            return false;
        }

        #endregion

        #region AoE

        public async Task<bool> Rockbreaker()
        {
            if (CoeurlForm || BalanceActive && Core.Player.HasAura(Spellbook.TwinSnakes.Name))
            {
                return await Spellbook.Rockbreaker.Cast();
            }
            return false;
        }

        #endregion

        #region Cooldown

        public async Task<bool> ShoulderTackle()
        {
            if (Shadow.Settings.MonkShoulderTackle && Core.Player.TargetDistance(10))
            {
                return await Spellbook.ShoulderTackle.Cast(null, false);
            }
            return false;
        }

        public async Task<bool> ForbiddenChakra()
        {
            if (Shadow.Settings.MonkForbiddenChakra && Resource.FithChakra == 5)
            {
                return await Spellbook.ForbiddenChakra.Cast(null, false);
            }
            return false;
        }

        public async Task<bool> ElixirField()
        {
            if (Shadow.Settings.MonkElixirField && Core.Player.TargetDistance(5, false))
            {
                return await Spellbook.ElixirField.Cast();
            }
            return false;
        }


        #endregion

        #region Buff


        public async Task<bool> PerfectBalance()
        {
            if (Shadow.Settings.MonkPerfectBalance)
            {
                return await Spellbook.PerfectBalance.Cast();
            }
            return false;
        }

        public async Task<bool> FormShift()
        {
            if (!Shadow.Settings.MonkFormShift || !Core.Player.HasTarget || CoeurlForm) return false;

            return await Spellbook.FormShift.Cast();
        }

        public async Task<bool> Meditation()
        {
            if (!Shadow.Settings.MonkMeditation || Resource.FithChakra == 5) return false;

            return await Spellbook.Meditation.Cast();
        }

        public async Task<bool> RiddleOfFire()
        {
            if (Shadow.Settings.MonkRiddleOfFire)
            {
                return await Spellbook.RiddleOfFire.Cast();
            }
            return false;
        }

        public async Task<bool> Brotherhood()
        {
            if (Shadow.Settings.MonkBrotherhood)
            {
                return await Spellbook.Brotherhood.Cast();
            }
            return false;
        }

        #endregion

        #region Fists

        public async Task<bool> FistsOfEarth()
        {
            if (Shadow.Settings.MonkFist == MonkFists.Earth ||
                Shadow.Settings.MonkFist == MonkFists.Wind && !ActionManager.HasSpell(Spellbook.FistsOfWind.Name) ||
                Shadow.Settings.MonkFist == MonkFists.Fire && !ActionManager.HasSpell(Spellbook.FistsOfFire.Name))
            {
                if (!Core.Player.HasAura(Spellbook.FistsOfEarth.Name))
                {
                    return await Spellbook.FistsOfEarth.Cast(null, false);
                }
            }
            return false;
        }

        public async Task<bool> FistsOfWind()
        {
            if (Shadow.Settings.MonkFist == MonkFists.Wind && !Core.Player.HasAura(Spellbook.FistsOfWind.Name) &&
                !Core.Player.HasAura(Spellbook.RiddleOfEarth.Name))
            {
                return await Spellbook.FistsOfWind.Cast(null, false);
            }
            return false;
        }

        public async Task<bool> FistsOfFire()
        {
            if (Shadow.Settings.MonkFist == MonkFists.Fire && !Core.Player.HasAura(Spellbook.FistsOfFire.Name) &&
                !Core.Player.HasAura(Spellbook.RiddleOfEarth.Name))
            {
                return await Spellbook.FistsOfFire.Cast(null, false);
            }
            return false;
        }

        #endregion

        #region Role

        public async Task<bool> SecondWind()
        {
            if (Shadow.Settings.MonkSecondWind && Core.Player.CurrentHealthPercent < Shadow.Settings.MonkSecondWindPct)
            {
                return await Spellbook.Role.SecondWind.Cast();
            }
            return false;
        }

        public async Task<bool> Bloodbath()
        {
            if (Shadow.Settings.MonkBloodbath && Core.Player.CurrentHealthPercent < Shadow.Settings.MonkBloodbathPct)
            {
                return await Spellbook.Role.Bloodbath.Cast();
            }
            return false;
        }

        public async Task<bool> TrueNorth()
        {
            if (Shadow.Settings.MonkTrueNorth && Core.Player.TargetDistance(5, false))
            {
                return await Spellbook.Role.TrueNorth.Cast();
            }
            return false;
        }

        #endregion

        #region Custom

        public static bool OpoOpoForm => Core.Player.HasAura(107);
        public static bool RaptorForm => Core.Player.HasAura(108);
        public static bool CoeurlForm => Core.Player.HasAura(109);
        public static bool BalanceActive => Core.Player.HasAura(110);

        #endregion
    }
}
