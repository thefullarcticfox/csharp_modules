using System;

namespace d04_ex02.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoDisplayAttribute : Attribute {}
}
