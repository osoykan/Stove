﻿using System.Reflection;

using Autofac.Extras.IocManager;

using Stove.Configuration;
using Stove.Domain.Uow;
using Stove.Events.Bus;

namespace Stove
{
    public static class StoveRegistrationExtensions
    {
        public static IIocBuilder UseStove(this IIocBuilder builder)
        {
            RegisterDefaults(builder);
            return builder;
        }

        private static void RegisterDefaults(IIocBuilder builder)
        {
            builder.RegisterServices(r => r.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly()));
            builder.RegisterServices(r => r.Register<IGuidGenerator>(context => SequentialGuidGenerator.Instance));
            builder.RegisterServices(r => r.Register<IStoveStartupConfiguration, StoveStartupConfiguration>(Lifetime.Singleton));
        }

        public static IIocBuilder UseTypedConnectionStringResolver(this IIocBuilder builder)
        {
            builder.RegisterServices(r => r.Register<IConnectionStringResolver, TypedConnectionStringResolver>());
            return builder;
        }

        public static IIocBuilder UseDefaultConnectionStringResolver(this IIocBuilder builder)
        {
            builder.RegisterServices(r => r.Register<IConnectionStringResolver, DefaultConnectionStringResolver>());
            return builder;
        }

        public static IIocBuilder UseDefaultEventBus(this IIocBuilder builder)
        {
            builder.RegisterServices(r => r.Register<IEventBus>(context => EventBus.Default));
            return builder;
        }

        public static IIocBuilder UseEventBus(this IIocBuilder builder)
        {
            builder.RegisterServices(r => r.Register<IEventBus, EventBus>());
            return builder;
        }
    }
}
