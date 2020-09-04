using Volo.Abp.Settings;

namespace FacCigo.Settings
{
    public class FacCigoSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from FacCigoSettings class.
             */
            context.Add(
                new SettingDefinition(FacCigoSettings.PivotCurrency,"USD")
                );
            context.Add(
                new SettingDefinition(FacCigoSettings.InvoiceCurrency, "FC")
                );
        }
    }
}