using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Astrologian;
using static ShadowCR.Constants;
using ff14bot.Objects;
using Buddy.Coroutines;
using ff14bot.Helpers;
using System.Windows.Media;

namespace ShadowCR.Rotations
{
    public class AstrologianActions : IHealerActions
    {
        public AstrologianSpells Spellbook { get; } = new AstrologianSpells();

        #region Damage

        public async Task<bool> Malefic()
        {
            if (!ActionManager.HasSpell(Spellbook.MaleficII.Name) && !StopDamage)
            {
                return await Spellbook.Malefic.Cast();
            }
            return false;
        }

        public async Task<bool> MaleficII()
        {
            if (!ActionManager.HasSpell(Spellbook.MaleficIII.Name) && !StopDamage)
            {
                return await Spellbook.MaleficII.Cast();
            }
            return false;
        }

        public async Task<bool> MaleficIII()
        {
            if (!StopDamage)
            {
                return await Spellbook.MaleficIII.Cast();
            }
            return false;
        }

        #endregion

        #region DoT

        public async Task<bool> Combust()
        {
            if (!ActionManager.HasSpell(Spellbook.CombustII.Name) && !StopDots &&
                !Core.Player.CurrentTarget.HasAura(Spellbook.Combust.Name, true, 4000))
            {
                return await Spellbook.Combust.Cast();
            }
            return false;
        }

        public async Task<bool> CombustII()
        {
            if (!StopDots && !Core.Player.CurrentTarget.HasAura(Spellbook.CombustII.Name, true, 4000))
            {
                return await Spellbook.CombustII.Cast();
            }
            return false;
        }

        #endregion

        #region AoE

        public async Task<bool> Gravity()
        {
            if (!StopDamage)
            {
                return await Spellbook.Gravity.Cast();
            }
            return false;
        }

        public async Task<bool> EarthlyStar()
        {
            if (Shadow.Settings.AstrologianEarthlyStar)
            {
                var count = Shadow.Settings.CustomAoE ? Shadow.Settings.CustomAoECount : 3;

                if (Shadow.Settings.RotationMode == Modes.Multi || Helpers.EnemiesNearTarget(8) >= count)
                {
                    return await Spellbook.EarthlyStar.Cast();
                }
            }
            return false;
        }

        public async Task<bool> StellarDetonation()
        {
            if (Shadow.Settings.AstrologianStellarDetonation)
            {
                return await Spellbook.StellarDetonation.Cast(null, false);
            }
            return false;
        }

        #endregion

        #region Buff

        public async Task<bool> Lightspeed()
        {
            if (Shadow.Settings.AstrologianLightspeed && Shadow.Settings.AstrologianPartyHeal)
            {
                if (Helpers.HealManager.Count(hm => hm.CurrentHealthPercent < Shadow.Settings.AstrologianLightspeedPct) >=
                    Shadow.Settings.AstrologianLightspeedCount)
                {
                    return await Spellbook.Lightspeed.Cast(null, false);
                }
            }
            return false;
        }

        public async Task<bool> Synastry()
        {
            if (Shadow.Settings.AstrologianPartyHeal && Shadow.Settings.AstrologianSynastry)
            {
                if (Helpers.HealManager.Count(hm => hm.CurrentHealthPercent < Shadow.Settings.AstrologianSynastryPct) >=
                    Shadow.Settings.AstrologianSynastryCount)
                {
                    var target = Helpers.HealManager.FirstOrDefault(hm => hm.IsTank());

                    if (target != null)
                    {
                        return await Spellbook.Synastry.Cast(target, false);
                    }
                }
            }
            return false;
        }

        public async Task<bool> CelestialOpposition()
        {
            if (Shadow.Settings.AstrologianCelestialOpposition && Core.Player.HasAura(Spellbook.Role.LucidDreaming.Name))
            {
                return await Spellbook.CelestialOpposition.Cast(null, false);
            }
            return false;
        }

        #endregion

        #region Heal

