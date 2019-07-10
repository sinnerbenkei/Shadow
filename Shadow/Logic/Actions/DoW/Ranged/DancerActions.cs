﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Dancer;
using static ShadowCR.Constants;

namespace ShadowCR.Rotations
{
    public class DancerActions : IRangedActions
    {
        private DancerSpells Spellbook { get; } = new DancerSpells();
    }
}
