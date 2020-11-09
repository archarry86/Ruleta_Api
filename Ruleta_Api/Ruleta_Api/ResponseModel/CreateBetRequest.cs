using Ruleta_Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.ResponseModel {
    public class CreateBetRequest {
        public string id_roulette { get; set; }

        public Bet bet { get; set; }
        public CreateBetRequest() {
           
        }
    }
}
