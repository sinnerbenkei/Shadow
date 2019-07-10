using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowCR.Spells.Role
{
    public class HealerSpells
    {
        public Spell Repose { get; } = new Spell
        {
            Name = "Repose",
            ID = 0,
            Level = 8,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Target
        };

        public Spell Esuna { get; } = new Spell
        {
            Name = "Esuna",
            ID = 7568,
            Level = 10,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Target
        };

        public Spell LucidDreaming { get; } = new Spell
        {
            Name = "Lucid Dreaming",
            ID = 7562,
            Level = 24,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Swiftcast { get; } = new Spell
        {
            Name = "Swiftcast",
            ID = 7561,
            Level = 18,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Surecast { get; } = new Spell
        {
            Name = "Surecast",
            ID = 7559,
            Level = 44,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Rescue { get; } = new Spell
        {
            Name = "Rescue",
            ID = 7571,
            Level = 48,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Target
        };
    }
}
