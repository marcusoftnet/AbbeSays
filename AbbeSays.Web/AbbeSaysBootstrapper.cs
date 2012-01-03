using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Cryptography;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Authentication.Basic;
using Nancy.Security;
using Simple.Data;

namespace AbbeSays.Web
{
    public class AbbeSaysBootstrapper : DefaultNancyBootstrapper
	{
		protected override void ApplicationStartup(TinyIoC.TinyIoCContainer container, IPipelines pipelines)
		{
            base.ApplicationStartup(container, pipelines);

            pipelines.EnableBasicAuthentication(new BasicAuthenticationConfiguration(
                container.Resolve<IUserValidator>(),
                "MyRealm"));
		}

        public class UserValidator : IUserValidator
        {
            private dynamic db = Database.Open();

            public IUserIdentity Validate(string username, string password)
            {
                var hashedPassword = GetPasswordHash(password);

                if (db.Families.ExistsByUserNameAndPassword(username, hashedPassword))
                {
                    return new QuotesUserIdentity { UserName = username };                    
                }

                throw  new AuthenticationException(string.Format("Could not authorized '{0}'. Check your username and password", username));

            }

            private string GetPasswordHash(string password)
            {
                var x = new MD5CryptoServiceProvider();
                byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
                data = x.ComputeHash(data);
                return System.Text.Encoding.ASCII.GetString(data);
            }
        }

        public class QuotesUserIdentity : IUserIdentity
        {
            public string UserName { get; set; }

            public IEnumerable<string> Claims { get; set; }
        }
    }
}