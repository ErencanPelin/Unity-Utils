// -----------------------------------------------------
// Copyright (c) 2025 Erencan Pelin. All Rights Reserved.
// 
// Author: Erencan Pelin
// Date: 20/09/2025
// -----------------------------------------------------

using System;
using System.Collections.Generic;

namespace UnityUtils.Core
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        private readonly Dictionary<Type, object> _services = new();

        public static ServiceLocator Instance => _instance ??= new ServiceLocator();

        /// <summary>
        /// Registers a service
        /// </summary>
        /// <param name="service"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="Exception">Throws if that service is already registered</exception>
        public void Register<T>(T service) where T : class
        {
            var type = typeof(T);
            if (_services.ContainsKey(type)) throw new Exception($"Service {type} is already registered!");
            _services.Add(type, service);
        }

        /// <summary>
        /// Unregisters an existing service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="Exception">Throws if that service is not already registered</exception>
        public void Unregister<T>() where T : class
        {
            var type = typeof(T);
            if (!_services.ContainsKey(type)) throw new NullReferenceException($"Service {type} is not registered");
            _services.Remove(type);
        }

        /// <summary>
        /// Returns the instance of the service based on the given service type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T Resolve<T>() where T : class
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var service)) return (T)service;
            throw new NullReferenceException($"Service {type} not found!");
        }

        /// <summary>
        /// Returns true if that service is registered already
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsRegistered<T>() where T : class
        {
            return _services.ContainsKey(typeof(T));
        }
    }
}