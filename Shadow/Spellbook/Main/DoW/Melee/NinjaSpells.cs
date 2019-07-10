using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class NinjaSpells : RogueSpells
    {
        public Spell Ten { get; } = new Spell
        {
            Name = "Ten",
            ID = 2259,
            Level = 30,
            GCDType = GCDType.On,
            SpellType = SpellType.Mudra,
            CastType = CastType.Self
        };

        public Spell Ninjutsu { get; } = new Spell
        {
            Name = "Ninjutsu",
            ID = 2260,
            Level = 30,
            GCDType = GCDType.On,
            SpellType = SpellType.Ninjutsu,
            CastType = CastType.Target
        };

        public Spell FumaShuriken { get; } = new Spell
        {
            Name = "Fuma Shuriken",
            ID = 2265,
            Level = 30,
            GCDType = GCDType.On,
            SpellType = SpellType.Ninjutsu,
            CastType = CastType.Target
        };

        public Spell Chi { get; } = new Spell
        {
            Name = "Chi",
            ID = 2261,
            Level = 35,
            GCDType = GCDType.On,
            SpellType = SpellType.Mudra,
            CastType = CastType.Self
        };

        public Spell Katon { get; } = new Spell
        {
            Name = "Katon",
            ID = 2266,
            Level = 35,
            GCDType = GCDType.On,
            SpellType = SpellType.Ninjutsu,
            CastType = CastType.Target
        };

        public Spell Raiton { get; } = new Spell
        {
            Name = "Raiton",
            ID = 2267,
            Level = 35,
            GCDType = GCDType.On,
            SpellType = SpellType.Ninjutsu,
            CastType = CastType.Target
        };

        public Spell Shukuchi { get; } = new Spell
        {
            Name = "Shukuchi",
            ID = 2262,
            Level = 40,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.TargetLocation
        };

        public Spell Jin { get; } = new Spell
        {
            Name = "Jin",
            ID = 2263,
            Level = 45,
            GCDType = GCDType.On,
            SpellType = SpellType.Mudra,
            CastType = CastType.Self
        };

        public Spell Hyoton { get; } = new Spell
        {
            Name = "Hyoton",
            ID = 2268,
            Level = 45,
            GCDType = GCDType.On,
            SpellType = SpellType.Ninjutsu,
            CastType = CastType.Target
        };

        public Spell Huton { get; } = new Spell
        {
            Name = "Huton",
            ID = 2269,
            Level = 45,
            GCDType = GCDType.On,
            SpellType = SpellType.Ninjutsu,
            CastType = CastType.Self
        };

        public Spell Doton { get; } = new Spell
        {
            Name = "Doton",
            ID = 2270,
            Level = 45,
            GCDType = GCDType.On,
            SpellType = SpellType.Ninjutsu,
            CastType = CastType.SelfLocation
        };

        public Spell Suiton { get; } = new Spell
        {
            Name = "Suiton",
            ID = 2271,
            Level = 45,
            GCDType = GCDType.On,
            SpellType = SpellType.Ninjutsu,
            CastType = CastType.Target
        };

        public Spell Kassatsu { get; } = new Spell
        {
            Name = "Kassatsu",
            ID = 2264,
            Level = 50,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell ArmorCrush { get; } = new Spell
        {
            Name = "Armor Crush",
            ID = 3563,
            Level = 54,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell DreamWithinADream { get; } = new Spell
        {
            Name = "Dream Within a Dream",
            ID = 3566,
            Level = 60,
            GCDType = GCDType.Off,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell HellfrogMedium { get; } = new Spell
        {
            Name = "Hellfrog Medium",
            ID = 7401,
            Level = 62,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell Bhavacakra { get; } = new Spell
        {
            Name = "Bhavacakra",
            ID = 7402,
            Level = 68,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell TenChiJin { get; } = new Spell
        {
            Name = "Ten Chi Jin",
            ID = 7403,
            Level = 70,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };
    }
}
