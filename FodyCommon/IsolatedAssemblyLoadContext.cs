﻿using System;

#if NET46
public class IsolatedAssemblyLoadContext
{
    AppDomain appDomain;

    public IsolatedAssemblyLoadContext(string friendlyName, string applicationBase)
    {
        var appDomainSetup = new AppDomainSetup
        {
            ApplicationBase = applicationBase,
        };
        appDomain = AppDomain.CreateDomain(friendlyName, null, appDomainSetup);
    }

    public object CreateInstanceFromAndUnwrap(string assemblyPath, string typeName)
    {
        return appDomain.CreateInstanceFromAndUnwrap(assemblyPath, typeName);
    }

    public void Unload()
    {
        AppDomain.Unload(appDomain);
    }
}
#else
using System.Reflection;
using System.Runtime.Loader;

public class IsolatedAssemblyLoadContext : AssemblyLoadContext
{
    public IsolatedAssemblyLoadContext(string friendlyName, string applicationBase)
    {
    }

    /// <inheritdoc />
    protected override Assembly Load(AssemblyName assemblyName)
    {
        return null;
    }

    public object CreateInstanceFromAndUnwrap(string assemblyPath, string typeName)
    {
        var assembly = LoadFromAssemblyPath(assemblyPath);
        return assembly.CreateInstance(typeName);
    }

    public void Unload()
    {
        // Not supported on .NET Core yet
    }
}
#endif