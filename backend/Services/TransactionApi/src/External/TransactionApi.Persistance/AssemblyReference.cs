using System.Reflection;

namespace TransactionApi.Persistance;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Assembly).Assembly;
}
