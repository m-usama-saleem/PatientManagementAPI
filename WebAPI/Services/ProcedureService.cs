using Microsoft.EntityFrameworkCore;
using WebAPI.Contracts;
using WebAPI.DB.Models;
using WebAPI.ViewModels.ViewModels;

namespace WebAPI.Services
{
    public class ProcedureService : IProcedure, ICrud<ProcedureViewModel>
    {
        private readonly ILogger _logger;
        private readonly PatientManagementContext _db;
        private readonly IConfiguration _configuration;

        public ProcedureService(PatientManagementContext dBContext, IConfiguration configuration, ILogger<UserService> logger)
        {
            _logger = logger;
            _db = dBContext;
            _configuration = configuration;
        }

        public async Task<ProcedureViewModel> DeleteRecord(long id)
        {
            ProcedureViewModel pvm = null;
            var proc = await _db.ProcedureLists.FirstOrDefaultAsync(x => x.Id == id);
            if (proc != null)
            {
                proc.IsActive = false;
                _db.ProcedureLists.Update(proc);
                await _db.SaveChangesAsync();
                pvm = new ProcedureViewModel(proc);
            }
            return pvm;
        }

        public ProcedureViewModel GetRecord(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProcedureViewModel> GetRecordById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProcedureViewModel>> ProcedureList()
        {
            var list = new List<ProcedureViewModel>();
            var procedures = await _db.ProcedureLists.ToListAsync();
            procedures.ForEach(x => list.Add(new ProcedureViewModel(x)));
            return list;
        }

        public async Task<ProcedureViewModel> SaveRecord(ProcedureViewModel entity)
        {
            var proc = new ProcedureList
            {
                DefaultUnit = entity.DefaultUnit,
                Description = entity.Description,
                Name = entity.Name,
                Price = entity.Price,
                Tax = entity.Tax,
                IsActive = true,
            };
            await _db.ProcedureLists.AddAsync(proc);
            await _db.SaveChangesAsync();
            entity.Id = proc.Id;
            return entity;
        }

        public async Task<ProcedureViewModel> UpdateRecord(ProcedureViewModel entity)
        {
            var proc = await _db.ProcedureLists.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (proc != null)
            {
                proc.DefaultUnit = entity.DefaultUnit;
                proc.Description = entity.Description;
                proc.Name = entity.Name;
                proc.Price = entity.Price;
                proc.Tax = entity.Tax;
                proc.IsActive = true;

                _db.ProcedureLists.Update(proc);
                await _db.SaveChangesAsync();
            }
            return entity;
        }
    }
}
