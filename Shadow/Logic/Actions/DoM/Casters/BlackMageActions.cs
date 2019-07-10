using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.BlackMage;
using static ShadowCR.Constants;
using Buddy.Coroutines;

namespace ShadowCR.Rotations
{
    public class BlackMageActions : ThaumaturgeActions, ICasterActions
    {
        public BlackMageSpells Spellbook { get; } = new BlackMageSpells();


        #region Damage

        public async Task<bool> Blizzard()
        {
            if (!ActionManager.HasSpell(Spellbook.BlizzardIII.Name) && (!UmbralIce || !ActionManager.HasSpell(Spellbook.Fire.Name)))
            {
                return await Spellbook.Blizzard.Cast();
            }
            return false;
        }

        public async Task<bool> BlizzardIII()
        {
            if (!UmbralIce)
            {
                return await Spellbook.BlizzardIII.Cast();
            }
            return false;
        }

        public async Task<bool> BlizzardIV()
        {
            if (UmbralIce && Resource.UmbralHearts < 3)
            {
                return await Spellbook.BlizzardIV.Cast();
            }
            return false;
        }

        public async Task<bool> Fire()
        {
            if (Core.Player.CurrentManaPercent > 10)
            {
                if (AstralFire || Core.Player.ClassLevel < 34 && Core.Player.CurrentManaPercent > 80)
                {
                    return await Spellbook.Fire.Cast();
                }
            }
            return false;
        }

        public async Task<bool> FireIII()
        {
            if (!AstralFire && Core.Player.CurrentManaPercent > 80 || AstralFire && Core.Player.HasAura("Firestarter"))
            {
                return await Spellbook.FireIII.Cast();
            }
            return false;
        }

        public async Task<bool> FireIV()
        {
            if (Resource.StackTimer.TotalMilliseconds > 6000)
            {
                return await Spellbook.FireIV.Cast();
            }
            return false;
        }

        public async Task<bool> Foul()
        {
            if (UmbralIce)
            {
                return await Spellbook.Foul.Cast();
            }
            return false;
        }

        public async Task<bool> Scathe()
        {
            if (Shadow.Settings.BlackMageScathe && MovementManager.IsMoving && Core.Player.CurrentManaPercent > 20)
            {
                if (Resource.StackTimer.TotalMilliseconds > 8000 || Resource.StackTimer.TotalMilliseconds == 0)
                {
                    return await Spellbook.Scathe.Cast();
                }
            }
            return false;
        }

        #endregion

        #region DoT

        public async Task<bool> Thunder()
        {
            if (!ActionManager.HasSpell(Spellbook.ThunderIII.Name))
            {
                if (UmbralIce && !Core.Player.CurrentTarget.HasAura(Spellbook.Thunder.Name, true, 10000) ||
                    Core.Player.HasAura("Thundercloud"))
                {
                    return await Spellbook.Thunder.Cast();
                }
            }
            return false;
        }

        public async Task<bool> ThunderIII()
        {
            if (UmbralIce && !Core.Player.CurrentTarget.HasAura(Spellbook.ThunderIII.Name, true, 12000))
            {
                return await Spellbook.ThunderIII.Cast();
            }
            return false;
        }

        public async Task<bool> Thundercloud()
        {
            if (Core.Player.HasAura("Thundercloud") && Resource.StackTimer.TotalMilliseconds > 6000)
            {
                if (!Core.Player.CurrentTarget.HasAura(Spellbook.ThunderIII.Name, true, 3000) ||
                    !Core.Player.HasAura("Thundercloud", false, 3000))
                {
                    return await Spellbook.ThunderIII.Cast();
                }
            }
            return false;
        }

        #endregion

        #region AoE

        public async Task<bool> BlizzardMulti()
        {
            if (!AstralFire && !UmbralIce || !ActionManager.HasSpell(Spellbook.Flare.Name))
            {
                return await Spellbook.Blizzard.Cast();
            }
            return false;
        }