        public async Task<bool> UpdateHealing()
        {
            if (Shadow.Settings.AstrologianPartyHeal || Shadow.Settings.AstrologianDraw)
            {
                if (!await Helpers.UpdateHealManager())
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> StopCasting()
        {
            if (Shadow.Settings.AstrologianInterruptOverheal && Core.Player.IsCasting)
            {
                var target = GameObjectManager.GetObjectByObjectId(Core.Player.SpellCastInfo.TargetId);
                var spellName = Core.Player.SpellCastInfo.Name;

                if (target != null)
                {
                    if (spellName == Spellbook.Benefic.Name && target.CurrentHealthPercent >= Shadow.Settings.AstrologianBeneficPct + 10 ||
                        spellName == Spellbook.BeneficII.Name && target.CurrentHealthPercent >= Shadow.Settings.AstrologianBeneficIIPct + 10)
                    {
                        var debugSetting = spellName == Spellbook.Benefic.Name ? Shadow.Settings.AstrologianBeneficPct
                            : Shadow.Settings.AstrologianBeneficIIPct;
                        Helpers.Debug($@"Target HP: {target.CurrentHealthPercent}, Setting: {debugSetting}, Adjusted: {debugSetting + 10}");

                        Logging.Write(Colors.Yellow, $@"[Shadow] Interrupting >>> {spellName}");
                        ActionManager.StopCasting();
                        await Coroutine.Wait(500, () => !Core.Player.IsCasting);
                    }
                }
            }
            return false;
        }

        public async Task<bool> Benefic()
        {
            if (Shadow.Settings.AstrologianBenefic)
            {
                var target = Shadow.Settings.AstrologianPartyHeal
                    ? Helpers.HealManager.FirstOrDefault(hm => hm.CurrentHealthPercent < Shadow.Settings.AstrologianBeneficPct)
                    : Core.Player.CurrentHealthPercent < Shadow.Settings.AstrologianBeneficPct ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.Benefic.Cast(target);
                }
            }
            return false;
        }

        public async Task<bool> BeneficII()
        {
            if (Shadow.Settings.AstrologianBeneficII)
            {
                var target = Shadow.Settings.AstrologianPartyHeal
                    ? Helpers.HealManager.FirstOrDefault(hm => hm.CurrentHealthPercent < Shadow.Settings.AstrologianBeneficIIPct)
                    : Core.Player.CurrentHealthPercent < Shadow.Settings.AstrologianBeneficIIPct ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.BeneficII.Cast(target);
                }
            }
            return false;
        }

        public async Task<bool> EssentialDignity()
        {
            if (Shadow.Settings.AstrologianEssDignity)
            {
                var target = Shadow.Settings.AstrologianPartyHeal
                    ? Helpers.HealManager.FirstOrDefault(hm => hm.CurrentHealthPercent < Shadow.Settings.AstrologianEssDignityPct)
                    : Core.Player.CurrentHealthPercent < Shadow.Settings.AstrologianEssDignityPct ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.EssentialDignity.Cast(target, false);
                }
            }
            return false;
        }

