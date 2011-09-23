using System;

namespace Rholiver.Mvvm.Infrastructure
{
    public static class Extensions
    {
        public static string Fmt(this string format, params object[] args) {
            return String.Format(format, args);
        }
    }
}