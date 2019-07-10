using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class GladiatorSpells
    {
        public TankSpells Role { get; } = new TankSpells();

        public Spell FastBlade { get; } = new Spell
        {
            Name = "Fast Blade",
            ID = 9,
            Level = 1,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell FightOrFlight { get; } = new Spell
        {
            Name = "Fight or Flight",
            ID = 20,
            Level = 2,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell RiotBlade { get; } = new Spell
        {
            Name = "Riot Blade",
            ID = 15,
            Level = 10,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell ShieldLob { get; } = new Spell
        {
            Name = "Shield Lob",
            ID = 24,
            Level = 15,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell ShieldBash { get; } = new Spell
        {
            Name = "Shield Bash",
            ID = 16,
            Level = 18,
            GCDType = GCDType.On,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell RageOfHalone { get; } = new Spell
        {
            Name = "Rage of Halone",
            ID = 21,
            Level = 26,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Sentinel { get; } = new Spell
        {
            Name = "Sentinel",
            ID = 17,
            Level = 38,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell CircleOfScorn { get; } = new Spell
        {
            Name = "Circle of Scorn",
            ID = 23,
            Level = 50,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Self
        };
    }
}
