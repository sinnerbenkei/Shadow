using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using Buddy.Coroutines;
using ff14bot;
using ff14bot.AClasses;
using ff14bot.Enums;
using ff14bot.Helpers;
using ff14bot.Managers;
using ff14bot.Navigation;
using ShadowCR.Rotations;
using ShadowCR.Settings;
using ShadowCR.Settings.Forms;
using ShadowCR.Spells;
using TreeSharp;

namespace ShadowCR
{
    public class Shadow : CombatRoutine
    {
        #region Overrides

        public sealed override string Name => "Shadow";
        public sealed override float PullRange => 25;
        public sealed override bool WantButton => true;
        public sealed override CapabilityFlags SupportedCapabilities => CapabilityFlags.All;

        private static DateTime runTime;

        public sealed override void Initialize()
        {
            Logging.Write(Colors.GreenYellow, $@"[Shadow] Loaded Version: {Helpers.GetLocalVersion()}");
            RegisterHotkeys();
        }

        public sealed override void Pulse()
        {
            if (DateTime.Now < runTime) return;
            runTime = DateTime.Now.AddSeconds(1);
            var _class = CurrentClass;
            Helpers.ResetOpener();
        }

        public sealed override void ShutDown()
        {
            Logging.Write(Colors.GreenYellow, @"[Shadow] Shutting down...");
            UnregisterHotkeys();
        }

        #endregion

        #region Settings

        private Form _configForm;

        public static ShadowSettings Settings = ShadowSettings.Instance;

        public sealed override void OnButtonPress()
        {
            if (_configForm == null || _configForm.IsDisposed || _configForm.Disposing)
            {
                _configForm = new ShadowForm();
            }
            _configForm.ShowDialog();
        }

        #endregion

        #region Hotkeys

        public static void RegisterHotkeys()
        {
            HotkeyManager.Register("Shadow Rotation", Helpers.GetHotkey(Settings.RotationHotkey),
                                   Helpers.GetModkey(Settings.RotationHotkey), hk =>
                                   {
                                       Settings.RotationMode = Settings.RotationMode.Cycle("Rotation", Settings.IgnoreSmart);
                                   });
            HotkeyManager.Register("Shadow Cooldown", Helpers.GetHotkey(Settings.CooldownHotkey),
                                   Helpers.GetModkey(Settings.CooldownHotkey), hk =>
                                   {
                                       Settings.CooldownMode = Settings.CooldownMode.Cycle("Cooldown");
                                   });
            HotkeyManager.Register("Shadow Tank", Helpers.GetHotkey(Settings.TankHotkey),
                                   Helpers.GetModkey(Settings.TankHotkey), hk =>
                                   {
                                       Settings.TankMode = Settings.TankMode.Cycle("Tank");
                                   });
        }

        public static void RegisterClassHotkeys()
        {
            HotkeyManager.Unregister("Shadow Job");
            //switch (Core.Player.CurrentJob)
            //{
            //    case ClassJobType.Machinist:
            //        HotkeyManager.Register("Shadow Job", Helpers.GetHotkey(Settings.MachinistTurretHotkey),
            //                               Helpers.GetModkey(Settings.MachinistTurretHotkey),
            //                               hk => Settings.MachinistTurret = Settings.MachinistTurret.Cycle("Turret", true));
            //        break;
            //}
        }

        public static void UnregisterHotkeys()
        {
            HotkeyManager.Unregister("Shadow Rotation");
            HotkeyManager.Unregister("Shadow Cooldown");
            HotkeyManager.Unregister("Shadow Tank");
            HotkeyManager.Unregister("Shadow Job");
        }

        #endregion

        #region CurrentClass

        private IRotation _myRotation;
        private IRotation MyRotation => _myRotation ?? (_myRotation = GetRotation(CurrentClass));

        private ClassJobType _currentClass;
        private ClassJobType CurrentClass
        {
            get
            {
                if (_currentClass == Core.Player.CurrentJob)
                {
                    return _currentClass;
                }
                _currentClass = Core.Player.CurrentJob;
                _myRotation = GetRotation(_currentClass);
                Logging.Write(Colors.Yellow, $@"[Shadow] Loading {_currentClass}...");
                RegisterClassHotkeys();
                return _currentClass;
            }
        }

