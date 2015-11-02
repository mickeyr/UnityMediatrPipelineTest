using System;
using System.Net;
using System.Reflection;

namespace UnityMediatrPipelineTest
{
	public class AuthorizationHandler : IPreRequestHandler
	{
		public void Handle<TRequest>( TRequest request )
		{
			Console.WriteLine( "Authorizing user for the echo request" );
			var authorizationAttribute = typeof( TRequest ).GetCustomAttribute( typeof( AuthorizeAttribute ) ) as AuthorizeAttribute;
			if (authorizationAttribute == null) return;

			if ( authorizationAttribute.Roles == "Admin" )
			{
				Console.WriteLine( "Not Authorized!" );
			}
		}
	}

	public class AuthorizeAttribute : Attribute
	{
		public string Roles { get; set; }

		public AuthorizeAttribute( string roles )
		{
			Roles = roles;
		}
	}
}