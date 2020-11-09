using Ruleta_Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.DataAccess
{
    public abstract class AbstracRouletteModel {
        public AbstracRouletteModel() { 
        }
        public abstract string CreateRoullete();
        public abstract bool OpenRoulette(string serial);
        public abstract bool AddBet(string serial , Bet bet);
        public abstract Bet CloseRoulette(string serial);
        public abstract Roulette GetRoulette(string serial);
        public abstract IEnumerable<Roulette> GetRoulettes();
        public abstract Dictionary<string,List<BetBoard>> GetRoulettesBoards();
        public abstract bool PlayerHasCredit(Bet bet);
         
    }
}
