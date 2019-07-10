using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class PugilistSpells
    {
        public MeleeSpells Role { get; } = new MeleeSpells();

        public Spell Bootshine { get; } = new Spell
        {
            Name = "Bootshine",
            ID = 53,
            Level = 1,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell TrueStrike { get; } = new Spell
        {
            Name = "True Strike",
            ID = 54,
            Level = 4,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell SnapPunch { get; } = new Spell
        {
            Name = "Snap Punch",
            ID = 56,
            Level = 6,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell FistsOfEarth { get; } = new Spell
        {
            Name = "Fists of Earth",
            ID = 60,
            Level = 15,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell TwinSnakes { get; } = new Spell
        {
            Name = "Twin Snakes",
            ID = 61,
            Level = 18,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell ArmOfTheDestroyer { get; } = new Spell
        {
            Name = "Arm of the Destroyer",
            ID = 62,
            Level = 26,
            GCDType = GCDType.On,
            SpellType = SpellType.AoE,
            CastType = CastType.Self
        };

        public Spell Demolish { get; } = new Spell
        {
            Name = "Demolish",
            ID = 66,
            Level = 30,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell FistsOfWind { get; } = new Spell
        {
            Name = "Fists of Wind",
            ID = 73,
            Level = 34,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Mantra { get; } = new Spell
        {
            Name = "Mantra",
            ID = 65,
            Level = 42,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell PerfectBalance { get; } = new Spell
        {
            Name = "Perfect Balance",
            ID = 69,
            Level = 50,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };
    }
}
