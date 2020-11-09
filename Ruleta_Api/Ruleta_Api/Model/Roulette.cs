using Ruleta_Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ruleta_Api.Model {
    public class Roulette {
        public RouletteState state { get; private set; }
        public long id { get; private set; }
        public HashSet<BetPosibility> Bets { get; private set; }
        public BetBoard BetBoard { get; private set; }
        public Roulette(long id) {
            id = this.id;
            Bets = new HashSet<BetPosibility>();
            for(int index = 0; index <= BetPosibility.Max_Number; index++) {
                var index_mod_two = index % 2;
                var bet = new BetPosibility(BetColor._none.GetColorByIndex(index_mod_two), ( index  ));
                var result = Bets.Add(bet);
            }
            state = RouletteState.waiting;
        }
        public void StartGame(BetBoard BetBoard) { 
            
            if(this.BetBoard != null)
                throw new InvalidOperationException("This Roullete has not finished the game.");
            if(state != RouletteState.waiting)
                throw new InvalidOperationException("The Roullete can not be started.");
            if(BetBoard == null)
                throw new ArgumentNullException("Invalid Parameter to start the game.");
            this.BetBoard = BetBoard;
            state = RouletteState.on_playing;
        }
        public void FinalizeGame() {
            this.BetBoard = null;
            state = RouletteState.waiting;
        }
        public Bet PlayRouletteAndGetTheWinner() {
            if(BetBoard == null) {
                throw new InvalidOperationException("This Roullete is not ready to play, you must first star a game.");
            }

            if(state != RouletteState.on_playing)
                throw new InvalidOperationException("The Roullete is already Closed.");

            
            state = RouletteState.calculating_winner;
            //here would be ok if I inclued a sime prime number
            var i = Thread.CurrentThread.ManagedThreadId * this.BetBoard.Id.GetHashCode();
            Random r = new Random(i);
            var result =  (r.Next()* Bets.Count) ;
            return this.BetBoard.BetPlayerWinner(result);
        }
    }
}
