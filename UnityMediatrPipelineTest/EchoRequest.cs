using System;
using System.Collections.Generic;
using MediatR;

namespace UnityMediatrPipelineTest
{
	[Authorize("User")]
	public class EchoRequest : IRequest<string>
	{
		public string Input { get; set; }
	}

	public class EchoRequestValidator : IValidator<EchoRequest>
	{
		public EchoRequestValidator()
		{
			ValidationErrors = new List<string>();
		}
		public bool IsValid( EchoRequest request )
		{
			ValidationErrors.Add( "It's invalid" );
			return false;
		}

		public IList<string> ValidationErrors { get; private set; }
	}

	public class LogEchoRequestWhatever : IPostRequestHandler
	{
		public void Handle<TRequest, TResponse>( TRequest request, TResponse response )
		{
			Console.WriteLine( $"Echo request returned {response}" );
		}
	}

	public class EchoHandler : IRequestHandler<EchoRequest, String>
	{
		public string Handle( EchoRequest message )
		{
			Console.WriteLine( message.Input );
			return $"{message.Input} was echoed to the console";
		}
	}
}