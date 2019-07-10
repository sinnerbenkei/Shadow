using ff14bot;
using ff14bot.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowCR
{
    public class Constants
    {
        internal static LocalPlayer Me => Core.Me;
        internal static GameObject Target => Core.Player.CurrentTarget;
        internal static GameObject Pet => Core.Me.Pet;
    }
}
