﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.BlackMage;
using static ShadowCR.Constants;

namespace ShadowCR.Rotations
{
    public class ThaumaturgeActions
    {
        private ThaumaturgeSpells Spellbook { get; } = new ThaumaturgeSpells();
    }
}
