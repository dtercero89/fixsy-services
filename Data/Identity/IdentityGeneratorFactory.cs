namespace FixsyWebApi.Data.Identity
{

    public class IdentityGeneratorFactory : IIdentityFactory
    {
        public IIdentityGenerator Create()
        {
            return new IdentityGenerator();
        }
    }
}