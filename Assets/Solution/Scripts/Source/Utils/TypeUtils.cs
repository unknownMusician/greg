using System;

namespace Greg.Utils
{
    public static class TypeUtils
    {
        /// <summary>
        /// Retrieves the type without boxing if that is possible (if T is sealed).
        /// Otherwise, calls value.GetType().
        /// </summary>
        public static Type GetType<T>(T value)
        {
            return typeof(T).IsSealed switch
            {
                true => typeof(T),
                false => value.GetType(),
            };
        }
    }
}
