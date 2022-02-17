namespace Core {
    public static class Utils {
        public static bool TryConvertVal<A, B>(A obj, out B returnVal) {
            if (obj is B b) {
                returnVal = b;
                return true;
            }
            returnVal = default(B);
            return false;
        }
    }
}