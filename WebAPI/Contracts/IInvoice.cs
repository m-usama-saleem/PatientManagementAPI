using WebAPI.ViewModels.ViewModels;

namespace WebAPI.Contracts
{
    public interface IInvoice
    {
        Task<List<InvoiceViewModel>> InvoiceList();
        Task<List<InvoiceViewModel>> PaidInvoices();
        Task<List<InvoiceViewModel>> UnPaidInvoices();
    }
}
