using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.Exceptions {
    public class BetBoardStateException : Exception {
        public BetBoardStateException(string message) : base(message) {
        }
        public BetBoardStateException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
