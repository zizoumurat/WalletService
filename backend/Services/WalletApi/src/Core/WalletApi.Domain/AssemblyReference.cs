using System.Reflection;

namespace WalletApi.Domain;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Assembly).Assembly;
}
