using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Samurai;
using static ShadowCR.Constants;
using Buddy.Coroutines;

namespace ShadowCR.Rotations
{
    public class SamuraiActions : IMeleeActions
    {
        public SamuraiSpells Spellbook { get; } = new SamuraiSpells();

        #region Damage

        public async Task<bool> Hakaze()
        {
            return await Spellbook.Hakaze.Cast();
        }

        public async Task<bool> Jinpu()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Hakaze.Name && !GetsuActive)
            {
                return await Spellbook.Jinpu.Cast();
            }
            return false;
        }

        public async Task<bool> JinpuBuff()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Hakaze.Name && !Core.Player.HasAura(Spellbook.Jinpu.Name, true, 8000))
            {
                return await Spellbook.Jinpu.Cast();
            }
            return false;
        }

        public async Task<bool> Gekko()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Jinpu.Name)
            {
                return await Spellbook.Gekko.Cast();
            }
            return false;
        }

        public async Task<bool> Shifu()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Hakaze.Name && !KaActive)
            {
                return await Spellbook.Shifu.Cast();
            }
            return false;
        }

        public async Task<bool> ShifuBuff()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Hakaze.Name && !Core.Player.HasAura(Spellbook.Shifu.Name, true, 8000))
            {
                return await Spellbook.Shifu.Cast();
            }
            return false;
        }

        public async Task<bool> Kasha()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Shifu.Name)
            {
                return await Spellbook.Kasha.Cast();
            }
            return false;
        }

        public async Task<bool> Yukikaze()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Hakaze.Name)
            {
                return await Spellbook.Yukikaze.Cast();
            }
            return false;
        }

        public async Task<bool> YukikazeDebuff()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Hakaze.Name && !Core.Player.CurrentTarget.HasAura(819, false, 8000))
            {
                return await Spellbook.Yukikaze.Cast();
            }
            return false;
        }

        public async Task<bool> Meikyo()
        {
            if (Core.Player.HasAura(1233))
            {
                if (!GetsuActive)
                {
                    return await Spellbook.Gekko.Cast();
                }
                if (!KaActive)
                {
                    return await Spellbook.Kasha.Cast();
                }
                if (!SetsuActive)
                {
                    return await Spellbook.Yukikaze.Cast();
                }
            }
            return false;
        }

        public async Task<bool> MidareSetsugekka()
        {
            if (!UseMidare) return false;

            if (!Core.Player.HasAura(1229) && ActionManager.CanCast(Spellbook.MidareSetsugekka.Name, Core.Player.CurrentTarget))
            {
                if (await Spellbook.HissatsuKaiten.Cast(null, false))
                {
                    await Coroutine.Wait(3000, () => Core.Player.HasAura(1229));
                }
            }
            return await Spellbook.MidareSetsugekka.Cast();
        }

        public async Task<bool> HissatsuShinten()
        {
            if (Resource.Kenki >= 45 && (!PoolKenki))
            {
                return await Spellbook.HissatsuShinten.Cast();
            }
            return false;
        }

        public async Task<bool> HissatsuSeigan()
        {
            if (Shadow.LastSpell.Name != Spellbook.HissatsuKaiten.Name && Resource.Kenki >= 35 && !PoolKenki)
            {
                return await Spellbook.HissatsuSeigan.Cast();
            }
            return false;
        }

        public async Task<bool> Enpi()
        {
            if (Core.Player.HasAura("Enhanced Enpi") && Core.Player.TargetDistance(10))
            {
                return await Spellbook.Enpi.Cast();
            }
            return false;
        }

        #endregion

        #region DoT

        public async Task<bool> Higanbana()
        {
            if (!UseHiganbana) return false;

            if (!Core.Player.HasAura(1229) && ActionManager.CanCast(Spellbook.Higanbana.Name, Core.Player.CurrentTarget))
            {
                if (await Spellbook.HissatsuKaiten.Cast(null, false))
                {
                    await Coroutine.Wait(3000, () => Core.Player.HasAura(1229));
                }
            }
            return await Spellbook.Higanbana.Cast();
        }

        #endregion

        #region AoE

        public async Task<bool> Fuga()
        {
            if (Core.Player.HasAura(Spellbook.Shifu.Name) && Core.Player.HasAura(Spellbook.Jinpu.Name))
            {
                return await Spellbook.Fuga.Cast();
            }
            return false;
        }

        public async Task<bool> Mangetsu()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Fuga.Name && !GetsuActive)
            {
                return await Spellbook.Mangetsu.Cast();
            }
            return false;
        }

        public async Task<bool> Oka()
        {
            if (ActionManager.LastSpell.Name == Spellbook.Fuga.Name && !KaActive)
            {
                return await Spellbook.Oka.Cast();
            }
            return false;
        }

        public async Task<bool> TenkaGoken()
        {
            if (!UseTenka) return false;

            if (!Core.Player.HasAura(1229) && ActionManager.CanCast(Spellbook.TenkaGoken.Name, Core.Player.CurrentTarget))
            {
                if (await Spellbook.HissatsuKaiten.Cast(null, false))
                {
                    await Coroutine.Wait(3000, () => Core.Player.HasAura(1229));
                }
            }
            return await Spellbook.TenkaGoken.Cast();
        }

        public async Task<bool> HissatsuKyuten()
        {
            if (Shadow.Settings.RotationMode != Modes.Single)
            {
                if (Shadow.LastSpell.Name != Spellbook.HissatsuKaiten.Name && Resource.Kenki >= 45 && !PoolKenki)
                {
                    return await Spellbook.HissatsuKyuten.Cast();
                }
            }
            return false;
        }

        #endregion

        #region Cooldown

        public async Task<bool> HissatsuGyoten()
        {
            if (Shadow.Settings.SamuraiGyoten && Core.Player.TargetDistance(10))
            {
                return await Spellbook.HissatsuGyoten.Cast(null, false);
            }
            return false;
        }

        public async Task<bool> HissatsuGuren()
        {
            if (Shadow.Settings.SamuraiGuren && Resource.Kenki >= 70)
            {
                return await Spellbook.HissatsuGuren.Cast();
            }
            return false;
        }

        #endregion

        #region Buff

        public async Task<bool> MeikyoShisui()
        {
            if (Shadow.Settings.SamuraiMeikyo && Core.Player.TargetDistance(5, false))
            {
                if (ActionManager.LastSpell.Name == Spellbook.Gekko.Name || ActionManager.LastSpell.Name == Spellbook.Kasha.Name ||
                    ActionManager.LastSpell.Name == Spellbook.Yukikaze.Name)
                {
                    return await Spellbook.MeikyoShisui.Cast();
                }
            }
            return false;
        }

        public static async Task<bool> Kaiten()
        {
            return Core.Player.HasAura(1229);
        }

        public async Task<bool> HissatsuKaiten()
        {
            if (!UseHiganbana && !UseTenka && !UseMidare)
                return false;

            if (await Spellbook.HissatsuKaiten.Cast())
            {
                return await Coroutine.Wait(3000, () => Core.Player.HasAura(1229));
            }
            return false;
        }

        public async Task<bool> Meditate()
        {
            return Core.Player.HasAura(Spellbook.Meditate.Name);
        }

        #endregion

        #region Heal

        public async Task<bool> MercifulEyes()
        {
            if (Shadow.Settings.SamuraiMerciful && Core.Player.CurrentHealthPercent < Shadow.Settings.SamuraiMercifulPct)
            {
                return await Spellbook.MercifulEyes.Cast();
            }
            return false;
        }

        #endregion

        #region Role

        public async Task<bool> SecondWind()
        {
            if (Shadow.Settings.SamuraiSecondWind && Core.Player.CurrentHealthPercent < Shadow.Settings.SamuraiSecondWindPct)
            {
                return await Spellbook.Role.SecondWind.Cast();
            }
            return false;
        }

        public async Task<bool> Bloodbath()
        {
            if (Shadow.Settings.SamuraiBloodbath && Core.Player.CurrentHealthPercent < Shadow.Settings.SamuraiBloodbathPct)
            {
                return await Spellbook.Role.Bloodbath.Cast();
            }
            return false;
        }

        public async Task<bool> TrueNorth()
        {
            if (Shadow.Settings.SamuraiTrueNorth && Core.Player.TargetDistance(5, false))
            {
                return await Spellbook.Role.TrueNorth.Cast();
            }
            return false;
        }

        #endregion

        #region Custom

        public static bool GetsuActive => Resource.Sen.HasFlag(Resource.Iaijutsu.Getsu);
        public static bool KaActive => Resource.Sen.HasFlag(Resource.Iaijutsu.Ka);
        public static bool SetsuActive => Resource.Sen.HasFlag(Resource.Iaijutsu.Setsu);
        public static bool PoolKenki => Shadow.Settings.SamuraiGuren && ActionManager.HasSpell(7496) &&
                                         DataManager.GetSpellData(7496).Cooldown.TotalMilliseconds < 6000;

        public static bool UseHiganbana => Shadow.Settings.SamuraiHiganbana && NumSen == 1 &&
                                            !Core.Player.CurrentTarget.HasAura(1228, true, 8000) &&
                                            (Core.Player.CurrentTarget.IsBoss() ||
                                             Core.Player.CurrentTarget.CurrentHealth > Shadow.Settings.SamuraiHiganbanaHP);

        public static bool UseTenka => NumSen == 2 && (Shadow.Settings.RotationMode == Modes.Multi ||
                                                        Shadow.Settings.RotationMode == Modes.Smart &&
                                                        Helpers.EnemiesNearTarget(5) >= Helpers.AoECount);

        public static bool UseMidare => Shadow.Settings.SamuraiMidare && NumSen == 3 &&
                                         (Core.Player.CurrentTarget.IsBoss() ||
                                          Core.Player.CurrentTarget.CurrentHealth > Shadow.Settings.SamuraiMidareHP);

        public static int NumSen
        {
            get { return Enum.GetValues(typeof(Resource.Iaijutsu)).Cast<Enum>().Count(value => Resource.Sen.HasFlag(value)); }
        }

        #endregion
    }
}
