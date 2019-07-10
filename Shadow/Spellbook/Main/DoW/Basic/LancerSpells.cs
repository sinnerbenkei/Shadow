using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class LancerSpells
    {
        public MeleeSpells Role { get; } = new MeleeSpells();

        public Spell TrueThrust { get; } = new Spell
        {
            Name = "True Thrust",
            ID = 75,
            Level = 1,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell VorpalThrust { get; } = new Spell
        {
            Name = "Vorpal Thrust",
            ID = 78,
            Level = 4,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell PiercingTalon { get; } = new Spell
        {
            Name = "Piercing Talon",
            ID = 90,
            Level = 15,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell LifeSurge { get; } = new Spell
        {
            Name = "Life Surge",
            ID = 83,
            Level = 18,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Disembowel { get; } = new Spell
        {
            Name = "Disembowel",
            ID = 87,
            Level = 38,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell ChaosThrust { get; } = new Spell
        {
            Name = "Chaos Thrust",
            ID = 88,
            Level = 50,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };
    }
}
