﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowCR.Spells.Role;

namespace ShadowCR.Spells
{
    public class ScholarSpells : ArcanistSpells
    {
        new public HealerSpells Role { get; } = new HealerSpells();

        public Spell Adloquium { get; } = new Spell
        {
            Name = "Adloquium",
            ID = 185,
            Level = 30,
            GCDType = GCDType.On,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell Succor { get; } = new Spell
        {
            Name = "Succor",
            ID = 186,
            Level = 35,
            GCDType = GCDType.On,
            SpellType = SpellType.Heal,
            CastType = CastType.Self
        };

        public Spell SacredSoil { get; } = new Spell
        {
            Name = "Sacred Soil",
            ID = 188,
            Level = 45,
            GCDType = GCDType.Off,
            SpellType = SpellType.Heal,
            CastType = CastType.TargetLocation
        };

        public Spell Lustrate { get; } = new Spell
        {
            Name = "Lustrate",
            ID = 189,
            Level = 50,
            GCDType = GCDType.Off,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell Indomitability { get; } = new Spell
        {
            Name = "Indomitability",
            ID = 3583,
            Level = 52,
            GCDType = GCDType.Off,
            SpellType = SpellType.Heal,
            CastType = CastType.Self
        };

        public Spell Broil { get; } = new Spell
        {
            Name = "Broil",
            ID = 3584,
            Level = 54,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell DeploymentTactics { get; } = new Spell
        {
            Name = "Deployment Tactics",
            ID = 3585,
            Level = 56,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Target
        };

        public Spell EmergencyTactics { get; } = new Spell
        {
            Name = "Emergency Tactics",
            ID = 3586,
            Level = 58,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Dissipation { get; } = new Spell
        {
            Name = "Dissipation",
            ID = 3587,
            Level = 60,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };

        public Spell Excogitation { get; } = new Spell
        {
            Name = "Excogitation",
            ID = 7434,
            Level = 62,
            GCDType = GCDType.Off,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell BroilII { get; } = new Spell
        {
            Name = "Broil II",
            ID = 7435,
            Level = 64,
            GCDType = GCDType.On,
            SpellType = SpellType.Damage,
            CastType = CastType.Target
        };

        public Spell ChainStrategem { get; } = new Spell
        {
            Name = "Chain Strategem",
            ID = 7436,
            Level = 66,
            GCDType = GCDType.Off,
            SpellType = SpellType.Cooldown,
            CastType = CastType.Target
        };

        public Spell Aetherpact { get; } = new Spell
        {
            Name = "Aetherpact",
            ID = 7437,
            Level = 70,
            GCDType = GCDType.Off,
            SpellType = SpellType.Heal,
            CastType = CastType.Target
        };

        public Spell DissolveUnion { get; } = new Spell
        {
            Name = "Dissolve Union",
            ID = 7869,
            Level = 70,
            GCDType = GCDType.Off,
            SpellType = SpellType.Buff,
            CastType = CastType.Self
        };
    }
}
