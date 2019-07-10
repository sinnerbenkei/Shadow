using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class SummonerSpells : ArcanistSpells
    {
        public Spell SummonIII { get; } = new Spell
        {
            Name = "Summon III",
            ID = 180,
            Level = 30,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Fester { get; } = new Spell
        {
            Name = "Fester",
            ID = 181,
            Level = 35,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell Contagion { get; } = new Spell
        {
            Name = "Contagion",
            ID = 795,
            Level = 40,
            GCDType = GCDType.On,
            SpellType = SpellType.Pet,
            CastType = CastType.Target
        };

        public Spell Enkindle { get; } = new Spell
        {
            Name = "Enkindle",
            ID = 184,
            Level = 50,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell Painflare { get; } = new Spell
        {
            Name = "Painflare",
            ID = 3578,
            Level = 52,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell RuinIII { get; } = new Spell
        {
            Name = "Ruin III",
            ID = 3579,
            Level = 54,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell TriDisaster { get; } = new Spell
        {
            Name = "Tri-disaster",
            ID = 3580,
            Level = 56,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell DreadwyrmTrance { get; } = new Spell
        {
            Name = "Dreadwyrm Trance",
            ID = 3581,
            Level = 58,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Deathflare { get; } = new Spell
        {
            Name = "Deathflare",
            ID = 3582,
            Level = 60,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell RuinIV { get; } = new Spell
        {
            Name = "Ruin IV",
            ID = 7426,
            Level = 62,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Aetherpact { get; } = new Spell
        {
            Name = "Aetherpact",
            ID = 7423,
            Level = 64,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell BioIII { get; } = new Spell
        {
            Name = "Bio III",
            ID = 7424,
            Level = 66,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell MiasmaIII { get; } = new Spell
        {
            Name = "Miasma III",
            ID = 7425,
            Level = 66,
            GCDType = GCDType.On,
            SpellType = SpellType.DoT,
            CastType = CastType.Target
        };

        public Spell SummonBahamut { get; } = new Spell
        {
            Name = "Summon Bahamut",
            ID = 7427,
            Level = 70,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell EnkindleBahamut { get; } = new Spell
        {
            Name = "Enkindle Bahamut",
            ID = 7429,
            Level = 70,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };
    }
}
