using GigaApp.Domain.Identity;

namespace GigaApp.Domain.Authentication
{
    internal class IdentityProvider : IIdentityProvider
    {
        public IIdentity Current { get; set; }
    }
}
