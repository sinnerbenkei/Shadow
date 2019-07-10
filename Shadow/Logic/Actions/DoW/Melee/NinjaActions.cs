using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Ninja;
using static ShadowCR.Constants;
using Buddy.Coroutines;

namespace ShadowCR.Rotations
{
    public class NinjaActions : RogueActions, IMeleeActions
    {
        public NinjaSpells Spellbook { get; } = new NinjaSpells();

        #region Damage

        public async Task<bool> SpinningEdge()
        {
            return await Spellbook.SpinningEdge.Cast();
        }

        public async Task<bool> GustSlash()
        {
            if (ActionManager.LastSpell.Name == Spellbook.SpinningEdge.Name)
            {
                return await Spellbook.GustSlash.Cast();
            }
            return false;
        }

        public async Task<bool> AeolianEdge()
        {
            if (ActionManager.LastSpell.Name != Spellbook.GustSlash.Name) return false;

            return await Spellbook.AeolianEdge.Cast();
        }

        public async Task<bool> ArmorCrush()
        {
            if (ActionManager.LastSpell.Name != Spellbook.GustSlash.Name || !UseArmorCrush) return false;

            return await Spellbook.ArmorCrush.Cast();
        }

        public async Task<bool> DualityActive()
        {
            return await Spellbook.AeolianEdge.Cast();
        }

        #endregion

        #region DoT

        public async Task<bool> ShadowFang()
        {
            if (ActionManager.LastSpell.Name != Spellbook.GustSlash.Name || !UseShadowFang) return false;

            return await Spellbook.ShadowFang.Cast();
        }

        #endregion

        #region AoE

        public async Task<bool> DeathBlossom()
        {
            //if (Core.Player.CurrentTPPercent > 40)
            //{
            //    return await Spellbook.DeathBlossom.Cast();
            //}
            return false;
        }

        #endregion

        #region Cooldown

        public async Task<bool> Assassinate()
        {
            if (Shadow.Settings.NinjaAssassinate && UseOffGCD)
            {
                return await Spellbook.Assassinate.Cast();
            }
            return false;
        }

        public async Task<bool> Mug()
        {
            if (Shadow.Settings.NinjaMug && UseOffGCD)
            {
                if (Resource.NinkiGauge <= 70 || Core.Player.ClassLevel < 66)
                {
                    return await Spellbook.Mug.Cast();
                }
            }
            return false;
        }

        public async Task<bool> TrickAttack()
        {
            if (Shadow.Settings.NinjaTrickAttack && UseOffGCD && !Core.Player.CurrentTarget.HasAura(638, false, 3000))
            {
                if (Core.Player.CurrentTarget.IsBehind || BotManager.Current.IsAutonomous ||
                    Core.Player.HasAura(Spellbook.Role.TrueNorth.Name) || Core.Player.HasAura(Spellbook.Suiton.Name, false, 100) &&
                    !Core.Player.HasAura(Spellbook.Suiton.Name, false, 4000))
                {
                    return await Spellbook.TrickAttack.Cast();
                }
            }
            return false;
        }

        public async Task<bool> Shukuchi()
        {
            if (Shadow.Settings.NinjaShukuchi && Core.Player.TargetDistance(10))
            {
                return await Spellbook.Shukuchi.Cast(null, false);
            }
            return false;
        }

        public async Task<bool> DreamWithinADream()
        {
            if (Shadow.Settings.NinjaDreamWithin && UseOffGCD && TrickAttackActive)
            {
                return await Spellbook.DreamWithinADream.Cast();
            }
            return false;
        }

        public async Task<bool> HellfrogMedium()
        {
            if (Shadow.Settings.NinjaHellfrogMedium && UseOffGCD)
            {
                if (Shadow.Settings.RotationMode == Modes.Multi || !ActionManager.HasSpell(Spellbook.Bhavacakra.Name) || UseHellfrog ||
                    Shadow.Settings.RotationMode == Modes.Smart && Helpers.EnemiesNearTarget(6) >= AoECount)
                {
                    return await Spellbook.HellfrogMedium.Cast();
                }
            }
            return false;
        }

