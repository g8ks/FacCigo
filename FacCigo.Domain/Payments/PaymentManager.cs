using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Settings;
using FacCigo.Settings;
using Volo.Abp.Uow;

namespace FacCigo
{
    public class PaymentManager:DomainService
    {
        private readonly IInvoiceRepository InvoiceRepository;
        private readonly IExchangeRateRepository ExchangeRateRepository;
        private readonly IPaymentRepository PayementRepository;
        private readonly ISettingProvider SettingProvider;

        public PaymentManager (IInvoiceRepository invoices, IExchangeRateRepository exchangeRates, IPaymentRepository payments, ISettingProvider settingProvider)
        {
            this.PayementRepository = payments;
            this.InvoiceRepository = invoices;
            this.ExchangeRateRepository = exchangeRates;
            SettingProvider = settingProvider;
        }
        [UnitOfWork]
        public async Task<Payment> CreateAsync(Guid invoiceId, decimal amountPaid,string paidCurrencyId,Guid exchangeRateId,DateTime payementDate)
        {
            Invoice invoice = await InvoiceRepository.GetAsync(invoiceId);
            if (invoice == null) throw new InvoiceNotExisting(invoiceId);
            string pivotCurrency = await SettingProvider.GetOrNullAsync(FacCigoSettings.PivotCurrency);
            if (pivotCurrency != paidCurrencyId)
            {
                var exchange = await ExchangeRateRepository.GetAsync(exchangeRateId);
                if (exchange == null) throw new ExamNotExisting(exchangeRateId);
                decimal converted= Math.Round(invoice.TotalAmount * exchange.Rate,2);
                if (converted != Math.Round(amountPaid, 2)) throw new AmountNotMatchingException();
            }
            Payment result = await PayementRepository.InsertAsync(
                new Payment
            (
                 GuidGenerator.Create(),
                 invoiceId,
                 amountPaid,
                 paidCurrencyId,
                 exchangeRateId,
                 payementDate
               
            )) ;
            invoice.ChangeStatus(InvoiceStatus.PAID);
            await InvoiceRepository.UpdateAsync(invoice,autoSave:true);
            return result;
        }
    }
}
