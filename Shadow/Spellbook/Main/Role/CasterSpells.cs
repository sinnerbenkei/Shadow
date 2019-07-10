using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowCR.Spells.Role
{
    public class CasterSpells
    {
        public Spell Addle { get; } = new Spell
        {
            Name = "Addle",
            ID = 7560,
            Level = 8,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell LucidDreaming { get; } = new Spell
        {
            Name = "Lucid Dreaming",
            ID = 7562,
            Level = 24,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Swiftcast { get; } = new Spell
        {
            Name = "Swiftcast",
            ID = 7561,
            Level = 18,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Surecast { get; } = new Spell
        {
            Name = "Surecast",
            ID = 7559,
            Level = 44,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };
    }
}
