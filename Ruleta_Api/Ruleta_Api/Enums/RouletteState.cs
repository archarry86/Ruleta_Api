using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.Enums {
    public enum RouletteState {
        _none,
        on_playing,
        waiting,
        calculating_winner,
    }
}
