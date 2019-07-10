using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Bard;
using static ShadowCR.Constants;

namespace ShadowCR.Rotations
{
    public class ArcherActions : IRangedActions
    {
        private ArcherSpells Spellbook { get; } = new ArcherSpells();
    }
}
