using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class PaladinSpells : GladiatorSpells
    {
        public Spell IronWill { get; } = new Spell
        {
            Name = "Iron Will",
            ID = 28,
            Level = 30,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Cover { get; } = new Spell
        {
            Name = "Cover",
            ID = 27,
            Level = 40,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Target
        };

        public Spell SpiritsWithin { get; } = new Spell
        {
            Name = "Spirits Within",
            ID = 29,
            Level = 45,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell TotalEclipse { get; } = new Spell
        {
            Name = "Total Eclipse",
            ID = 7381,
            Level = 46,
            GCDType = GCDType.On,
            SpellType = SpellType.AoE,
            CastType = CastType.Self
        };

        public Spell HallowedGround { get; } = new Spell
        {
            Name = "Hallowed Ground",
            ID = 30,
            Level = 50,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Sheltron { get; } = new Spell
        {
            Name = "Sheltron",
            ID = 3542,
            Level = 52,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell GoringBlade { get; } = new Spell
        {
            Name = "Goring Blade",
            ID = 3538,
            Level = 54,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell DivineVeil { get; } = new Spell
        {
            Name = "Divine Veil",
            ID = 3540,
            Level = 56,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Clemency { get; } = new Spell
        {
            Name = "Clemency",
            ID = 3541,
            Level = 58,
            GCDType = GCDType.On,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell RoyalAuthority { get; } = new Spell
        {
            Name = "Royal Authority",
            ID = 3539,
            Level = 60,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Intervention { get; } = new Spell
        {
            Name = "Intervention",
            ID = 7382,
            Level = 62,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Target
        };

        public Spell HolySpirit { get; } = new Spell
        {
            Name = "Holy Spirit",
            ID = 7384,
            Level = 64,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Requiescat { get; } = new Spell
        {
            Name = "Requiescat",
            ID = 7383,
            Level = 68,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell PassageOfArms { get; } = new Spell
        {
            Name = "Passage of Arms",
            ID = 7385,
            Level = 70,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };
    }
}
