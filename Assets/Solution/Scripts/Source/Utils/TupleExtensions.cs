namespace Greg.Utils
{
    public static class TupleExtensions
    {
        public static (T1, T0) Swap<T0, T1>(this (T0, T1) tuple)
        {
            var (v0, v1) = tuple;
            return (v1, v0);
        }
    }
}
