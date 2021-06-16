using System;

namespace d04_ex03
{
    public static class TypeFactory
    {
        public static T CreateWithConstructor<T>() where T : class =>
            (T)typeof(T).GetConstructor(Type.EmptyTypes)?.Invoke(null);

        public static T CreateWithActivator<T>() where T : class =>
            (T)Activator.CreateInstance(typeof(T));

        public static T CreateWithParameters<T>(object[] args) where T : class =>
            (T)Activator.CreateInstance(typeof(T), args);
    }
}
