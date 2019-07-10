using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Gunbreaker;

namespace ShadowCR.Rotations
{
    public class GunbreakerActions : ITankActions
    {
        public GunbreakerSpells Spellbook { get; } = new GunbreakerSpells();


        #region Damage

        public async Task<bool> LightningShot()
        {
            return await Spellbook.LightningShot.Cast();
        }
        public async Task<bool> KeenEdge()
        {
            return await Spellbook.KeenEdge.Cast();
        }
        public async Task<bool> BrutalShell()
        {
            if (ActionManager.LastSpell.Name == Spellbook.KeenEdge.Name)
            {
                return await Spellbook.BrutalShell.Cast();
            }
            return false;
        }
        public async Task<bool> SolidBarrel()
        {
            if (ActionManager.LastSpell.Name == Spellbook.BrutalShell.Name)
            {
                return await Spellbook.SolidBarrel.Cast();
            }
            return false;
        }
        public async Task<bool> DemonSlice()
        {
            return await Spellbook.DemonSlice.Cast();
        }
        public async Task<bool> DemonSlaughter()
        {
            if (ActionManager.LastSpell.Name == Spellbook.DemonSlice.Name)
            {
                return await Spellbook.DemonSlaughter.Cast();
            }
            return false;
        }

        public async Task<bool> SonicBreak()
        {
            return await Spellbook.SonicBreak.Cast();
        }

        public async Task<bool> DangerZone()
        {
            return await Spellbook.DangerZone.Cast();
        }

        //public async Task<bool> BlastingZone()
        //{
        //    return await Spellbook.BlastingZone.Cast();
        //}

        public async Task<bool> BurstStrike()
        {
            if (Cartridge > 0)
            {
                return await Spellbook.BurstStrike.Cast();
            }
            return false;
        }

        public async Task<bool> GnashingFang()
        {
            if (Cartridge > 0)
            {
                return await Spellbook.GnashingFang.Cast();
            }
            return false;
        }

        //public async Task<bool> FatedCircle()
        //{
        //    if (Cartridge > 0)
        //    {
        //        return await Spellbook.FatedCircle.Cast();
        //    }
        //    return false;
        //}

        public async Task<bool> SavageClaw()
        {
            return await Spellbook.SavageClaw.Cast();
        }

        public async Task<bool> WickedTalon()
        {
            return await Spellbook.WickedTalon.Cast();
        }

        public async Task<bool> Continuation()
        {
            return await Spellbook.Continuation.Cast();
        }

        public async Task<bool> JugularRip()
        {
            return await Spellbook.JugularRip.Cast();
        }

        public async Task<bool> AbdomenTear()
        {
            return await Spellbook.AbdomenTear.Cast();
        }

        public async Task<bool> EyeGouge()
        {
            return await Spellbook.EyeGouge.Cast();
        }

        public async Task<bool> BowShock()
        {
            return await Spellbook.BowShock.Cast();
        }

        #endregion


        #region Aura

        public async Task<bool> RoyalGuard()
        {
            if (!Core.Player.HasAura(Spellbook.RoyalGuard.Name))
            {
                return await Spellbook.RoyalGuard.Cast();
            }
            return false;
        }

        #endregion

        #region Cooldown

        public async Task<bool> NoMercy()
        {
            return await Spellbook.NoMercy.Cast();
        }

        //public async Task<bool> Nebula()
        //{
        //    if (Core.Player.CurrentHealthPercent < 70)
        //    {
        //        return await Spellbook.Nebula.Cast(Core.Player);
        //    }
        //    return false;
        //}

        //public async Task<bool> Camouflage()
        //{
        //    if (Core.Player.CurrentHealthPercent < 50)
        //    {
        //        return await Spellbook.Camouflage.Cast();
        //    }
        //    return false;
        //}

        //public async Task<bool> HeartOfStone()
        //{
        //    if (Core.Player.CurrentHealthPercent < 30)
        //    {
        //        return await Spellbook.HeartOfStone.Cast(Core.Player);
        //    }
        //    return false;
        //}

        //public async Task<bool> HeartOfLight()
        //{
        //    if (Core.Player.CurrentHealthPercent < 30)
        //    {
        //        return await Spellbook.HeartOfLight.Cast();
        //    }
        //    return false;
        //}

        //public async Task<bool> Superbolide()
        //{
        //    if (Core.Player.CurrentHealthPercent < 10)
        //    {
        //        return await Spellbook.Superbolide.Cast(Core.Player);
        //    }
        //    return false;
        //}

        //public async Task<bool> Aurora()
        //{
        //    if (Core.Player.CurrentHealthPercent < 70)
        //    {
        //        return await Spellbook.Aurora.Cast(Core.Player);
        //    }
        //    return false;
        //}
        //public async Task<bool> Bloodfest()
        //{
        //    if (Cartridge == 0)
        //    {
        //        return await Spellbook.Bloodfest.Cast(Core.Player);
        //    }
        //    return false;
        //}

        #endregion


        #region Role

        public async Task<bool> Rampart()
        {
            return false;
        }

        public async Task<bool> Convalescence()
        {
            return false;
        }

        public async Task<bool> Anticipation()
        {
            return false;
        }

        public async Task<bool> Reprisal()
        {
            return false;
        }

        public async Task<bool> Awareness()
        {
            return false;
        }

        #endregion


        #region Custom

        public static int Cartridge => Resource.Cartridge;

        #endregion
    }
}
