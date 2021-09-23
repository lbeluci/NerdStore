﻿using NS.Core.Messages.Integration;
using System;
using System.Threading.Tasks;

namespace NS.MessageBus
{
    public interface IMessageBus : IDisposable
    {
        bool IsConnected { get; }

        void Publish<T>(T message) where T : IntegrationEvent;

        Task PublishAsync<T>(T message) where T : IntegrationEvent;

        void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class;

        Task SubscribeAsync<T>(string subscriptionId, Action<T> onMessage) where T : class;

        TResponse Request<TRequest, TResponse>(TRequest request) where TRequest : IntegrationEvent where TResponse : ResponseMessage;

        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request) where TRequest : IntegrationEvent where TResponse : ResponseMessage;

        IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> func) where TRequest : IntegrationEvent where TResponse : ResponseMessage;

        Task<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> func) where TRequest : IntegrationEvent where TResponse : ResponseMessage;
    }
}