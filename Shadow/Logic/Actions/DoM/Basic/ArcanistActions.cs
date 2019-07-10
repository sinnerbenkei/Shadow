using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ShadowCR.Settings;
using ShadowCR.Spells;
using Resource = ff14bot.Managers.ActionResourceManager.Arcanist;
using static ShadowCR.Constants;

namespace ShadowCR.Rotations
{
    public class ArcanistActions
    {
        private ArcanistSpells Spellbook { get; } = new ArcanistSpells();
    }
}
