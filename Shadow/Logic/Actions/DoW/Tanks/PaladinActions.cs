using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Paladin;
using static ShadowCR.Constants;

namespace ShadowCR.Rotations
{
    public class PaladinActions : GladiatorActions, ITankActions
    {
        public PaladinSpells Spellbook { get; } = new PaladinSpells();

        #region Damage

        public async Task<bool> FastBlade()
        {
            return await Spellbook.FastBlade.Cast();
        }

        public async Task<bool> RiotBlade()
        {
            if (ActionManager.LastSpell.Name == Spellbook.FastBlade.Name)
            {
                return await Spellbook.RiotBlade.Cast();
            }
            return false;
        }

        public async Task<bool> GoringBlade()
        {
            if (ActionManager.LastSpell.Name == Spellbook.RiotBlade.Name &&
                !Core.Player.CurrentTarget.HasAura(Spellbook.GoringBlade.Name, true, 4000))
            {
                return await Spellbook.GoringBlade.Cast();
            }
            return false;
        }

        public async Task<bool> RoyalAuthority()
        {
            if (ActionManager.LastSpell.Name == Spellbook.RiotBlade.Name)
            {
                if (ActionManager.HasSpell(Spellbook.RoyalAuthority.Name))
                {
                    return await Spellbook.RoyalAuthority.Cast();
                }
                if (ActionManager.HasSpell(Spellbook.GoringBlade.Name))
                {
                    return await Spellbook.GoringBlade.Cast();
                }
                return await Spellbook.RageOfHalone.Cast();
            }
            return false;
        }

        public async Task<bool> HolySpirit()
        {
            if (Shadow.Settings.TankMode != TankModes.DPS || MovementManager.IsMoving) return false;

            if (Shadow.LastSpell.Name == Spellbook.Requiescat.Name || Core.Player.HasAura(Spellbook.Requiescat.Name, true, 1000))
            {
                return await Spellbook.HolySpirit.Cast();
            }
            return false;
        }

        public async Task<bool> ShieldLob()
        {
            if (Core.Player.TargetDistance(10))
            {
                return await Spellbook.ShieldLob.Cast();
            }
            return false;
        }

        #endregion

        #region AoE

        public async Task<bool> TotalEclipse()
        {
            //if (Shadow.Settings.PaladinTotalEclipse && Core.Player.CurrentTPPercent > 40)
            //{
            //    return await Spellbook.TotalEclipse.Cast();
            //}
            return false;
        }

        #endregion

        #region Cooldown

        public async Task<bool> SpiritsWithin()
        {
            if (Shadow.Settings.PaladinSpiritsWithin && Shadow.LastSpell.Name != Spellbook.CircleOfScorn.Name)
            {
                return await Spellbook.SpiritsWithin.Cast();
            }
            return false;
        }

        public async Task<bool> CircleOfScorn()
        {
            if (Shadow.Settings.PaladinCircleOfScorn && Shadow.LastSpell.Name != Spellbook.SpiritsWithin.Name)
            {
                if (Core.Player.TargetDistance(5, false))
                {
                    return await Spellbook.CircleOfScorn.Cast();
                }
            }
            return false;
        }

        public async Task<bool> Requiescat()
        {
            if (!Shadow.Settings.PaladinRequiescat || !Core.Player.CurrentTarget.HasAura(Spellbook.GoringBlade.Name, true, 12000) ||
                MovementManager.IsMoving || Core.Player.CurrentManaPercent < 80 || Shadow.LastSpell.Name == Spellbook.FightOrFlight.Name ||
                Core.Player.HasAura(Spellbook.FightOrFlight.Name))
            {
                return false;
            }

            var gcd = DataManager.GetSpellData(9).Cooldown.TotalMilliseconds;

            if (gcd == 0 || gcd > 500) return false;

            return await Spellbook.Requiescat.Cast(null, false);
        }

        #endregion

        #region Buff

        public async Task<bool> FightOrFlight()
        {
            if (!Shadow.Settings.PaladinFightOrFlight || Shadow.LastSpell.Name == Spellbook.Requiescat.Name ||
                Core.Player.HasAura(Spellbook.Requiescat.Name) || !Core.Player.TargetDistance(5, false))
            {
                return false;
            }

            return await Spellbook.FightOrFlight.Cast();
        }

        public async Task<bool> Sentinel()
        {
            if (Shadow.Settings.PaladinSentinel && Core.Player.CurrentHealthPercent < Shadow.Settings.PaladinSentinelPct)
            {
                return await Spellbook.Sentinel.Cast();
            }
            return false;
        }

        public async Task<bool> HallowedGround()
        {
            if (Shadow.Settings.PaladinHallowedGround && Core.Player.CurrentHealthPercent < Shadow.Settings.PaladinHallowedGroundPct)
            {
                return await Spellbook.HallowedGround.Cast(null, false);
            }
            return false;
        }

        public async Task<bool> Sheltron()
        {
            if (Shadow.Settings.PaladinSheltron && !Core.Player.HasAura(Spellbook.Sheltron.Name))
            {
                if (OathValue == 100 || OathValue > 50 && Core.Player.CurrentManaPercent < 70)
                {
                    return await Spellbook.Sheltron.Cast();
                }
            }
            return false;
        }

        public async Task<bool> PassageOfArms()
        {
            if (Core.Player.HasAura(Spellbook.PassageOfArms.Name))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Heal

        public async Task<bool> Clemency()
        {
            if (Shadow.Settings.PaladinClemency && Core.Player.CurrentHealthPercent < Shadow.Settings.PaladinClemencyPct)
            {
                if (Core.Player.CurrentManaPercent > 40 && !MovementManager.IsMoving)
                {
                    var target = Core.Player;

                    if (target != null)
                    {
                        return await Spellbook.Clemency.Cast(target);
                    }
                }
            }
            return false;
        }

        #endregion

        #region Oath

        public async Task<bool> IronWill()
        {
            if (!Core.Player.HasAura(Spellbook.IronWill.Name))
            {
                return await Spellbook.IronWill.Cast();
            }
            return false;
        }

        #endregion

        #region Role

        public async Task<bool> Rampart()
        {
            if (Shadow.Settings.PaladinRampart && Core.Player.CurrentHealthPercent < Shadow.Settings.PaladinRampartPct)
            {
                return await Spellbook.Role.Rampart.Cast();
            }
            return false;
        }

        public async Task<bool> Reprisal()
        {
            if (Shadow.Settings.PaladinReprisal)
            {
                return await Spellbook.Role.Reprisal.Cast();
            }
            return false;
        }


        #endregion

        #region Custom

        public static int OathValue => Resource.Oath;

        #endregion
    }
}
