using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class WhiteMageSpells : ConjurerSpells
    {
        public Spell PresenceOfMind { get; } = new Spell
        {
            Name = "Presence of Mind",
            ID = 136,
            Level = 30,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Regen { get; } = new Spell
        {
            Name = "Regen",
            ID = 137,
            Level = 35,
            GCDType = GCDType.On,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell CureIII { get; } = new Spell
        {
            Name = "Cure III",
            ID = 131,
            Level = 40,
            GCDType = GCDType.On,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell Holy { get; } = new Spell
        {
            Name = "Holy",
            ID = 139,
            Level = 45,
            GCDType = GCDType.On,
            SpellType = SpellType.AoE,
            CastType = CastType.Self
        };

        public Spell Benediction { get; } = new Spell
        {
            Name = "Benediction",
            ID = 140,
            Level = 50,
            GCDType = GCDType.Off,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell Asylum { get; } = new Spell
        {
            Name = "Asylum",
            ID = 3569,
            Level = 52,
            GCDType = GCDType.Off,
            SpellType = SpellType.Heal,
            CastType = CastType.TargetLocation
        };

        public Spell StoneIII { get; } = new Spell
        {
            Name = "Stone III",
            ID = 3568,
            Level = 54,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Assize { get; } = new Spell
        {
            Name = "Assize",
            ID = 3571,
            Level = 56,
            GCDType = GCDType.Off,
            SpellType = SpellType.Heal,
            CastType = CastType.Self
        };

        public Spell Tetragrammaton { get; } = new Spell
        {
            Name = "Tetragrammaton",
            ID = 3570,
            Level = 60,
            GCDType = GCDType.Off,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell ThinAir { get; } = new Spell
        {
            Name = "Thin Air",
            ID = 7430,
            Level = 62,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell StoneIV { get; } = new Spell
        {
            Name = "Stone IV",
            ID = 7431,
            Level = 64,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell DivineBenison { get; } = new Spell
        {
            Name = "Divine Benison",
            ID = 7432,
            Level = 66,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Target
        };

        public Spell PlenaryIndulgence { get; } = new Spell
        {
            Name = "Plenary Indulgence",
            ID = 7433,
            Level = 70,
            GCDType = GCDType.Off,
            SpellType = SpellType.Heal,
            CastType = CastType.Self
        };
    }
}
