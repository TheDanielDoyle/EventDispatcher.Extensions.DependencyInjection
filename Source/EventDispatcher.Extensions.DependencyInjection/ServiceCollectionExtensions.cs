using System;
using EventDispatcher;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventDispatcher(this IServiceCollection services)
        {
            return services.AddScoped<IEventDispatcher, DefaultEventDispatcher>();
        }

        public static IServiceCollection AddEventDispatcher(this IServiceCollection services, Type eventDispatcherType)
        {
            if (!typeof(IEventDispatcher).IsAssignableFrom(eventDispatcherType))
            {
                throw new ArgumentException($"{eventDispatcherType.FullName} is not assignable to {nameof(IEventDispatcher)}");
            }
            return services.AddScoped<IEventDispatcher, DefaultEventDispatcher>();
        }

        public static IServiceCollection AddEventDispatcher<TEventDispatcher>(this IServiceCollection services)
        {
            return services.AddEventDispatcher(typeof(TEventDispatcher));
        }

        public static IServiceCollection AddEventHandler<TEvent, THandler>(this IServiceCollection services)
            where THandler : class, IEventDispatchHandler<TEvent>
            where TEvent : IEvent
        {
            services.AddTransient<IEventDispatchHandler<TEvent>, THandler>();
            return services;
        }
    }
}
