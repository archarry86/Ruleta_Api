using Ruleta_Api.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.Model {
    public class BetPosibility:IEquatable<BetPosibility>,IComparable<BetPosibility> {
        public const int Max_Number = 36;
        public const int Min_Number = 0;
        public BetColor Color { get; private set; }
        public int Number { get; private set; }
        public BetPosibility(BetColor color, int number) {
            Color = color;
            Number = number;
        }
        public int CompareTo(BetPosibility obj) {
            if(obj == null)
                return 1;
            return this.Number - obj.Number;
        }
        public bool Equals(BetPosibility other) {
            return other != null && ( other.Color == this.Color && other.Number == this.Number);
        }
        public override bool Equals(Object other) {
            return Equals(other as BetPosibility);
        }
        public override int GetHashCode() {
            return HashCode.Combine(Color, Number);
        }
        public override string ToString() {
            return $"Bet Color {Color}, Number {Number}";
        }
    }
}
