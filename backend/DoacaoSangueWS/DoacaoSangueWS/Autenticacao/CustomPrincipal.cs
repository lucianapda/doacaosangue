using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace DoacaoSangueWS.Autenticacao
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(usuarios user)
        {
            Identity = new GenericIdentity(user.nome, user.privilegio);
        }

        public IIdentity Identity
        {
            get;
            private set;
        }

        public bool IsInRole(string role)
        {
            return Identity.AuthenticationType == role;
        }
    }
}