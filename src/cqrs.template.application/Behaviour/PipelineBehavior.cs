using System;
using MediatR;
using System.Linq;
using System.Threading;
using FluentValidation;
using System.Threading.Tasks;
using System.Collections.Generic;
using cqrs.template.domain.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace cqrs.template.application.Behaviour
{
    public class PipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IMemoryCache _cache;
        private readonly IEnumerable<IValidator> _validators;
        private readonly IMediator _bus;

        public PipelineBehavior(IMemoryCache cache, IEnumerable<IValidator<TRequest>> validators, IMediator bus)
        {
            _cache = cache;
            _validators = validators;
            _bus = bus;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!ValidateRequest(request))
            {
                return default;
            }

            if (request is IProvideCacheKey cacheableRequest)
            {
                var cacheKey = cacheableRequest.CacheKey;

                if (_cache.TryGetValue<TResponse>(cacheKey, out var cacheResponse))
                {
                    return cacheResponse;
                }

                var response = await next();
                _cache.Set(cacheKey, response, TimeSpan.FromMinutes(1));

                return response;
            }
            else
            {
                return await next();
            }
        }

        private bool ValidateRequest(TRequest request)
        {
            var failures = _validators
                .Select(v => v.Validate(request as IValidationContext))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();
            
            if (failures.Any())
            {
                foreach(var error in failures)
                {
                    _bus.Publish(new ExceptionNotification(error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }

                return false;
            }

            return true;
        }
    }
}