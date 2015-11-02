using System;
using System.Linq;
using MediatR;
using Microsoft.Practices.Unity;

namespace UnityMediatrPipelineTest
{
	class Program
	{
		static void Main( string[] args )
		{
			var container = CreateUnityContainer();
			var message = new EchoRequest
			{
				Input = "Echo This"
			};

			var mediator = (IMediator)container.Resolve( typeof( IMediator ) );
			var result = mediator.Send( message );
			Console.WriteLine( result );

			return;
		}

		public static IUnityContainer CreateUnityContainer()
		{
			var container = new UnityContainer();
			container.RegisterType<IMediator, Mediator>();
			var types = AllClasses.FromAssemblies( typeof( Program ).Assembly );

			container.RegisterTypes(
				types,
				WithMappings.FromAllInterfaces,
				GetName,
				GetLifetimeManager,
				null,
				true );

			container.RegisterInstance<SingleInstanceFactory>( t =>
			 {
				 var pipeline = container.Resolve(
					 typeof( MediatorPipeline<,> ).MakeGenericType( t.GetGenericArguments() )
				 );

				 return pipeline;

			 } );
			container.RegisterInstance<MultiInstanceFactory>( t => container.ResolveAll( t ) );

			return container;
		}

		static bool IsNotificationHandler( Type type )
		{
			return type.GetInterfaces().Any( x => x.IsGenericType && ( x.GetGenericTypeDefinition() == typeof( INotificationHandler<> ) || x.GetGenericTypeDefinition() == typeof( IAsyncNotificationHandler<> ) ) );
		}

		static LifetimeManager GetLifetimeManager( Type type )
		{
			return IsNotificationHandler( type ) ? new ContainerControlledLifetimeManager() : null;
		}

		static bool IsPreOrPostHandler( Type type )
		{
			return type.GetInterfaces()
				.Any(
					x => typeof( IPreRequestHandler ).IsAssignableFrom( x ) || typeof( IPostRequestHandler ).IsAssignableFrom( x ) );
		}

		static string GetName( Type type )
		{
			return IsPreOrPostHandler( type ) ? string.Format( "HandlerFor" + type.Name ) : string.Empty;
		}
	}


}
