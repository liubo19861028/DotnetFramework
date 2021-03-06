﻿using System;
using System.Reflection;

namespace Dotnet.Dependency
{
    /// <summary>容器接口
    /// </summary>
    public interface IIocContainer
    {
        void UseEngine(object container);
        T GetEngine<T>();
        T Resolve<T>();
        T Resolve<T>(Type type);
        T Resolve<T>(object argumentsAsAnonymousType);
        object Resolve(Type type, params object[] args);
        T ResolveNamed<T>(string serviceName);

        object Resolve(Type type);
        bool IsRegistered(Type type);
        bool IsRegistered<T>();

        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton, string serviceName = null)
            where T : class;

        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton, string serviceName = null);

        void Register<T>(T impl) where T : class;

        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton, string serviceName = null)
            where TType : class
            where TImpl : class, TType;

        void Register<TType, TImpl>(TImpl impl)
            where TType : class
            where TImpl : class, TType;


        void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton, string serviceName = null, bool propertiesAutowired = true, bool isDefault = false);

        void RegisterAssemblyByConvention(Assembly assembly);
    }
}