        public async Task<bool> Bhavacakra()
        {
            if (Shadow.Settings.NinjaBhavacakra && UseOffGCD)
            {
                if (Shadow.Settings.RotationMode == Modes.Single || Shadow.Settings.RotationMode == Modes.Smart &&
                    Helpers.EnemiesNearTarget(6) < AoECount)
                {
                    return await Spellbook.Bhavacakra.Cast();
                }
            }
            return false;
        }

        #endregion

        #region Buff

        public async Task<bool> ShadeShift()
        {
            if (Shadow.Settings.NinjaShadeShift && Core.Player.CurrentHealthPercent < Shadow.Settings.NinjaShadeShiftPct)
            {
                return await Spellbook.ShadeShift.Cast();
            }
            return false;
        }

        public async Task<bool> Kassatsu()
        {
            if (Shadow.Settings.NinjaKassatsu && UseOffGCD)
            {
                if (TrickAttackActive || Shadow.Settings.RotationMode == Modes.Multi || TrickCooldown > 30000 ||
                    Shadow.Settings.RotationMode == Modes.Smart && Helpers.EnemiesNearTarget(5) >= AoECount)
                {
                    return await Spellbook.Kassatsu.Cast();
                }
            }
            return false;
        }

        public async Task<bool> TenChiJin()
        {
            if (Shadow.Settings.NinjaTenChiJin && UseOffGCD && !MovementManager.IsMoving)
            {
                return await Spellbook.TenChiJin.Cast();
            }
            return false;
        }

        #endregion

        #region Ninjutsu

        public static bool UseNinjutsu(bool targetSelf = false, int range = 15)
        {
            return Core.Player.HasAura(496) ||
                   (NinjutsuGcd > 1000 || NinjutsuGcd <= 0 && !ActionManager.CanCast(2240, Core.Player.CurrentTarget)) &&
                   (targetSelf || Core.Player.HasTarget && Core.Player.CurrentTarget.CanAttack &&
                    Core.Player.TargetDistance(range, false) && Core.Player.CurrentTarget.InLineOfSight());
        }

        #region Fuma

