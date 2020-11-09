using Ruleta_Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.DataAccess {
    public class RedisRoulleteModel : AbstracRouletteModel {
        public override bool AddBet(string serial, Bet bet) {
            throw new NotImplementedException();
        }

        public override void AddCreditToThePlayer(string playerId, decimal new_amount) {
            throw new NotImplementedException();
        }

        public override Bet CloseRoulette(string serial) {
            throw new NotImplementedException();
        }

        public override string CreateRoullete() {
            throw new NotImplementedException();
        }

        public override Roulette GetRoulette(string serial) {
            throw new NotImplementedException();
        }

        public override IEnumerable<Roulette> GetRoulettes() {
            throw new NotImplementedException();
        }

        public override Dictionary<string, List<BetBoard>> GetRoulettesBoards() {
            throw new NotImplementedException();
        }

        public override bool OpenRoulette(string serial) {
            throw new NotImplementedException();
        }

        public override bool PlayerHasCredit(Bet bet) {
            throw new NotImplementedException();
        }
    }
}