        public sealed override ClassJobType[] Class => new[] { Core.Player.CurrentJob };

        private static IRotation GetRotation(ClassJobType classJob)
        {
            switch (classJob)
            {
                case ClassJobType.Arcanist:
                    return new ArcanistRotation();
                case ClassJobType.Archer:
                    return new ArcherRotation();
                case ClassJobType.Astrologian:
                    return new AstrologianRotation();
                case ClassJobType.Bard:
                    return new BardRotation();
                case ClassJobType.BlackMage:
                    return new BlackMageRotation();
                case ClassJobType.BlueMage:
                    return new BlueMageRotation();
                case ClassJobType.Conjurer:
                    return new ConjurerRotation();
                case ClassJobType.Dancer:
                    return new DancerRotation();
                case ClassJobType.DarkKnight:
                    return new DarkKnightRotation();
                case ClassJobType.Dragoon:
                    return new DragoonRotation();
                case ClassJobType.Gladiator:
                    return new GladiatorRotation();
                case ClassJobType.Gunbreaker:
                    return new GunbreakerRotation();
                case ClassJobType.Lancer:
                    return new LancerRotation();
                case ClassJobType.Machinist:
                    return new MachinistRotation();
                case ClassJobType.Marauder:
                    return new MarauderRotation();
                case ClassJobType.Monk:
                    return new MonkRotation();
                case ClassJobType.Ninja:
                    return new NinjaRotation();
                case ClassJobType.Paladin:
                    return new PaladinRoatation();
                case ClassJobType.Pugilist:
                    return new PugilistRotation();
                case ClassJobType.RedMage:
                    return new RedMageRotation();
                case ClassJobType.Rogue:
                    return new RogueRotation();
                case ClassJobType.Samurai:
                    return new SamuraiRotation();
                case ClassJobType.Scholar:
                    return new ScholarRotation();
                case ClassJobType.Summoner:
                    return new SummonerRotation();
                case ClassJobType.Thaumaturge:
                    return new ThaumaturgeRotation();
                case ClassJobType.Warrior:
                    return new WarriorRotation();
                case ClassJobType.WhiteMage:
                    return new WhiteMageRotation();


                default:
                    Logging.Write(Colors.OrangeRed, $@"[Shadow] {classJob} is not supported.");
                    return new UnsupportedRotation();
            }
        }

        #endregion

        #region Behaviors

        public override Composite CombatBehavior
        {
            get
            {
                return new Decorator(r => Core.Player.HasTarget,
                                     new PrioritySelector(new Decorator(r => WorldManager.InPvP, new ActionRunCoroutine(ctx => MyRotation.CombatPVP())),
                                                          new Decorator(r => !WorldManager.InPvP, new ActionRunCoroutine(ctx => MyRotation.Combat()))));
            }
        }

        public override Composite CombatBuffBehavior
        {
            get { return new Decorator(r => Core.Player.HasTarget && !WorldManager.InPvP, new ActionRunCoroutine(ctx => MyRotation.CombatBuff())); }
        }

        public override Composite PullBehavior
        {
            get
            {
                return new Decorator(r => Core.Player.HasTarget,
                                     new PrioritySelector(new Decorator(r => WorldManager.InPvP, new ActionRunCoroutine(ctx => MyRotation.CombatPVP())),
                                                          new Decorator(r => !WorldManager.InPvP, new ActionRunCoroutine(ctx => MyRotation.Pull()))));
            }
        }

        public override Composite HealBehavior { get { return new ActionRunCoroutine(ctx => MyRotation.Heal()); } }
        public override Composite PreCombatBuffBehavior { get { return new ActionRunCoroutine(ctx => MyRotation.PreCombatBuff()); } }
        public override Composite RestBehavior { get { return new ActionRunCoroutine(ctx => Rest()); } }

        #endregion

        #region Rest

