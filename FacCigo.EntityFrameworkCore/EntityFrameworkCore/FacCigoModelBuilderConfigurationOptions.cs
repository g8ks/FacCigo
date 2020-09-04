using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace FacCigo.EntityFrameworkCore
{
    public class FacCigoModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public FacCigoModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}