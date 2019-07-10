using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class GunbreakerSpells
    {
        public TankSpells Role { get; } = new TankSpells();

        public Spell KeenEdge { get; } = new Spell
        {
            Name = "Keen Edge",
            ID = 16137,
            Level = 1,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell NoMercy { get; } = new Spell
        {
            Name = "No Mercy",
            ID = 3619,
            Level = 2,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell BrutalShell { get; } = new Spell
        {
            Name = "Brutal Shell",
            ID = 16139,
            Level = 4,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Camouflage { get; } = new Spell
        {
            Name = "Camouflage",
            ID = 16140,
            Level = 6,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell DemonSlice { get; } = new Spell
        {
            Name = "Demon Slice",
            ID = 16141,
            Level = 10,
            GCDType = GCDType.On,
            SpellType = SpellType.AoE,
            CastType = CastType.Self
        };

        public Spell RoyalGuard { get; } = new Spell
        {
            Name = "Royal Guard",
            ID = 16142,
            Level = 10,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell LightningShot { get; } = new Spell
        {
            Name = "Lightning Shot",
            ID = 16143,
            Level = 15,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell DangerZone { get; } = new Spell
        {
            Name = "Danger Zone",
            ID = 16144,
            Level = 18,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell SolidBarrel { get; } = new Spell
        {
            Name = "Solid Barrel",
            ID = 16145,
            Level = 26,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell BurstStrike { get; } = new Spell
        {
            Name = "Burst Strike",
            ID = 16162,
            Level = 30,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell Nebula { get; } = new Spell
        {
            Name = "Nebula",
            ID = 16148,
            Level = 38,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell DemonSlaughter { get; } = new Spell
        {
            Name = "Demon Slaughter",
            ID = 16149,
            Level = 40,
            GCDType = GCDType.On,
            SpellType = SpellType.AoE,
            CastType = CastType.Self
        };

        public Spell Aurora { get; } = new Spell
        {
            Name = "Aurora",
            ID = 16151,
            Level = 45,
            GCDType = GCDType.Off,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell Superbolide { get; } = new Spell
        {
            Name = "Superbolide",
            ID = 16152,
            Level = 50,
            GCDType = GCDType.On,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell SonicBreak { get; } = new Spell
        {
            Name = "Sonic Break",
            ID = 16153,
            Level = 54,
            GCDType = GCDType.On,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell RoughDivide { get; } = new Spell
        {
            Name = "Rough Divide",
            ID = 16154,
            Level = 56,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell GnashingFang { get; } = new Spell
        {
            Name = "Gnashing Fang",
            ID = 16146,
            Level = 60,
            GCDType = GCDType.On,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell SavageClaw { get; } = new Spell
        {
            Name = "Savage Claw",
            ID = 16147,
            Level = 60,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell WickedTalon { get; } = new Spell
        {
            Name = "Wicked Talon",
            ID = 16150,
            Level = 60,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell BowShock { get; } = new Spell
        {
            Name = "Bow Shock",
            ID = 16159,
            Level = 62,
            GCDType = GCDType.Off,
            SpellType = SpellType.AoE,
            CastType = CastType.Self
        };

        public Spell HeartOfLight { get; } = new Spell
        {
            Name = "Heart of Light",
            ID = 16160,
            Level = 64,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell HeartOfStone { get; } = new Spell
        {
            Name = "Heart of Stone",
            ID = 16161,
            Level = 68,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Target
        };

        public Spell Continuation { get; } = new Spell
        {
            Name = "Continuation",
            ID = 16155,
            Level = 70,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        //public Spell FatedCircle { get; } = new Spell
        //{
        //    Name = "Fated Circle",
        //    ID = 0,
        //    Level = 62,
        //    GCDType = GCDType.Off,
        //    SpellType = SpellType.AoE,
        //    CastType = CastType.Self
        //};

        //public Spell Bloodfest { get; } = new Spell
        //{
        //    Name = "Bloodfest",
        //    ID = 0,
        //    Level = 62,
        //    GCDType = GCDType.Off,
        //    SpellType = SpellType.AoE,
        //    CastType = CastType.Self
        //};

        //public Spell BlastingZone { get; } = new Spell
        //{
        //    Name = "Blasting Zone",
        //    ID = 0,
        //    Level = 62,
        //    GCDType = GCDType.Off,
        //    SpellType = SpellType.AoE,
        //    CastType = CastType.Self
        //};

        public Spell JugularRip { get; } = new Spell
        {
            Name = "Jugular Rip",
            ID = 16156,
            Level = 70,
            GCDType = GCDType.Off,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell AbdomenTear { get; } = new Spell
        {
            Name = "Abdomen Tear",
            ID = 16157,
            Level = 70,
            GCDType = GCDType.Off,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell EyeGouge { get; } = new Spell
        {
            Name = "Eye Gouge",
            ID = 16158,
            Level = 70,
            GCDType = GCDType.Off,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };
    }
}
