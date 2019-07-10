using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class BlackMageSpells : ThaumaturgeSpells
    {
        public Spell Convert { get; } = new Spell
        {
            Name = "Convert",
            ID = 158,
            Level = 30,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Freeze { get; } = new Spell
        {
            Name = "Freeze",
            ID = 159,
            Level = 35,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.TargetLocation
        };

        public Spell BlizzardIII { get; } = new Spell
        {
            Name = "Blizzard III",
            ID = 154,
            Level = 40,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell ThunderIII { get; } = new Spell
        {
            Name = "Thunder III",
            ID = 153,
            Level = 45,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell Thundercloud { get; } = new Spell
        {
            Name = "Thundercloud",
            ID = 153,
            Level = 45,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Flare { get; } = new Spell
        {
            Name = "Flare",
            ID = 162,
            Level = 50,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell LeyLines { get; } = new Spell
        {
            Name = "Ley Lines",
            ID = 3573,
            Level = 52,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Sharpcast { get; } = new Spell
        {
            Name = "Sharpcast",
            ID = 3574,
            Level = 54,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Enochian { get; } = new Spell
        {
            Name = "Enochian",
            ID = 3575,
            Level = 56,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell BlizzardIV { get; } = new Spell
        {
            Name = "Blizzard IV",
            ID = 3576,
            Level = 58,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell FireIV { get; } = new Spell
        {
            Name = "Fire IV",
            ID = 3577,
            Level = 60,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell BetweenTheLines { get; } = new Spell
        {
            Name = "Between The Lines",
            ID = 7419,
            Level = 62,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell ThunderIV { get; } = new Spell
        {
            Name = "Thunder IV",
            ID = 7420,
            Level = 64,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell Triplecast { get; } = new Spell
        {
            Name = "Triplecast",
            ID = 7421,
            Level = 66,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Foul { get; } = new Spell
        {
            Name = "Foul",
            ID = 7422,
            Level = 70,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };
    }
}
