using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowCR.Spells.Role
{
    public class MeleeSpells
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

        public Spell ArmsLength { get; } = new Spell
        {
            Name = "Arm's Length",
            ID = 7548,
            Level = 32,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell LegSweep { get; } = new Spell
        {
            Name = "Leg Sweep",
            ID = 7863,
            Level = 10,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell Bloodbath { get; } = new Spell
        {
            Name = "Bloodbath",
            ID = 7542,
            Level = 12,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Feint { get; } = new Spell
        {
            Name = "Feint",
            ID = 7549,
            Level = 22,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell TrueNorth { get; } = new Spell
        {
            Name = "True North",
            ID = 7546,
            Level = 50,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };
    }
}
