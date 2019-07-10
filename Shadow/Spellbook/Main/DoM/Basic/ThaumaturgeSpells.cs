using ShadowCR.Spells.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowCR.Spells
{
    public class ThaumaturgeSpells
    {
        public CasterSpells Role { get; } = new CasterSpells();

        public Spell Blizzard { get; } = new Spell
        {
            Name = "Blizzard",
            ID = 142,
            Level = 1,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Fire { get; } = new Spell
        {
            Name = "Fire",
            ID = 141,
            Level = 2,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Transpose { get; } = new Spell
        {
            Name = "Transpose",
            ID = 149,
            Level = 4,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Thunder { get; } = new Spell
        {
            Name = "Thunder",
            ID = 144,
            Level = 6,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell Sleep { get; } = new Spell
        {
            Name = "Sleep",
            ID = 145,
            Level = 10,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell BlizzardII { get; } = new Spell
        {
            Name = "Blizzard II",
            ID = 146,
            Level = 12,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Self
        };

        public Spell Scathe { get; } = new Spell
        {
            Name = "Scathe",
            ID = 156,
            Level = 15,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell FireII { get; } = new Spell
        {
            Name = "Fire II",
            ID = 147,
            Level = 18,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell ThunderII { get; } = new Spell
        {
            Name = "Thunder II",
            ID = 7447,
            Level = 26,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell Manaward { get; } = new Spell
        {
            Name = "Manaward",
            ID = 157,
            Level = 30,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell FireIII { get; } = new Spell
        {
            Name = "Fire III",
            ID = 152,
            Level = 34,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell AetherialManipulation { get; } = new Spell
        {
            Name = "Aetherial Manipulation",
            ID = 155,
            Level = 50,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };
    }
}
