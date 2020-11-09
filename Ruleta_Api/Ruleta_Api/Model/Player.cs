using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.Model {
    public class Player {
        public string Id { get; private set; }
        public Decimal Credit { get; private set; }
        public Player(string id, decimal credit) {
            this.Id = id;
            Credit = credit;
        }
    }
}
