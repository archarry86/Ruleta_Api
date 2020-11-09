using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.Model {
    public class Bet {
        public decimal Max_amount_value = 10_000;
        public decimal Min_amount_value = 0;
        public BetPosibility BetSelected { get; private set; }
        public string PlayerId { get; private set; }
        public decimal BetAmount { get; private set; }

        public Bet(BetPosibility betValue, string playerid, decimal betamount) {
            if(betValue.Number < BetPosibility.Min_Number || betValue.Number > BetPosibility.Max_Number)
                throw new ArgumentException("The number of the Bet is invalid.");
            if(string.IsNullOrEmpty(playerid))
                throw new ArgumentNullException("Player id must not be neither null nor empty");
            if(betamount < Min_amount_value || betamount > Max_amount_value)
                throw new ArgumentException("The amount of the bet is invalid.");
            this.BetSelected = betValue;
            this.PlayerId = playerid;
            this.BetAmount = betamount;
        }

        internal void AssingPlayerId(StringValues playerId) {
            PlayerId = playerId;
        }
    }
}
