namespace FixsyWebApi.Data.Identity
{
public static class IdentityFactory{
         private static IIdentityFactory _identityFactory;
       
        public static void SetCurrent(IIdentityFactory identityFactory)
        {
            _identityFactory = identityFactory;
        }
       
        public static IIdentityGenerator CreateIdentity()
        {
            return (_identityFactory != null) ? _identityFactory.Create() : null;
        }
}
}