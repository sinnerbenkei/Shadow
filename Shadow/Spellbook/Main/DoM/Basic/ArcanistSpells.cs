using ShadowCR.Spells.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowCR.Spells
{
    public class ArcanistSpells
    {
        public CasterSpells Role { get; } = new CasterSpells();

        public Spell Ruin { get; } = new Spell
        {
            Name = "Ruin",
            ID = 163,
            Level = 1,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Bio { get; } = new Spell
        {
            Name = "Bio",
            ID = 164,
            Level = 2,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell Summon { get; } = new Spell
        {
            Name = "Summon",
            ID = 165,
            Level = 4,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Physick { get; } = new Spell
        {
            Name = "Physick",
            ID = 190,
            Level = 4,
            GCDType = GCDType.On,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell Miasma { get; } = new Spell
        {
            Name = "Miasma",
            ID = 168,
            Level = 10,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell SummonII { get; } = new Spell
        {
            Name = "Summon II",
            ID = 170,
            Level = 15,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Resurrection { get; } = new Spell
        {
            Name = "Resurrection",
            ID = 173,
            Level = 18,
            GCDType = GCDType.On,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell BioII { get; } = new Spell
        {
            Name = "Bio II",
            ID = 178,
            Level = 26,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell Bane { get; } = new Spell
        {
            Name = "Bane",
            ID = 174,
            Level = 30,
            GCDType = GCDType.Off,
            SpellType = SpellType.AoE,
            CastType = CastType.Target
        };

        public Spell RuinII { get; } = new Spell
        {
            Name = "Ruin II",
            ID = 172,
            Level = 38,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };
    }
}
