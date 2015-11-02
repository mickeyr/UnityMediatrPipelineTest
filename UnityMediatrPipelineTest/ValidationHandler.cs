using System;
using Microsoft.Practices.Unity;

namespace UnityMediatrPipelineTest
{
	public class ValidationHandler : IPreRequestHandler
	{
		private readonly IUnityContainer _container;

		public ValidationHandler( IUnityContainer container )
		{
			_container = container;
		}

		public void Handle<TRequest>( TRequest request )
		{
			Console.WriteLine( "Validating Echo Request" );
			var validator = (IValidator<TRequest>)_container.Resolve( typeof( IValidator<TRequest> ) );
			if ( !validator.IsValid( request ) )
			{
				Console.WriteLine( $"Invalid Request: {string.Join( "\n", validator.ValidationErrors )}" );
			}
		}
	}
}