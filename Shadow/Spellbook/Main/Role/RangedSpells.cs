using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowCR.Spells.Role
{
    public class RangedSpells
    {
        public Spell SecondWind { get; } = new Spell
        {
            Name = "Second Wind",
            ID = 7541,
            Level = 8,
            GCDType = GCDType.Off,
            SpellType = SpellType.Heal,
            CastType = CastType.Self
        };

        public Spell FootGraze { get; } = new Spell
        {
            Name = "Foot Graze",
            ID = 7553,
            Level = 10,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell LegGraze { get; } = new Spell
        {
            Name = "Leg Graze",
            ID = 7554,
            Level = 6,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell Peloton { get; } = new Spell
        {
            Name = "Peloton",
            ID = 7557,
            Level = 20,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell HeadGraze { get; } = new Spell
        {
            Name = "Head Graze",
            ID = 7551,
            Level = 24,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell ArmsLength { get; } = new Spell
        {
            Name = "Arm's Length",
            ID = 7548,
            Level = 32,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };
    }
}
