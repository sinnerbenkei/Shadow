using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class ArcherSpells
    {
        public RangedSpells Role { get; } = new RangedSpells();

        public Spell HeavyShot { get; } = new Spell
        {
            Name = "Heavy Shot",
            ID = 97,
            Level = 1,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell StraightShot { get; } = new Spell
        {
            Name = "Straight Shot",
            ID = 98,
            Level = 2,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell RagingStrikes { get; } = new Spell
        {
            Name = "Raging Strikes",
            ID = 101,
            Level = 4,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell VenomousBite { get; } = new Spell
        {
            Name = "Venomous Bite",
            ID = 100,
            Level = 6,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell Bloodletter { get; } = new Spell
        {
            Name = "Bloodletter",
            ID = 110,
            Level = 12,
            GCDType = GCDType.Off,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell RepellingShot { get; } = new Spell
        {
            Name = "Repelling Shot",
            ID = 112,
            Level = 15,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell QuickNock { get; } = new Spell
        {
            Name = "Quick Nock",
            ID = 106,
            Level = 18,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Windbite { get; } = new Spell
        {
            Name = "Windbite",
            ID = 113,
            Level = 30,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell Barrage { get; } = new Spell
        {
            Name = "Barrage",
            ID = 107,
            Level = 38,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };
    }
}
