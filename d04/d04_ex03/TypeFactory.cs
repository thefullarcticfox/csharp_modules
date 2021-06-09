using System;
using System.Reflection;

namespace d04_ex03
{
    class TypeFactory
    {
        public static T CreateWithConstructor<T>() where T : class
        {
            ConstructorInfo ctorInfo = typeof(T)
                .GetConstructor(
                    BindingFlags.Instance | BindingFlags.Public,
                    null, CallingConventions.HasThis, Array.Empty<Type>(), null
                );
            return (T)ctorInfo?.Invoke(Array.Empty<object>());
        }

        public static T CreateWithActivator<T>() where T : class =>
            (T)Activator.CreateInstance(typeof(T));

        public static T CreateWithParameters<T>(object[] args) where T : class =>
            (T)Activator.CreateInstance(typeof(T), args);
    }
}
