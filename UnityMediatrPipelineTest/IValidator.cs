using System.Collections.Generic;

namespace UnityMediatrPipelineTest
{
	public interface IValidator<in T>
	{
		bool IsValid( T request );
		IList<string> ValidationErrors { get; }
	}
}