using FacCigo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace FacCigo.Permissions
{
    public class FacCigoPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(FacCigoPermissions.GroupName, L("Permission:FacCigo"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<FacCigoResource>(name);
        }
    }
}