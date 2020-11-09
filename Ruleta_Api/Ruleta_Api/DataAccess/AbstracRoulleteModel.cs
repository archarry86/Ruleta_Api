using Ruleta_Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.DataAccess
{
    public abstract class AbstracRoulleteModel {
        public AbstracRoulleteModel() { 
        }
        public abstract string CreateRoullete(Roulette newRoullete);
        public abstract string SaveBetBoard(BetBoard betBoard);
        public abstract Roulette GetRoulette(string serial);
        public abstract Roulette GetRoulettes();

    }
}