        public async Task<bool> Rest()
        {
            if (!BotManager.Current.IsAutonomous || WorldManager.InSanctuary || Core.Player.HasAura("Sprint") ||
                (!Settings.RestHealth || Core.Player.CurrentHealthPercent > Settings.RestHealthPct) &&
                (!Settings.RestEnergy || Core.Player.CurrentManaPercent > Settings.RestEnergyPct))
            {
                return false;
            }
            if (MovementManager.IsMoving)
            {
                Navigator.PlayerMover.MoveStop();
            }
            Logging.Write(Colors.Yellow, @"[Shadow] Resting...");
            return true;
        }

        #endregion

        #region Chocobo

        #region Summon

        public static async Task<bool> SummonChocobo()
        {
            if (!Settings.SummonChocobo || !BotManager.Current.IsAutonomous || !ChocoboManager.CanSummon || MovementManager.IsMoving ||
                ChocoboManager.Summoned)
            {
                return false;
            }
            ChocoboManager.Summon();
            await Coroutine.Wait(2000, () => ChocoboManager.Summoned);
            Logging.Write(Colors.Yellow, @"[Shadow] Summoning Chocobo...");

            if (!ChocoboManager.Summoned)
            {
                return await SummonChocobo();
            }
            return true;
        }

        #endregion

        #region Stance

        public static async Task<bool> ChocoboStance()
        {
            if (!Settings.SummonChocobo || !ChocoboManager.Summoned || Core.Player.IsMounted || ChocoboManager.Object == null)
            {
                return false;
            }

            if (Settings.ChocoboStanceDance)
            {
                if (ChocoboManager.Stance == CompanionStance.Healer &&
                    Core.Player.CurrentHealthPercent < Math.Min(100, Settings.ChocoboStanceDancePct + 10))
                {
                    return false;
                }

                if (Core.Player.CurrentHealthPercent < Settings.ChocoboStanceDancePct)
                {
                    ChocoboManager.HealerStance();
                    await Coroutine.Wait(1000, () => ChocoboManager.Stance == CompanionStance.Healer);
                    Logging.Write(Colors.Yellow, @"[Shadow] Chocobo Stance >>> Healer");
                    return true;
                }
            }

            switch (Settings.ChocoboStance)
            {
                case Stances.Free:
                    if (ChocoboManager.Stance == CompanionStance.Free)
                    {
                        break;
                    }
                    ChocoboManager.FreeStance();
                    await Coroutine.Wait(1000, () => ChocoboManager.Stance == CompanionStance.Free);
                    Logging.Write(Colors.Yellow, @"[Shadow] Chocobo Stance >>> Free");
                    return true;
                case Stances.Attacker:
                    if (ChocoboManager.Stance == CompanionStance.Attacker)
                    {
                        break;
                    }
                    ChocoboManager.AttackerStance();
                    await Coroutine.Wait(1000, () => ChocoboManager.Stance == CompanionStance.Attacker);
                    Logging.Write(Colors.Yellow, @"[Shadow] Chocobo Stance >>> Attacker");
                    return true;
                case Stances.Healer:
                    if (ChocoboManager.Stance == CompanionStance.Healer)
                    {
                        break;
                    }
                    ChocoboManager.HealerStance();
                    await Coroutine.Wait(1000, () => ChocoboManager.Stance == CompanionStance.Healer);
                    Logging.Write(Colors.Yellow, @"[Shadow] Chocobo Stance >>> Healer");
                    return true;
                case Stances.Defender:
                    if (ChocoboManager.Stance == CompanionStance.Defender)
                    {
                        break;
                    }
                    ChocoboManager.DefenderStance();
                    await Coroutine.Wait(1000, () => ChocoboManager.Stance == CompanionStance.Defender);
                    Logging.Write(Colors.Yellow, @"[Shadow] Chocobo Stance >>> Defender");
                    return true;
            }
            return false;
        }

        #endregion

        #endregion

        #region LastSpell

        private static Spell _lastSpell;
        public static Spell LastSpell { get { return _lastSpell ?? (_lastSpell = new Spell()); } set { _lastSpell = value; } }

        #endregion
    }
}
