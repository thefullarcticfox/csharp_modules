using System;
using System.Reflection;

namespace d04_ex03
{
    public static class TypeFactory
    {
        public static T CreateWithConstructor<T>() where T : class
        {
            ConstructorInfo ctorInfo = typeof(T).GetConstructor(Type.EmptyTypes);
            return (T)ctorInfo?.Invoke(null);
        }

        public static T CreateWithActivator<T>() where T : class =>
            (T)Activator.CreateInstance(typeof(T));

        public static T CreateWithParameters<T>(object[] args) where T : class =>
            (T)Activator.CreateInstance(typeof(T), args);
    }
}
