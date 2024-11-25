namespace FixsyWebApi.Data.Identity
{
public interface IIdentityFactory{
    IIdentityGenerator Create();
}
}