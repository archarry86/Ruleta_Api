using Ruleta_Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.ResponseModel {
    public class RouletteInfo {
        public Roulette Roulette { get; set; }
        public IList<BetBoard> BetBoardList { get; set; }
    }
}
