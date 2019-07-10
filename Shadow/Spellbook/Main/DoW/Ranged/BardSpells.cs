using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class BardSpells : ArcherSpells
    {
        public Spell MagesBallad { get; } = new Spell
        {
            Name = "Mage's Ballad",
            ID = 114,
            Level = 30,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell ArmysPaeon { get; } = new Spell
        {
            Name = "Army's Paeon",
            ID = 116,
            Level = 40,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell RainOfDeath { get; } = new Spell
        {
            Name = "Rain of Death",
            ID = 117,
            Level = 45,
            GCDType = GCDType.Off,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell BattleVoice { get; } = new Spell
        {
            Name = "Battle Voice",
            ID = 118,
            Level = 50,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell WanderersMinuet { get; } = new Spell
        {
            Name = "The Wanderer's Minuet",
            ID = 3559,
            Level = 52,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell PitchPerfect { get; } = new Spell
        {
            Name = "Pitch Perfect",
            ID = 7404,
            Level = 52,
            GCDType = GCDType.Off,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell EmpyrealArrow { get; } = new Spell
        {
            Name = "Empyreal Arrow",
            ID = 3558,
            Level = 54,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell IronJaws { get; } = new Spell
        {
            Name = "Iron Jaws",
            ID = 3560,
            Level = 56,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell TheWardensPaean { get; } = new Spell
        {
            Name = "The Warden's Paean",
            ID = 3561,
            Level = 58,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Target
        };

        public Spell Sidewinder { get; } = new Spell
        {
            Name = "Sidewinder",
            ID = 3562,
            Level = 60,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        // Troubadour
        // Caustic Bite
        // Stormbite
        // Nature's Minne

        public Spell RefulgentArrow { get; } = new Spell
        {
            Name = "Refulgent Arrow",
            ID = 7409,
            Level = 70,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };
    }
}
