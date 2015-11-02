using MediatR;

namespace UnityMediatrPipelineTest
{
	public class MediatorPipeline<TRequest, TResponse>
	: IRequestHandler<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
	{

		private readonly IRequestHandler<TRequest, TResponse> _inner;
		private readonly IPreRequestHandler[] _preRequestHandlers;
		private readonly IPostRequestHandler[] _postRequestHandlers;

		public MediatorPipeline(
			IRequestHandler<TRequest, TResponse> inner,
			IPreRequestHandler[] preRequestHandlers,
			IPostRequestHandler[] postRequestHandlers
			)
		{
			_inner = inner;
			_preRequestHandlers = preRequestHandlers;
			_postRequestHandlers = postRequestHandlers;
		}

		public TResponse Handle( TRequest message )
		{

			foreach ( var preRequestHandler in _preRequestHandlers )
			{
				preRequestHandler.Handle( message );
			}

			var result = _inner.Handle( message );

			foreach ( var postRequestHandler in _postRequestHandlers )
			{
				postRequestHandler.Handle( message, result );
			}

			return result;
		}
	}
}
