﻿using System.Reflection;

namespace WalletApi.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Assembly).Assembly;
}