        public async Task<bool> AspectedBenefic()
        {
            if (Shadow.Settings.AstrologianAspBenefic && SectActive)
            {
                var target = Shadow.Settings.AstrologianPartyHeal
                    ? Helpers.HealManager.FirstOrDefault(hm => hm.CurrentHealthPercent < Shadow.Settings.AstrologianAspBeneficPct &&
                                                               !hm.HasAura(Spellbook.AspectedBenefic.Name, true))
                    : Core.Player.CurrentHealthPercent < Shadow.Settings.AstrologianAspBeneficPct &&
                      !Core.Player.HasAura(Spellbook.AspectedBenefic.Name, true) ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.AspectedBenefic.Cast(target);
                }
            }
            return false;
        }

        public async Task<bool> Helios()
        {
            if (Shadow.Settings.AstrologianHelios && Shadow.Settings.AstrologianPartyHeal && UseAoEHeals)
            {
                var count = Helpers.FriendsNearPlayer(Shadow.Settings.AstrologianHeliosPct);

                if (count > 2)
                {
                    return await Spellbook.Helios.Cast();
                }
            }
            return false;
        }

        public async Task<bool> AspectedHelios()
        {
            if (Shadow.Settings.AstrologianAspHelios && Shadow.Settings.AstrologianPartyHeal && SectActive && UseAoEHeals &&
                !Core.Player.HasAura(Spellbook.AspectedHelios.Name, true))
            {
                var count = Helpers.FriendsNearPlayer(Shadow.Settings.AstrologianAspHeliosPct);

                if (count > 2)
                {
                    return await Spellbook.AspectedHelios.Cast();
                }
            }
            return false;
        }

        public async Task<bool> Ascend()
        {
            if (Shadow.Settings.AstrologianAscend &&
                (Shadow.Settings.AstrologianSwiftcast && ActionManager.CanCast(Spellbook.Role.Swiftcast.Name, Core.Player) ||
                 !Helpers.HealManager.Any(hm => hm.CurrentHealthPercent < Shadow.Settings.AstrologianBeneficPct)))
            {
                var target = Helpers.RessManager.FirstOrDefault(pm => !pm.HasAura("Raise"));

                if (target != null)
                {
                    if (Shadow.Settings.AstrologianSwiftcast && ActionManager.CanCast(Spellbook.Ascend.Name, target))
                    {
                        if (await Spellbook.Role.Swiftcast.Cast(null, false))
                        {
                            await Coroutine.Wait(3000, () => Core.Player.HasAura(Spellbook.Role.Swiftcast.Name));
                        }
                    }
                    return await Spellbook.Ascend.Cast(target);
                }
            }
            return false;
        }

        #endregion

        #region Card

        public async Task<bool> DrawTargetted()
        {
            if (!HasCard || BuffShared && Helpers.HealManager.Any(IsBuffed))
            {
                return false;
            }

            var target = Core.Player as BattleCharacter;

            if (CardOffensive)
            {
                target = Helpers.HealManager.FirstOrDefault(hm => hm.IsDPS() && !IsBuffed(hm)) ?? Core.Player;
            }
            if (CardBole && !BuffShared)
            {
                target = Helpers.HealManager.FirstOrDefault(hm => hm.IsTank() && !hm.HasAura("The Bole"));
            }

            if (target != null)
            {
                return await Spellbook.Draw.Cast(target);
            }
            return false;
        }

        public async Task<bool> Draw()
        {
            if (!HasCard && (!BuffShared || !SpreadOffensive || Core.Player.InCombat))
            {
                return await Spellbook.Draw.Cast();
            }
            return false;
        }

        public async Task<bool> Redraw()
        {
            if (!CardOffensive && (BuffShared || !CardSupport))
            {
                return await Spellbook.Redraw.Cast();
            }
            return false;
        }

        public async Task<bool> MinorArcana()
        {
            if (!HasArcana && !CardOffensive && (BuffShared || CardEwer))
            {
                return await Spellbook.MinorArcana.Cast();
            }
            return false;
        }

        public async Task<bool> Undraw()
        {
            if (HasArcana || !ActionManager.HasSpell(Spellbook.MinorArcana.Name))
            {
                if (!CardOffensive && (BuffShared || CardEwer))
                {
                    return await Spellbook.Undraw.Cast();
                }
            }
            return false;
        }

        public async Task<bool> LadyOfCrowns()
        {
            if (Shadow.Settings.AstrologianDraw && CardLady)
            {
                var target = Shadow.Settings.AstrologianPartyHeal ? Helpers.HealManager.FirstOrDefault(hm => hm.CurrentHealthPercent < 80)
                    : Core.Player.CurrentHealthPercent < 80 ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.LadyOfCrowns.Cast(target);
                }
            }
            return false;
        }

        public async Task<bool> LordOfCrowns()
        {
            if (CardLord)
            {
                return await Spellbook.LordOfCrowns.Cast();
            }
            return false;
        }

        public async Task<bool> SleeveDraw()
        {
            if (Shadow.Settings.AstrologianSleeveDraw && !HasCard && (!HasSpread || !BuffShared))
            {
                return await Spellbook.SleeveDraw.Cast();
            }
            return false;
        }

        #endregion

        #region Sect

        public async Task<bool> DiurnalSect()
        {
            if (Shadow.Settings.AstrologianSect == AstrologianSects.Diurnal ||
                Shadow.Settings.AstrologianSect == AstrologianSects.Nocturnal && !ActionManager.HasSpell(Spellbook.NocturnalSect.Name))
            {
                if (!Core.Player.HasAura(Spellbook.DiurnalSect.Name))
                {
                    return await Spellbook.DiurnalSect.Cast();
                }
            }
            return false;
        }

        public async Task<bool> NocturnalSect()
        {
            if (Shadow.Settings.AstrologianSect == AstrologianSects.Nocturnal && !Core.Player.HasAura(Spellbook.NocturnalSect.Name))
            {
                return await Spellbook.NocturnalSect.Cast();
            }
            return false;
        }

        #endregion

        #region Role

        public async Task<bool> Esuna()
        {
            if (Shadow.Settings.AstrologianEsuna)
            {
                var target = Shadow.Settings.AstrologianPartyHeal ? Helpers.HealManager.FirstOrDefault(hm => hm.HasDispellable())
                    : Core.Player.HasDispellable() ? Core.Player : null;

                if (target != null)
                {
                    return await Spellbook.Role.Esuna.Cast(target);
                }
            }
            return false;
        }

        public async Task<bool> LucidDreaming()
        {
            if (Shadow.Settings.AstrologianLucidDreaming && Core.Player.CurrentManaPercent < Shadow.Settings.AstrologianLucidDreamingPct)
            {
                return await Spellbook.Role.LucidDreaming.Cast(null, false);
            }
            return false;
        }


        #endregion

        #region Custom

        public static bool IsBuffed(GameObject unit)
        {
            return unit.HasAura("The Balance") || unit.HasAura("The Arrow") || unit.HasAura("The Spear");
        }

        public static bool StopDamage => Shadow.Settings.AstrologianStopDamage && Core.Player.CurrentManaPercent <= Shadow.Settings.AstrologianStopDamagePct;
        public static bool StopDots => Shadow.Settings.AstrologianStopDots && Core.Player.CurrentManaPercent <= Shadow.Settings.AstrologianStopDotsPct;

        public static bool HasCard => Resource.Cards[0] != Resource.AstrologianCard.None;
        public static bool HasSpread => Resource.Cards[1] != Resource.AstrologianCard.None;
        public static bool HasArcana => Resource.Arcana != Resource.AstrologianCard.None;

        public static bool BuffShared => Resource.Buff == Resource.AstrologianCardBuff.Shared;

        public static bool CardLord => Resource.Arcana == Resource.AstrologianCard.LordofCrowns;
        public static bool CardLady => Resource.Arcana == Resource.AstrologianCard.LadyofCrowns;
        public static bool CardBole => Resource.Cards[0] == Resource.AstrologianCard.Bole;
        public static bool CardEwer => Resource.Cards[0] == Resource.AstrologianCard.Ewer;
        public static bool CardSpire => Resource.Cards[0] == Resource.AstrologianCard.Spire;
        public static bool CardSupport => Resource.Cards[0] == Resource.AstrologianCard.Ewer || Resource.Cards[0] == Resource.AstrologianCard.Spire;
        public static bool CardOffensive => Resource.Cards[0] == Resource.AstrologianCard.Balance || Resource.Cards[0] == Resource.AstrologianCard.Arrow ||
                                             Resource.Cards[0] == Resource.AstrologianCard.Spear;

        public static bool SpreadOffensive => Resource.Cards[1] == Resource.AstrologianCard.Balance || Resource.Cards[1] == Resource.AstrologianCard.Arrow ||
                                             Resource.Cards[1] == Resource.AstrologianCard.Spear;

        public bool UseAoEHeals => Shadow.LastSpell.Name != Spellbook.Helios.Name && Shadow.LastSpell.Name != Spellbook.AspectedHelios.Name;
        public bool SectActive => Core.Player.HasAura(Spellbook.DiurnalSect.Name) || Core.Player.HasAura(Spellbook.NocturnalSect.Name);

        #endregion
    }
}
