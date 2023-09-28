using Microsoft.EntityFrameworkCore;
using WebAPI.Contracts;
using WebAPI.DB.Models;
using WebAPI.ViewModels.ViewModels;

namespace WebAPI.Services
{
    public class DoctorService : IDoctor, ICrud<DoctorViewModel>
    {
        private readonly ILogger _logger;
        private readonly PatientManagementContext _db;
        private readonly IConfiguration _configuration;

        public DoctorService(PatientManagementContext dBContext, IConfiguration configuration, ILogger<UserService> logger)
        {
            _logger = logger;
            _db = dBContext;
            _configuration = configuration;
        }

        public async Task<List<DoctorViewModel>> ActiveDoctorList()
        {
            var list = new List<DoctorViewModel>();
            var docs = await _db.Doctors.Where(x => x.IsActive == true).ToListAsync();
            docs.ForEach(x => list.Add(new DoctorViewModel(x)));
            return list;
        }
        public bool IsDoctorExists(int id)
        {
            return _db.Doctors.Any(x => x.Id == id);
        }
        public async Task<List<DoctorViewModel>> DoctorList()
        {
            var list = new List<DoctorViewModel>();
            var docs = await _db.Doctors.ToListAsync();
            docs.ForEach(x => list.Add(new DoctorViewModel(x)));
            return list;
        }
        public async Task<DoctorViewModel> SaveRecord(DoctorViewModel entity)
        {
            var doc = new Doctor
            {
                FirstName = entity.Firstname,
                LastName = entity.Lastname,
                Address = entity.Address,
                City = entity.City,
                Country = entity.Country,
                Contact = entity.Contact,
                Designation = entity.Designation,
                Email = entity.Email,
                IsActive = true,
            };
            await _db.Doctors.AddAsync(doc);
            await _db.SaveChangesAsync();
            entity.Id = doc.Id;
            return entity;
        }
        public async Task<DoctorViewModel> GetRecordById(long id)
        {
            var rec = await _db.Doctors.FirstAsync(x => x.Id == id);
            return new DoctorViewModel(rec);
        }
        public async Task<DoctorViewModel> UpdateRecord(DoctorViewModel entity)
        {
            var doc = await _db.Doctors.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (doc != null)
            {
                doc.FirstName = entity.Firstname;
                doc.LastName = entity.Lastname;
                doc.Address = entity.Address;
                doc.City = entity.City;
                doc.Country = entity.Country;
                doc.Contact = entity.Contact;
                doc.Designation = entity.Designation;
                doc.Email = entity.Email;

                _db.Doctors.Update(doc);
                await _db.SaveChangesAsync();
            }
            return entity;
        }
        public async Task<DoctorViewModel> DeleteRecord(long id)
        {
            DoctorViewModel dvm = null;
            var doc = await _db.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (doc != null)
            {
                doc.IsActive = false;
                _db.Doctors.Update(doc);
                await _db.SaveChangesAsync();
                dvm = new DoctorViewModel(doc);
            }
            return dvm;
        }
    }
}
