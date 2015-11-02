namespace UnityMediatrPipelineTest
{
	public interface IPostRequestHandler
	{
		void Handle<TRequest, TResponse>( TRequest request, TResponse response );
	}
}