        public async Task<bool> FumaShuriken()
        {
            if (Shadow.Settings.NinjaFuma && UseNinjutsu() && ActionManager.CanCast(Spellbook.Ten.Name, null) &&
                (Spellbook.TrickAttack.Cooldown() > 22000 || !ActionManager.HasSpell(Spellbook.Suiton.Name)))
            {
                if (!CanNinjutsu)
                {
                    if (await Spellbook.Ten.Cast())
                    {
                        await Coroutine.Wait(2000, () => CanNinjutsu);
                    }
                }
                if (CanNinjutsu && LastTen)
                {
                    if (await Spellbook.FumaShuriken.Cast())
                    {
                        await Coroutine.Wait(2000, () => !Core.Player.HasAura("Mudra"));
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region Katon

        public async Task<bool> Katon()
        {
            if (Shadow.Settings.NinjaKaton && UseNinjutsu() && ActionManager.CanCast(Spellbook.Chi.ID, null))
            {
                if (Shadow.Settings.RotationMode == Modes.Multi || Helpers.EnemiesNearTarget(5) >= AoECount)
                {
                    if (!CanNinjutsu)
                    {
                        if (await Spellbook.Chi.Cast())
                        {
                            await Coroutine.Wait(2000, () => CanNinjutsu);
                        }
                    }
                    if (LastChi)
                    {
                        if (await Spellbook.Ten.Cast())
                        {
                            await Coroutine.Wait(2000, () => CanNinjutsu);
                        }
                    }
                    if (LastTen)
                    {
                        if (await Spellbook.Katon.Cast())
                        {
                            await Coroutine.Wait(2000, () => !Core.Player.HasAura(496));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        #region Raiton

        public async Task<bool> Raiton()
        {
            if (Shadow.Settings.NinjaRaiton && UseNinjutsu() && ActionManager.CanCast(Spellbook.Chi.ID, null) &&
                (Spellbook.TrickAttack.Cooldown() > 22000 || !ActionManager.HasSpell(Spellbook.Suiton.Name)))
            {
                if (!CanNinjutsu)
                {
                    if (await Spellbook.Ten.Cast())
                    {
                        await Coroutine.Wait(2000, () => CanNinjutsu);
                    }
                }
                if (LastTen)
                {
                    if (await Spellbook.Chi.Cast())
                    {
                        await Coroutine.Wait(2000, () => CanNinjutsu);
                    }
                }
                if (LastChi)
                {
                    if (await Spellbook.Raiton.Cast())
                    {
                        await Coroutine.Wait(2000, () => !Core.Player.HasAura(496));
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region Huton

        public async Task<bool> Huton()
        {
            if (Shadow.Settings.NinjaHuton && UseNinjutsu(true) && ActionManager.CanCast(Spellbook.Jin.ID, null) &&
                Resource.HutonTimer.TotalMilliseconds < 20000)
            {
                if (Core.Player.InCombat || Core.Player.HasTarget && Core.Player.CurrentTarget.CanAttack)
                {
                    if (!CanNinjutsu)
                    {
                        if (await Spellbook.Jin.Cast())
                        {
                            await Coroutine.Wait(2000, () => CanNinjutsu);
                        }
                    }
                    if (LastJin)
                    {
                        if (await Spellbook.Chi.Cast())
                        {
                            await Coroutine.Wait(2000, () => CanNinjutsu);
                        }
                    }
                    if (LastChi)
                    {
                        if (await Spellbook.Ten.Cast())
                        {
                            await Coroutine.Wait(2000, () => CanNinjutsu);
                        }
                    }
                    if (LastTen)
                    {
                        if (await Spellbook.Huton.Cast())
                        {
                            await Coroutine.Wait(2000, () => !Core.Player.HasAura(496));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        #region Doton

        public async Task<bool> Doton()
        {
            if (Shadow.Settings.NinjaDoton && UseNinjutsu() && ActionManager.CanCast(Spellbook.Jin.ID, null) && !MovementManager.IsMoving &&
                !Core.Player.HasAura(Spellbook.Doton.Name, true, 5000))
            {
                if (Shadow.Settings.RotationMode == Modes.Multi || Helpers.EnemiesNearTarget(5) >= AoECount)
                {
                    if (!CanNinjutsu)
                    {
                        if (await Spellbook.Ten.Cast())
                        {
                            await Coroutine.Wait(2000, () => CanNinjutsu);
                        }
                    }
                    if (LastTen)
                    {
                        if (await Spellbook.Jin.Cast())
                        {
                            await Coroutine.Wait(2000, () => CanNinjutsu);
                        }
                    }
                    if (LastJin)
                    {
                        if (await Spellbook.Chi.Cast())
                        {
                            await Coroutine.Wait(2000, () => CanNinjutsu);
                        }
                    }
                    if (LastChi)
                    {
                        if (await Spellbook.Doton.Cast())
                        {
                            await Coroutine.Wait(2000, () => !Core.Player.HasAura(496));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        #region Suiton

        public async Task<bool> Suiton()
        {
            if (Shadow.Settings.NinjaSuiton && UseNinjutsu() && ActionManager.CanCast(Spellbook.Jin.ID, null) &&
                !Core.Player.HasAura(Spellbook.Suiton.Name) && TrickCooldown < 9000)
            {
                if (!CanNinjutsu)
                {
                    if (await Spellbook.Ten.Cast())
                    {
                        await Coroutine.Wait(2000, () => CanNinjutsu);
                    }
                }
                if (LastTen)
                {
                    if (await Spellbook.Chi.Cast())
                    {
                        await Coroutine.Wait(2000, () => CanNinjutsu);
                    }
                }
                if (LastChi)
                {
                    if (await Spellbook.Jin.Cast())
                    {
                        await Coroutine.Wait(2000, () => CanNinjutsu);
                    }
                }
                if (LastJin)
                {
                    if (await Spellbook.Suiton.Cast())
                    {
                        await Coroutine.Wait(2000, () => !Core.Player.HasAura(496));
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        public async Task<bool> TenChiJinBuff()
        {
            if (!Core.Player.HasAura(Spellbook.TenChiJin.Name)) return false;

            #region Fuma

            if (!Core.Player.HasAura("Mudra"))
            {
                if (TrickCooldown > 9000 && await Spellbook.Jin.Cast() || await Spellbook.Ten.Cast())
                    await Coroutine.Wait(2000, () => CanNinjutsu);
            }

            if (LastJin || LastTen)
            {
                if (await Spellbook.FumaShuriken.Cast())
                    await Coroutine.Wait(2000, () => !CanNinjutsu);
            }

            #endregion

            #region Raiton / Katon

            if (Shadow.LastSpell.ID == Spellbook.FumaShuriken.ID)
            {
                if (TrickCooldown > 9000 && await Spellbook.Ten.Cast() || await Spellbook.Chi.Cast())
                    await Coroutine.Wait(2000, () => CanNinjutsu);
            }

            if (LastChi && await Spellbook.Raiton.Cast())
                await Coroutine.Wait(2000, () => !CanNinjutsu);

            if (LastTen && await Spellbook.Katon.Cast())
                await Coroutine.Wait(2000, () => !CanNinjutsu);

            #endregion

            #region Suiton / Doton

            if (Shadow.LastSpell.ID == Spellbook.Raiton.ID && await Spellbook.Jin.Cast())
                await Coroutine.Wait(2000, () => CanNinjutsu);

            if (Shadow.LastSpell.ID == Spellbook.Katon.ID && await Spellbook.Chi.Cast())
                await Coroutine.Wait(2000, () => CanNinjutsu);

            if (LastJin && await Spellbook.Suiton.Cast())
                await Coroutine.Wait(2000, () => !CanNinjutsu);

            if (LastChi && await Spellbook.Doton.Cast())
                await Coroutine.Wait(2000, () => !CanNinjutsu);

            #endregion

            return true;
        }

        #endregion

        #region Role

        public async Task<bool> SecondWind()
        {
            if (Shadow.Settings.NinjaSecondWind && Core.Player.CurrentHealthPercent < Shadow.Settings.NinjaSecondWindPct)
            {
                return await Spellbook.Role.SecondWind.Cast();
            }
            return false;
        }

        public async Task<bool> Bloodbath()
        {
            if (Shadow.Settings.NinjaBloodbath && Core.Player.CurrentHealthPercent < Shadow.Settings.NinjaBloodbathPct)
            {
                return await Spellbook.Role.Bloodbath.Cast();
            }
            return false;
        }

        public async Task<bool> TrueNorth()
        {
            if (Shadow.Settings.NinjaTrueNorth)
            {
                return await Spellbook.Role.TrueNorth.Cast();
            }
            return false;
        }

        #endregion

        #region Custom

        public static int AoECount => Shadow.Settings.CustomAoE ? Shadow.Settings.CustomAoECount : 2;
        public static bool TrickAttackActive => Core.Player.CurrentTarget.HasAura(638);
        public static bool UseOffGCD => DataManager.GetSpellData(2260).Cooldown.TotalMilliseconds > 1000 || Core.Player.ClassLevel < 30;
        public static bool UseHellfrog => Resource.NinkiGauge == 100 && BhavacakraCooldown > 10000 && TenChiJinCooldown > 10000;
        public static bool UseArmorCrush => Resource.HutonTimer.TotalMilliseconds > 0 && Resource.HutonTimer.TotalMilliseconds < 40000;
        public static bool UseShadowFang => Shadow.Settings.NinjaShadowFang && !Core.Player.CurrentTarget.HasAura(508, true, 6000) &&
                                             (Core.Player.CurrentTarget.IsBoss() ||
                                              Core.Player.CurrentTarget.CurrentHealth >
                                              Shadow.Settings.NinjaShadowFangHP);

        public static double TrickCooldown => DataManager.GetSpellData(2258).Cooldown.TotalMilliseconds;
        public static double NinjutsuGcd => DataManager.GetSpellData(2240).Cooldown.TotalMilliseconds;
        public static double BhavacakraCooldown => ActionManager.HasSpell(7402) ? DataManager.GetSpellData(7402).Cooldown.TotalMilliseconds
            : 50000;
        public static double TenChiJinCooldown => ActionManager.HasSpell(7403) ? DataManager.GetSpellData(7403).Cooldown.TotalMilliseconds
            : 100000;

        public bool CanNinjutsu => ActionManager.CanCast(Spellbook.Ninjutsu.ID, null);
        public bool LastTen => Shadow.LastSpell.ID == Spellbook.Ten.ID;
        public bool LastChi => Shadow.LastSpell.ID == Spellbook.Chi.ID;
        public bool LastJin => Shadow.LastSpell.ID == Spellbook.Jin.ID;

        #endregion
    }
}
