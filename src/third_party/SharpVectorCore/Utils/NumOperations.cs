using System;

namespace SharpVectorCore.Utils
{
    public static class NumOperations
    {
        public static bool GreaterOrEquals(float left, float right) => left > right || Equals(left, right);
        public static bool LesserOrEquals(float left, float right) => left < right || Equals(left, right);
        public static bool Equals(float left, float right) => Math.Abs(left - right) < float.Epsilon;

        public static bool GreaterOrEquals(double left, double right) => left > right || Equals(left, right);
        public static bool LesserOrEquals(double left, double right) => left < right || Equals(left, right);
        public static bool Equals(double left, double right) => Math.Abs(left - right) < double.Epsilon;
    }
}
