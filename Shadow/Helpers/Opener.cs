using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buddy.Coroutines;
using ff14bot.Enums;
using ff14bot.Managers;
using ShadowCR.Rotations;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager;
using static ShadowCR.Constants;
using ff14bot.Helpers;
using System.Windows.Media;

namespace ShadowCR
{
    public static partial class Helpers
    {
        public static int OpenerStep;
        public static bool OpenerFinished;

        private static List<Spell> current;
        private static bool usePotion;
        private static int potionStep;
        private static HashSet<uint> potionType;
        private static DateTime resetTime;

        private static BardSpells Bard { get; } = new BardSpells();
        private static BlackMageSpells BlackMage { get; } = new BlackMageSpells();
        private static DarkKnightSpells DarkKnight { get; } = new DarkKnightSpells();
        private static DragoonSpells Dragoon { get; } = new DragoonSpells();
        private static MachinistSpells Machinist { get; } = new MachinistSpells();
        private static MonkSpells Monk { get; } = new MonkSpells();
        private static NinjaSpells Ninja { get; } = new NinjaSpells();
        private static RedMageSpells RedMage { get; } = new RedMageSpells();
        private static SamuraiSpells Samurai { get; } = new SamuraiSpells();
        private static SummonerSpells Summoner { get; } = new SummonerSpells();
        private static WarriorSpells Warrior { get; } = new WarriorSpells();

        //public static async Task<bool> ExecuteOpener()
        //{
        //    if (OpenerFinished || Me.ClassLevel < 70) return false;

        //    if (Shadow.Settings.CooldownMode == CooldownModes.Disabled)
        //    {
        //        AbortOpener("Please enable cooldown mode to use an opener.");
        //        return false;
        //    }

        //    #region GetOpener

        //    switch (Me.CurrentJob)
        //    {
        //        case ClassJobType.Bard:
        //            current = BardOpener.List;
        //            potionStep = 0;
        //            potionType = PotionIds.Dex;
        //            break;

        //        case ClassJobType.BlackMage:
        //            current = BlackMageOpener.List;
        //            potionStep = 7;
        //            potionType = PotionIds.Int;
        //            break;

        //        case ClassJobType.DarkKnight:
        //            current = DarkKnightOpener.List;
        //            potionStep = 3;
        //            potionType = PotionIds.Str;
        //            break;

        //        case ClassJobType.Dragoon:
        //            current = DragoonOpener.List;
        //            potionStep = 7;
        //            potionType = PotionIds.Str;
        //            break;

        //        case ClassJobType.Machinist:
        //            current = MachinistOpener.List;
        //            potionStep = 0;
        //            potionType = PotionIds.Dex;
        //            break;

        //        case ClassJobType.Monk:
        //            current = MonkOpener.List;
        //            potionStep = 4;
        //            potionType = PotionIds.Str;
        //            break;

        //        case ClassJobType.Ninja:
        //            current = NinjaOpener.List;
        //            potionStep = 7;
        //            potionType = PotionIds.Dex;
        //            break;

        //        case ClassJobType.Paladin:
        //            current = PaladinOpener.List;
        //            potionStep = 8;
        //            potionType = PotionIds.Str;
        //            break;

        //        case ClassJobType.RedMage:
        //            current = RedMageOpener.List;
        //            potionStep = 3;
        //            potionType = PotionIds.Int;
        //            break;

        //        case ClassJobType.Samurai:
        //            current = SamuraiOpener.List;
        //            potionStep = 4;
        //            potionType = PotionIds.Str;
        //            break;

        //        case ClassJobType.Summoner:
        //            current = SummonerOpener.List;
        //            potionStep = 2;
        //            potionType = PotionIds.Int;
        //            break;

        //        case ClassJobType.Warrior:
        //            current = WarriorOpener.List;
        //            potionStep = 5;
        //            potionType = PotionIds.Str;
        //            break;

        //        default:
        //            current = null;
        //            break;
        //    }

        //    if (current == null) return false;

        //    #endregion

        //    if (OpenerStep >= current.Count)
        //    {
        //        AbortOpener("Shadow >>> Opener Finished");
        //        return false;
        //    }

        //    if (usePotion && OpenerStep == potionStep)
        //    {
        //        if (await UsePotion(potionType)) return true;
        //    }

        //    var spell = current.ElementAt(OpenerStep);
        //    resetTime = DateTime.Now.AddSeconds(10);

        //    #region Job-Specific

        //    switch (Me.CurrentJob)
        //    {
        //        case ClassJobType.Bard:
        //        case ClassJobType.BlackMage:
        //        case ClassJobType.DarkKnight:
        //        case ClassJobType.Dragoon:
        //        case ClassJobType.Machinist:
        //        case ClassJobType.Monk:
        //        case ClassJobType.Ninja:
        //        case ClassJobType.RedMage:
        //        case ClassJobType.Samurai:
        //        case ClassJobType.Summoner:
        //        case ClassJobType.Warrior:
        //            break;
        //    }

        //    #endregion

        //    if (await spell.Cast(null, false))
        //    {
        //        Debug($"Executed opener step {OpenerStep} >>> {spell.Name}");
        //        OpenerStep++;
        //        if (spell.Name == "Swiftcast")
        //        {
        //            await Coroutine.Wait(3000, () => Me.HasAura("Swiftcast"));
        //        }

        //        if (OpenerStep == 1)
        //        {
        //            DisplayToast("Shadow >>> Opener Started", 2500);
        //        }

        //        #region Job-Specific
        //        #endregion
        //    }
        //    else if (spell.Cooldown(true) > 3000 && spell.Cooldown() > 500 && !Me.IsCasting)
        //    {
        //        Debug($"Skipped opener step {OpenerStep} due to cooldown >>> {spell.Name}");
        //        OpenerStep++;
        //    }
        //    return true;
        //}

        public static void AbortOpener(string msg)
        {
            Debug(msg);
            OpenerFinished = true;
            DisplayToast("Opener finished!", 2500);
        }

        public static void ResetOpener()
        {
            if (Me.InCombat || DateTime.Now < resetTime) return;

            OpenerStep = 0;
            OpenerFinished = false;
        }
    }
}
