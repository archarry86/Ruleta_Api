using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.Exceptions {
    public class RouletteCreationException : Exception {
        public RouletteCreationException(string message) : base(message) {
        }
        public RouletteCreationException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
