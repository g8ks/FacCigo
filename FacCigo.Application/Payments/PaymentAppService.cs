using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace FacCigo.Payments
{
    public class PaymentAppService:CrudAppService<Payment, PaymentDto, Guid,PaymentGetListInput>
    {
        private readonly PaymentManager paymentManager;
        public PaymentAppService(IPaymentRepository repo,PaymentManager manager) : base(repo)
        {
            this.paymentManager = manager;
        }
        public override async Task<PaymentDto> CreateAsync(PaymentDto input)
        {
            Payment payment = await paymentManager.CreateAsync(input.InvoiceId, input.AmountPaid,
                                          input.PaidCurrencyId, input.ExchangeRateApplied, input.PaymentDate);
            PaymentDto paymentDto = MapToGetOutputDto(payment);
            return paymentDto;
        }
        protected override IQueryable<Payment> CreateFilteredQuery(PaymentGetListInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.InvoiceId != null, t => t.InvoiceId == input.InvoiceId)
                .WhereIf(!string.IsNullOrEmpty(input.InvoiceRef), t => t.Invoice.ReferenceNo == input.InvoiceRef)
                .WhereIf(!string.IsNullOrEmpty(input.Currency), t => t.PaidCurrencyId == input.Currency)
                .WhereIf(input.Date.HasValue, t => t.PaymentDate.Date.Equals(input.Date.Value.Date));
        }
    }
}