        public async Task<bool> FireMulti()
        {
            if (Core.Player.ClassLevel < 18 && (AstralFire || Core.Player.CurrentManaPercent > 80))
            {
                return await Spellbook.Fire.Cast();
            }
            return false;
        }

        public async Task<bool> FireII()
        {
            if (AstralFire || Core.Player.ClassLevel < 34 && Core.Player.CurrentManaPercent > 80)
            {
                if (ActionManager.CanCast(Spellbook.FireII.Name, Core.Player.CurrentTarget))
                {
                    if (Shadow.Settings.BlackMageTriplecast && ActionManager.LastSpell.Name == Spellbook.FireIII.Name)
                    {
                        if (await Spellbook.Triplecast.Cast(null, false))
                        {
                            await Coroutine.Wait(3000, () => Core.Player.HasAura(Spellbook.Triplecast.Name));
                        }
                    }
                }
                return await Spellbook.FireII.Cast();
            }
            return false;
        }

        public async Task<bool> FireIIIMulti()
        {
            if (!AstralFire && Core.Player.CurrentManaPercent > 25 &&
                (ActionManager.HasSpell(Spellbook.Flare.Name) || Core.Player.CurrentManaPercent > 80))
            {
                Spell.RecentSpell.RemoveAll(t => DateTime.UtcNow > t);
                if (!RecentTranspose)
                {
                    return await Spellbook.FireIII.Cast();
                }
            }
            return false;
        }

        public async Task<bool> Flare()
        {
            if (AstralFire && (Core.Player.CurrentManaPercent < 25 || Core.Player.ClassLevel > 67 && Resource.UmbralHearts > 0))
            {
                if (Shadow.Settings.BlackMageConvert && ActionManager.HasSpell(Spellbook.Flare.Name) &&
                    !ActionManager.CanCast(Spellbook.Flare.Name, Core.Player.CurrentTarget))
                {
                    if (await Spellbook.Convert.Cast(null, false))
                    {
                        await Coroutine.Wait(3000, () => ActionManager.CanCast(Spellbook.Flare.Name, Core.Player.CurrentTarget));
                    }
                }
                if (Shadow.Settings.BlackMageSwiftcast && ActionManager.CanCast(Spellbook.Flare.Name, Core.Player.CurrentTarget) &&
                    !RecentTriplecast)
                {
                    if (await Spellbook.Role.Swiftcast.Cast(null, false))
                    {
                        await Coroutine.Wait(3000, () => Core.Player.HasAura(Spellbook.Role.Swiftcast.Name));
                    }
                }
                return await Spellbook.Flare.Cast();
            }
            return false;
        }

