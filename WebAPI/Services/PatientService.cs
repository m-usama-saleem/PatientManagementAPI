using Microsoft.EntityFrameworkCore;
using WebAPI.Contracts;
using WebAPI.DB.Models;
using WebAPI.ViewModels.ViewModels;

namespace WebAPI.Services
{
    public class PatientService : IPatient, ICrud<PatientViewModel>
    {
        private readonly ILogger _logger;
        private readonly PatientManagementContext _db;
        private readonly IConfiguration _configuration;

        public PatientService(PatientManagementContext dBContext, IConfiguration configuration, ILogger<UserService> logger)
        {
            _logger = logger;
            _db = dBContext;
            _configuration = configuration;
        }
        public bool IsPatientExists(int id)
        {
            return _db.Patients.Any(x => x.Id == id);
        }
        public async Task<List<PatientViewModel>> ActivePatientList()
        {
            var list = new List<PatientViewModel>();
            var patients = await _db.Patients.Where(x => x.IsActive == true).ToListAsync();
            patients.ForEach(x => list.Add(new PatientViewModel(x)));
            return list;
        }
        public async Task<List<PatientViewModel>> PatientList()
        {
            var list = new List<PatientViewModel>();
            var patients = await _db.Patients.ToListAsync();
            patients.ForEach(x => list.Add(new PatientViewModel(x)));
            return list;
        }

        public async Task<PatientViewModel> SaveRecord(PatientViewModel entity)
        {
            var patient = new Patient
            {
                FirstName = entity.Firstname,
                LastName = entity.Lastname,
                Address = entity.Address,
                City = entity.City,
                Country = entity.Country,
                Contact = entity.Contact,
                Dob = entity.Dob,
                Email = entity.Email,
                IsActive = true,
            };
            await _db.Patients.AddAsync(patient);
            await _db.SaveChangesAsync();
            entity.Id = patient.Id;
            return entity;
        }

        public async Task<PatientViewModel> GetRecordById(long id)
        {
            var rec = await _db.Patients.FirstAsync(x => x.Id == id);
            return new PatientViewModel(rec);
        }

        public async Task<PatientViewModel> UpdateRecord(PatientViewModel entity)
        {
            var patient = await _db.Patients.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (patient != null)
            {
                patient.FirstName = entity.Firstname;
                patient.LastName = entity.Lastname;
                patient.Address = entity.Address;
                patient.City = entity.City;
                patient.Country = entity.Country;
                patient.Contact = entity.Contact;
                patient.Dob = entity.Dob;
                patient.Email = entity.Email;

                _db.Patients.Update(patient);
                _db.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<PatientViewModel> DeleteRecord(long id)
        {
            PatientViewModel pvm = null;
            var patient = await _db.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (patient != null)
            {
                patient.IsActive = false;
                _db.Patients.Update(patient);
                await _db.SaveChangesAsync();
                pvm = new PatientViewModel(patient);
            }
            return pvm;
        }
    }
}
