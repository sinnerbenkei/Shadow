using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class MarauderSpells
    {
        public TankSpells Role { get; } = new TankSpells();

        public Spell HeavySwing { get; } = new Spell
        {
            Name = "Heavy Swing",
            ID = 31,
            Level = 1,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Berserk { get; } = new Spell
        {
            Name = "Berserk",
            ID = 38,
            Level = 6,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Overpower { get; } = new Spell
        {
            Name = "Overpower",
            ID = 41,
            Level = 10,
            GCDType = GCDType.On,
            SpellType = SpellType.AoE,
            CastType = CastType.Target
        };

        public Spell Tomahawk { get; } = new Spell
        {
            Name = "Tomahawk",
            ID = 46,
            Level = 15,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Maim { get; } = new Spell
        {
            Name = "Maim",
            ID = 37,
            Level = 18,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell ThrillOfBattle { get; } = new Spell
        {
            Name = "Thrill of Battle",
            ID = 40,
            Level = 26,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell StormsPath { get; } = new Spell
        {
            Name = "Storm's Path",
            ID = 42,
            Level = 38,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Vengeance { get; } = new Spell
        {
            Name = "Vengeance",
            ID = 44,
            Level = 46,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell StormsEye { get; } = new Spell
        {
            Name = "Storm's Eye",
            ID = 45,
            Level = 50,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };
    }
}