        public async Task<bool> TransposeMulti()
        {
            if (AstralFire && Core.Player.CurrentManaPercent < 20 && !ActionManager.CanCast(Spellbook.Flare.Name, Core.Player.CurrentTarget))
            {
                if (await Spellbook.Transpose.Cast(null, false))
                {
                    Spell.RecentSpell.Add(Spellbook.Transpose.Name, DateTime.UtcNow + TimeSpan.FromSeconds(4));
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> ThunderII()
        {
            if (Shadow.Settings.BlackMageThunder && !ActionManager.HasSpell(Spellbook.ThunderIV.Name))
            {
                if (UmbralIce && !Core.Player.CurrentTarget.HasAura(Spellbook.ThunderII.Name, true, 4000) || Core.Player.HasAura("Thundercloud"))
                {
                    return await Spellbook.ThunderII.Cast();
                }
            }
            return false;
        }

        public async Task<bool> ThunderIV()
        {
            if (Shadow.Settings.BlackMageThunder)
            {
                if (UmbralIce && !Core.Player.CurrentTarget.HasAura(Spellbook.ThunderIV.Name, true, 4000) || Core.Player.HasAura("Thundercloud"))
                {
                    return await Spellbook.ThunderIV.Cast();
                }
            }
            return false;
        }

        #endregion

        #region Buff

        public async Task<bool> Transpose()
        {
            if (AstralFire && Core.Player.CurrentManaPercent < 20 &&
                (!ActionManager.HasSpell(Spellbook.BlizzardIII.Name) || Core.Player.CurrentMana < BlizzardIIICost))
            {
                return await Spellbook.Transpose.Cast(null, false);
            }
            return false;
        }

        public async Task<bool> Convert()
        {
            if (Shadow.Settings.BlackMageConvert && AstralFire && ActionManager.LastSpell.Name == Spellbook.FireIII.Name)
            {
                return await Spellbook.Convert.Cast();
            }
            return false;
        }

        public async Task<bool> LeyLines()
        {
            if (Shadow.Settings.BlackMageLeyLines && !MovementManager.IsMoving)
            {
                if (Core.Player.CurrentManaPercent > 80 || ActionManager.LastSpell.Name == Spellbook.FireII.Name)
                {
                    return await Spellbook.LeyLines.Cast(null, false);
                }
            }
            return false;
        }

        public async Task<bool> Sharpcast()
        {
            if (Shadow.Settings.BlackMageSharpcast && AstralFire && Core.Player.CurrentManaPercent > 60 && !Core.Player.HasAura("Firestarter"))
            {
                return await Spellbook.Sharpcast.Cast();
            }
            return false;
        }

        public async Task<bool> Enochian()
        {
            if (Shadow.Settings.BlackMageEnochian && Core.Player.ClassLevel >= 60 && !Resource.Enochian &&
                Resource.StackTimer.TotalMilliseconds > 6000)
            {
                return await Spellbook.Enochian.Cast(null, false);
            }
            return false;
        }

        public async Task<bool> Triplecast()
        {
            if (Shadow.Settings.BlackMageTriplecast && ActionManager.LastSpell.Name == Spellbook.FireIII.Name && Core.Player.CurrentManaPercent > 80)
            {
                if (await Spellbook.Triplecast.Cast(null, false))
                {
                    await Coroutine.Wait(3000, () => Core.Player.HasAura(Spellbook.Triplecast.Name));
                }
            }
            return false;
        }

        #endregion

        #region Role

        public async Task<bool> LucidDreaming()
        {
            if (Shadow.Settings.BlackMageLucidDreaming && Core.Player.CurrentManaPercent < Shadow.Settings.BlackMageLucidDreamingPct)
            {
                return await Spellbook.Role.LucidDreaming.Cast();
            }
            return false;
        }

        public async Task<bool> Swiftcast()
        {
            if (Shadow.Settings.BlackMageSwiftcast && AstralFire && Resource.StackTimer.TotalMilliseconds > 8000 && !RecentTriplecast &&
                Core.Player.CurrentManaPercent > 40)
            {
                if (await Spellbook.Role.Swiftcast.Cast(null, false))
                {
                    await Coroutine.Wait(3000, () => Core.Player.HasAura(Spellbook.Role.Swiftcast.Name));
                }
            }
            return false;
        }

        #endregion

        #region Custom

        public static double ManaReduction => Resource.AstralStacks > 1 ? 0.25 : Resource.AstralStacks > 0 ? 0.5 : 1;
        public static double BlizzardIIICost => DataManager.GetSpellData("Blizzard III").Cost * ManaReduction;

        public static bool RecentTranspose { get { return Spell.RecentSpell.Keys.Any(rs => rs.Contains("Transpose")); } }
        public static bool RecentTriplecast => Core.Player.HasAura(1211) || Shadow.LastSpell.ID == 7421;
        public static bool AstralFire => Resource.AstralStacks > 0 && Shadow.LastSpell.Name != "Transpose";
        public static bool UmbralIce => Resource.UmbralStacks > 0;

        #endregion
    }
}
