using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class MonkSpells : PugilistSpells
    {
        public Spell Rockbreaker { get; } = new Spell
        {
            Name = "Rockbreaker",
            ID = 70,
            Level = 30,
            GCDType = GCDType.On,
            SpellType = SpellType.AoE,
            CastType = CastType.Target
        };

        public Spell ShoulderTackle { get; } = new Spell
        {
            Name = "Shoulder Tackle",
            ID = 71,
            Level = 35,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell FistsOfFire { get; } = new Spell
        {
            Name = "Fists of Fire",
            ID = 63,
            Level = 40,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell DragonKick { get; } = new Spell
        {
            Name = "Dragon Kick",
            ID = 74,
            Level = 50,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell FormShift { get; } = new Spell
        {
            Name = "Form Shift",
            ID = 4262,
            Level = 52,
            GCDType = GCDType.On,
            SpellType = SpellType.Heal,
            CastType = CastType.Self
        };

        public Spell Meditation { get; } = new Spell
        {
            Name = "Meditation",
            ID = 3546,
            Level = 54,
            GCDType = GCDType.On,
            SpellType = SpellType.Heal,
            CastType = CastType.Self
        };

        public Spell ForbiddenChakra { get; } = new Spell
        {
            Name = "The Forbidden Chakra",
            ID = 3547,
            Level = 54,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell ElixirField { get; } = new Spell
        {
            Name = "Elixir Field",
            ID = 3545,
            Level = 56,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Self
        };

        public Spell Purification { get; } = new Spell
        {
            Name = "Purification",
            ID = 3544,
            Level = 58,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell TornadoKick { get; } = new Spell
        {
            Name = "Tornado Kick",
            ID = 3543,
            Level = 60,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell RiddleOfEarth { get; } = new Spell
        {
            Name = "Riddle of Earth",
            ID = 7394,
            Level = 64,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell RiddleOfFire { get; } = new Spell
        {
            Name = "Riddle of Fire",
            ID = 7395,
            Level = 68,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Brotherhood { get; } = new Spell
        {
            Name = "Brotherhood",
            ID = 7396,
            Level = 70,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };
    }
}
