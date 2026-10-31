using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLigaSystem.Common
{
    public class LeagueException : Exception
    {
        public LeagueException(string message) : base(message) { }
    }
}
