using MediatR;
using MediatRApplication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Xml.Linq;

namespace MediatRWebAPI
{
    public static class MediatorWebAPIExtensions
    {
        public static IEndpointRouteBuilder MapMediatorWebAPIs(this IEndpointRouteBuilder app, params Assembly[] assemblies)
        {
            Type genericRequestType = typeof(IRequest<>);
            var sendMethodInfo = typeof(MediatorWebAPIExtensions).GetMethod("MapMediatorSendApi", BindingFlags.NonPublic | BindingFlags.Static);
            foreach (var assembly in assemblies)
            {
                var requestTypes = assembly.GetTypes().Where(type => !type.IsInterface && type.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == genericRequestType));
                foreach (var requestType in requestTypes)
                {
                    var responseType = requestType.GetInterfaces().First(t => t.IsGenericType && t.GetGenericTypeDefinition() == genericRequestType).GetGenericArguments().First();
                    var genericMethod = sendMethodInfo.MakeGenericMethod(requestType, responseType);
                    genericMethod.Invoke(null, new object[] { app, requestType.Name });
                }

            }

            Type genericNotificationType = typeof(INotification);
            var publishMethodInfo = typeof(MediatorWebAPIExtensions).GetMethod("MapMediatorPublishApi", BindingFlags.NonPublic | BindingFlags.Static);
            foreach (var assembly in assemblies)
            {
                var requestTypes = assembly.GetTypes().Where(type => !type.IsInterface && genericNotificationType.IsAssignableFrom(type));
                foreach (var requestType in requestTypes)
                {
                    var genericMethod = publishMethodInfo.MakeGenericMethod(requestType);
                    genericMethod.Invoke(null, new object[] { app, requestType.Name });
                }

            }

            return app;
        }



        internal static void MapMediatorSendApi<TRequest, TResponse>(IEndpointRouteBuilder app, string requestTypeName) where TRequest : IRequest<TResponse>
        {
            if (requestTypeName.StartsWith("Create"))
            {
                var uri = new Uri(requestTypeName.Replace("Create", ""), UriKind.Relative);
                app.MapPost(uri.ToString(), async ([FromServices] IMediator mediator, [FromBody] TRequest request) =>
                {
                    TResponse response = await mediator.Send(request);
                    return Results.Created(uri, response);
                }).WithName(requestTypeName).WithOpenApi();
            }
            else if (requestTypeName.StartsWith("Read"))
            {
                var uri = new Uri(requestTypeName.Replace("Read", ""), UriKind.Relative);
                app.MapGet(uri.ToString(), async ([FromServices] IMediator mediator, [FromBody] TRequest request) =>
                {
                    TResponse response = await mediator.Send(request);
                    return Results.Ok(response);
                }).WithName(requestTypeName).WithOpenApi();
            }
            else if (requestTypeName.StartsWith("Update"))
            {
                var uri = new Uri(requestTypeName.Replace("Update", ""), UriKind.Relative);
                app.MapPut(uri.ToString(), async ([FromServices] IMediator mediator, [FromBody] TRequest request) =>
                {
                    TResponse response = await mediator.Send(request);
                    return Results.Ok(response);
                }).WithName(requestTypeName).WithOpenApi();
            }
            else if (requestTypeName.StartsWith("Delete"))
            {
                var uri = new Uri(requestTypeName.Replace("Delete", ""), UriKind.Relative);
                app.MapDelete(uri.ToString(), async ([FromServices] IMediator mediator, [FromBody] TRequest request) =>
                {
                    TResponse response = await mediator.Send(request);
                    return Results.NoContent();
                }).WithName(requestTypeName).WithOpenApi();
            }
            else
            {
                app.MapPost("/mediatr/send/" + requestTypeName, async ([FromServices] IMediator mediator, [FromBody] TRequest request) =>
                {
                    TResponse response = await mediator.Send(request);
                    return Results.Ok(response);
                }).WithName(requestTypeName).WithOpenApi();
            }
        }

        internal static void MapMediatorPublishApi<TNotification>(IEndpointRouteBuilder app, string requestTypeName) where TNotification : INotification
        {
            app.MapPost("/mediatr/publish/" + requestTypeName, async ([FromServices] IMediator mediator, [FromBody] TNotification notification) =>
            {
                await mediator.Publish(notification);
                return Results.Ok();
            }).WithName(requestTypeName).WithOpenApi();
        }
    }
}
