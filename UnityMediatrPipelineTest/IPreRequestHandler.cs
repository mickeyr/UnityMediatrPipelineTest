namespace UnityMediatrPipelineTest
{
	public interface IPreRequestHandler
	{
		void Handle<TRequest>( TRequest request );
	}
}