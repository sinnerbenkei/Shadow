using ShadowCR.Spells.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowCR.Spells
{
    public class ConjurerSpells
    {
        public HealerSpells Role { get; } = new HealerSpells();

        public Spell Stone { get; } = new Spell
        {
            Name = "Stone",
            ID = 119,
            Level = 1,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Cure { get; } = new Spell
        {
            Name = "Cure",
            ID = 120,
            Level = 2,
            GCDType = GCDType.On,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell Aero { get; } = new Spell
        {
            Name = "Aero",
            ID = 121,
            Level = 4,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell Medica { get; } = new Spell
        {
            Name = "Medica",
            ID = 124,
            Level = 10,
            GCDType = GCDType.On,
            SpellType = SpellType.Heal,
            CastType = CastType.Self
        };

        public Spell Raise { get; } = new Spell
        {
            Name = "Raise",
            ID = 125,
            Level = 12,
            GCDType = GCDType.On,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell FluidAura { get; } = new Spell
        {
            Name = "Fluid Aura",
            ID = 134,
            Level = 15,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell StoneII { get; } = new Spell
        {
            Name = "Stone II",
            ID = 127,
            Level = 18,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell CureII { get; } = new Spell
        {
            Name = "Cure II",
            ID = 135,
            Level = 30,
            GCDType = GCDType.On,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell AeroII { get; } = new Spell
        {
            Name = "Aero II",
            ID = 132,
            Level = 46,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell MedicaII { get; } = new Spell
        {
            Name = "Medica II",
            ID = 133,
            Level = 50,
            GCDType = GCDType.On,
            SpellType = SpellType.Heal,
            CastType = CastType.Self
        };
    }
}
