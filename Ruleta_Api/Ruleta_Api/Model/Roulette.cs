using Ruleta_Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ruleta_Api.Model {
    public class Roulette {
        public HashSet<BetPosibility> Bets { get; private set; }
        private BetBoard BetBoard;
        public Roulette() {
            Bets = new HashSet<BetPosibility>();
            for(int index = 0; index <= BetPosibility.Max_Number; index++) {
                var index_mod_two = index % 2;
                var bet = new BetPosibility(BetColor._none.GetColorByIndex(index_mod_two), ( index  ));
                var result = Bets.Add(bet);
            }
        }
        public void StartGame(BetBoard BetBoard) {            
            if(this.BetBoard != null)
                throw new InvalidOperationException("This Roullete has not finished the game.");
            if(BetBoard == null)
                throw new ArgumentNullException("This Roullete has not finished the game.");
            this.BetBoard = BetBoard;
        }
        public void FinalizeGame() {
            this.BetBoard = null;
        }
        public Bet PlayRouletteAndGetTheWinner() {
            if(BetBoard == null) {
                throw new InvalidOperationException("This Roullete is not ready to play, you must first star a game.");
            }
            //here would be ok if I inclued a sime prime number
            var i = Thread.CurrentThread.ManagedThreadId * this.BetBoard.Id.GetHashCode();
            Random r = new Random(i);
            var result =  (r.Next()* Bets.Count) ;
            return this.BetBoard.BetPlayer(result);
        }
    }
}
