using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class WarriorSpells : MarauderSpells
    {
        public Spell Defiance { get; } = new Spell
        {
            Name = "Defiance",
            ID = 48,
            Level = 30,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell InnerBeast { get; } = new Spell
        {
            Name = "Inner Beast",
            ID = 49,
            Level = 35,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell SteelCyclone { get; } = new Spell
        {
            Name = "Steel Cyclone",
            ID = 51,
            Level = 45,
            GCDType = GCDType.On,
            SpellType = SpellType.AoE,
            CastType = CastType.Self
        };

        public Spell Infuriate { get; } = new Spell
        {
            Name = "Infuriate",
            ID = 52,
            Level = 50,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell FellCleave { get; } = new Spell
        {
            Name = "Fell Cleave",
            ID = 3549,
            Level = 54,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell RawIntuition { get; } = new Spell
        {
            Name = "Raw Intuition",
            ID = 3551,
            Level = 56,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Equilibrium { get; } = new Spell
        {
            Name = "Equilibrium",
            ID = 3552,
            Level = 58,
            GCDType = GCDType.Off,
            SpellType = SpellType.Heal,
            CastType = CastType.Self
        };

        public Spell Decimate { get; } = new Spell
        {
            Name = "Decimate",
            ID = 3550,
            Level = 60,
            GCDType = GCDType.On,
            SpellType = SpellType.AoE,
            CastType = CastType.Self
        };

        public Spell Onslaught { get; } = new Spell
        {
            Name = "Onslaught",
            ID = 7386,
            Level = 62,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell Upheaval { get; } = new Spell
        {
            Name = "Upheaval",
            ID = 7387,
            Level = 64,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell ShakeItOff { get; } = new Spell
        {
            Name = "Shake it Off",
            ID = 7388,
            Level = 68,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell InnerRelease { get; } = new Spell
        {
            Name = "Inner Release",
            ID = 7389,
            Level = 70,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };
    }
}
