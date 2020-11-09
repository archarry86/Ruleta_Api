using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.Enums {
    public enum BetColor {
        _none,
        Black,
        Red        
    }
    public static class BetColorExtencions {
        public static BetColor GetColorByIndex(this BetColor color, int index) {
            switch(index){
                case 0:
                   return  BetColor.Red;
                case 1:
                    return BetColor.Black;
                default:
                    return BetColor._none;
                    break;
            }
        }
    }
}
