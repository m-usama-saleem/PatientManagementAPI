using Microsoft.EntityFrameworkCore;
using WebAPI.Contracts;
using WebAPI.DB.Models;
using WebAPI.ViewModels.ViewModels;

namespace WebAPI.Services
{
    public class InvoiceService : IInvoice, ICrud<InvoiceViewModel>
    {
        private readonly ILogger _logger;
        private readonly PatientManagementContext _db;
        private readonly IConfiguration _configuration;

        public InvoiceService(PatientManagementContext dBContext, IConfiguration configuration, ILogger<UserService> logger)
        {
            _logger = logger;
            _db = dBContext;
            _configuration = configuration;
        }

        public async Task<InvoiceViewModel> DeleteRecord(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<InvoiceViewModel> GetRecordById(long id)
        {
            var invoice = await _db.Invoices.Include(x => x.InvoiceDetails)
                                                .ThenInclude(x => x.Procedure)
                                            .Include(x => x.Patient)
                                            .Include(x => x.Doctor)
                                            .Include(x => x.PaymentHistories)
                                            .FirstAsync(x => x.Id == id);

            var invoiceModel = new InvoiceViewModel(invoice);
            return invoiceModel;
        }

        public async Task<List<InvoiceViewModel>> InvoiceList()
        {
            var invModel = new List<InvoiceViewModel>();
            var invoices = await _db.Invoices.OrderByDescending(x => x.CreatedDate)
                                            .Include(x => x.InvoiceDetails)
                                            .ThenInclude(x => x.Procedure)
                                            .Include(x => x.Patient)
                                            .Include(x => x.Doctor)
                                            .Include(x => x.PaymentHistories)
                                            .ToListAsync();
            invoices.ForEach(x => invModel.Add(new InvoiceViewModel(x)));
            return invModel;
        }
        public async Task<List<InvoiceViewModel>> UnPaidInvoices()
        {
            var invModel = new List<InvoiceViewModel>();
            var invoices = await _db.Invoices.Where(x => x.InvoiceStatus == "unpaid").Include(x => x.InvoiceDetails).ToListAsync();
            invoices.ForEach(x => invModel.Add(new InvoiceViewModel(x)));
            return invModel;
        }

        public async Task<List<InvoiceViewModel>> PaidInvoices()
        {
            var invModel = new List<InvoiceViewModel>();
            var invoices = await _db.Invoices.Where(x => x.InvoiceStatus == "paid").Include(x => x.InvoiceDetails).ToListAsync();
            invoices.ForEach(x => invModel.Add(new InvoiceViewModel(x)));
            return invModel;
        }

        public async Task<InvoiceViewModel> SaveRecord(InvoiceViewModel entity)
        {
            InvoiceViewModel model = null;

            var invoice = new Invoice
            {
                CreatedDate = entity.CreatedDate,
                DoctorId = entity.DoctorId,
                PatientId = entity.PatientId,
                TotalAmount = entity.TotalAmount,
                Discount = entity.Discount,
                NetPayable = entity.NetPayable,
                PaidAmount = entity.PaidAmount,
                DueAmount = entity.DueAmount,
                AdditionalNotes = entity.AdditionalNotes,
                Notes = entity.Notes,
                InvoiceStatus = entity.InvoiceStatus,
            };

            _db.Invoices.Add(invoice);
            var res = await _db.SaveChangesAsync();

            if (res > 0)
            {
                entity.Id = invoice.Id;
                _db.PaymentHistories.Add(new PaymentHistory
                {
                    InvoiceId = invoice.Id,
                    PaidAmount = entity.PaidAmount,
                    Balance = entity.DueAmount,
                    TotalPaid = entity.PaidAmount,
                    CreatedDate = entity.CreatedDate
                });
                foreach (var d in entity.InvoiceDetails)
                {
                    var invDetail = new InvoiceDetail
                    {
                        InvoiceId = invoice.Id,
                        ProcedureId = d.ProcedureId,
                        ProceduralNotes = d.ProcedureNotes,
                        Price = d.Price,
                        Qty = d.Qty,
                        IsPercent = d.IsPercent,
                        Discount = d.Discount
                    };
                    _db.InvoiceDetails.Add(invDetail);
                }

                var res2 = await _db.SaveChangesAsync();
                if (res2 > 0)
                {
                    return entity;
                }
            }
            return entity;
        }

        public async Task<InvoiceViewModel> UpdateRecord(InvoiceViewModel entity)
        {
            var inv = _db.Invoices.FirstOrDefault(x => x.Id == entity.Id);
            if (inv != null)
            {
                inv.Notes = entity.Notes;
                inv.AdditionalNotes = entity.AdditionalNotes;
                inv.PaidAmount = entity.PaidAmount;
                inv.DueAmount = entity.DueAmount;
                _db.Invoices.Update(inv);
            }
            var payment = entity.InvoiceHistory.LastOrDefault();
            var payments = _db.PaymentHistories.Add(new PaymentHistory
            {
                InvoiceId = entity.Id,
                PaidAmount = payment.PaidAmount,
                Balance = payment.Balance,
                TotalPaid = payment.TotalPaid,
                CreatedDate = payment.CreatedDate
            });
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
