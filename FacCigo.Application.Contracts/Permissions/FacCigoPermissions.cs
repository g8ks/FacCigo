using Volo.Abp.Reflection;

namespace FacCigo.Permissions
{
    public class FacCigoPermissions
    {
        public const string GroupName = "FacCigo";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(FacCigoPermissions));
        }
    }
}