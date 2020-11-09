using Ruleta_Api.Model;
using Ruleta_Api.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.Contracts {
    interface IRouletteContract {
        ApiWrapperResponse<String> CreateRoulette();
        ApiWrapperResponse<bool> OpenRoulette(string serila);
        ApiWrapperResponse<string> CreateBet(CreateBetRequest request);
        ApiWrapperResponse<IList<RouletteInfo>> GetRoulettes(CreateBetRequest request);
        ApiWrapperResponse<Bet> CloseTheRoulette(string serial);
    }
}
