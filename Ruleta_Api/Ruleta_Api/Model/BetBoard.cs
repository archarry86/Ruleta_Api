using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Ruleta_Api.Enums;
using Ruleta_Api.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.Model {
    public class BetBoard {
        private Roulette Roulette { get; }
        public Guid Id { get; private set; }
        public BetBoardState State { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? FinalizationDate { get; private set; }
        public DateTime? CancelationDate { get; private set; }
        public Bet winner { get; private set; }
        //Structure to manage the concurrence
        private ConcurrentDictionary<String,Bet> Bets;
        public BetBoard(Roulette _roulette, Guid id) {
            if(Roulette == null)
                throw new ArgumentNullException("The Roulette can not be null.");
            if(id == Guid.Empty) {
                throw new ArgumentException("The id can not be empty.");
            }
            this.Roulette = _roulette;
            Id = id;
            State = BetBoardState.opend;
            CreationDate = DateTime.Now;        
            Bets = new ConcurrentDictionary<string, Bet>();
        }
        public BetBoard(Roulette roulette) : this(roulette, Guid.NewGuid()) { 
        
        }
        public bool AddBet(Bet newBet) {
            bool result = false;
            if(State != BetBoardState.opend) {
                //this message should be on a lang file
                throw new BetBoardStateException("This Roullete is not ready to recieve Bets.");
            }
            if(Roulette.Bets.Any(rbet => rbet == newBet.BetSelected)) {
                throw new InvalidOperationException("The Bet can not be played in this Roulette.");
            }
            bool exits = true;
            exits = Bets.Any(p => p.Value.BetSelected == newBet.BetSelected);
            if(exits) {
                throw new InvalidOperationException("The Bet was alredy Played");
            }
            exits = true;
            Bets.GetOrAdd(newBet.PlayerId, (string id)=>{
                exits = false;
                return newBet;
            });
            if(exits){
                throw new InvalidOperationException("The Player has already played a Bet.");
            }
            result = true;
            return result;
        }
      
        public void CancelTheBoard() {
            this.State = BetBoardState.canceled;
            Roulette.FinalizeGame();
            this.CancelationDate = DateTime.Now;
        }
        public Bet BetPlayerWinner( int number ) {
            if(winner != null) {
                throw new InvalidOperationException("The winner player has been already chossen");
            }
            if(this.State != BetBoardState.opend) {
                throw new InvalidOperationException("The winner player has been already chossen");
            }
            this.State = BetBoardState.calculating_winner;
            
            KeyValuePair<String, Bet> entry =  Bets.FirstOrDefault(P => P.Value.BetSelected.Number == number);
            winner = entry.Value;
            this.State = BetBoardState.finished;
            return winner;
        }
    }
}
