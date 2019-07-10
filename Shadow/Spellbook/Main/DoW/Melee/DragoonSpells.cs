using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class DragoonSpells : LancerSpells
    {
        public Spell Jump { get; } = new Spell
        {
            Name = "Jump",
            ID = 92,
            Level = 30,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell ElusiveJump { get; } = new Spell
        {
            Name = "Elusive Jump",
            ID = 94,
            Level = 35,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell DoomSpike { get; } = new Spell
        {
            Name = "Doom Spike",
            ID = 86,
            Level = 40,
            GCDType = GCDType.On,
            SpellType = SpellType.AoE,
            CastType = CastType.Target
        };

        public Spell SpineshatterDive { get; } = new Spell
        {
            Name = "Spineshatter Dive",
            ID = 95,
            Level = 45,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell DragonfireDive { get; } = new Spell
        {
            Name = "Dragonfire Dive",
            ID = 96,
            Level = 50,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell BattleLitany { get; } = new Spell
        {
            Name = "Battle Litany",
            ID = 3557,
            Level = 52,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell BloodOfTheDragon { get; } = new Spell
        {
            Name = "Blood of the Dragon",
            ID = 3553,
            Level = 54,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell FangAndClaw { get; } = new Spell
        {
            Name = "Fang and Claw",
            ID = 3554,
            Level = 56,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell WheelingThrust { get; } = new Spell
        {
            Name = "Wheeling Thrust",
            ID = 3556,
            Level = 58,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Geirskogul { get; } = new Spell
        {
            Name = "Geirskogul",
            ID = 3555,
            Level = 60,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell SonicThrust { get; } = new Spell
        {
            Name = "Sonic Thrust",
            ID = 7397,
            Level = 62,
            GCDType = GCDType.On,
            SpellType = SpellType.AoE,
            CastType = CastType.Target
        };

        public Spell DragonSight { get; } = new Spell
        {
            Name = "Dragon Sight",
            ID = 7398,
            Level = 66,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Target
        };

        public Spell MirageDive { get; } = new Spell
        {
            Name = "Mirage Dive",
            ID = 7399,
            Level = 68,
            GCDType = GCDType.Off,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Nastrond { get; } = new Spell
        {
            Name = "Nastrond",
            ID = 7400,
            Level = 70,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };
    }
}
