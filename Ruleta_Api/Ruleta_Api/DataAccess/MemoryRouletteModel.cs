using Ruleta_Api.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ruleta_Api.DataAccess {
    public class MemoryRouletteModel : AbstracRouletteModel {
        private ConcurrentDictionary<String, Roulette> roulettes;
        private ConcurrentDictionary<String, List< BetBoard>> boards;
        private ConcurrentDictionary<String, Player> players;
        private static long counter = 0;
        public MemoryRouletteModel():base() {
            roulettes = new ConcurrentDictionary<string, Roulette>();
            boards = new ConcurrentDictionary<string, List<BetBoard>>();
            players = new ConcurrentDictionary<string, Player>();
            //some random players
            for(int index = 0; index < 30; index++) {
                var random = new Random(Thread.CurrentThread.ManagedThreadId* (index+1));
                string id = index.ToString();
                var _credit = (decimal)(random.Next() * random .Next(0, 1000));
                players.GetOrAdd(index.ToString(), new Player(id, _credit) ); 
            }
        }
        public override string CreateRoullete() {
            //Adding in a Thread Safe Way
           long new_id = Interlocked.Increment(ref counter);
            roulettes.GetOrAdd(""+new_id, (string id) => {
                return new Roulette(new_id);
            });
            return "" + new_id;
        }
        public override Roulette GetRoulette(string serial) {
            Roulette roulete = null;
            roulettes.TryGetValue(serial, out roulete);
            return roulete;
        }
        public override IEnumerable<Roulette> GetRoulettes() {
            return roulettes.Values;
        }
        public override bool OpenRoulette(string serial) {
            bool result = false;
            Roulette roulete = null;
            if(roulettes.TryGetValue(serial, out roulete)) {
                BetBoard board = new BetBoard(roulete);
                roulete.StartGame(board);

                var bag = boards.GetOrAdd(serial, (serial) => {
                    return new List<BetBoard>();
                });
                bag.Add(roulete.BetBoard);
            }
            return result;
        }
        public override Bet CloseRoulette(string serial) {
            bool result = false;
            Roulette roulete = null;
            if(roulettes.TryGetValue(serial, out roulete)) {
               var winner_bet =  roulete.PlayRouletteAndGetTheWinner();
                roulete.FinalizeGame();

                return winner_bet;
            }
            return null;
        }
        public override Dictionary<string, List<BetBoard>> GetRoulettesBoards() {
            //a copy avoid thread problems
            return new Dictionary<string, List<BetBoard>>(this.boards);
        }

        public override bool AddBet(string serial, Bet bet) {
            var result = false;
            try {

                Roulette Roulette = null;
                if(roulettes.TryGetValue(serial, out Roulette)) {
                    var bet_board = Roulette.BetBoard;
                    if(bet_board != null) {
                        result =  bet_board.AddBet(bet);
                    }
                }
            }
            catch(Exception ) {
                throw;
            }
            return result;
        }

        public override bool PlayerHasCredit(Bet bet) {
            var result = false;
            Player player = null;
            if(players.TryGetValue(bet.PlayerId, out player)) {

               result =  player.Credit >= bet.BetAmount;
            }
            return result;  
        }
    }
}
