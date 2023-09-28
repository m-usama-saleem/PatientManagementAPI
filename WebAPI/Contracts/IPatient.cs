using WebAPI.DB.Models;
using WebAPI.ViewModels.ViewModels;

namespace WebAPI.Contracts
{
    public interface IPatient
    {
        bool IsPatientExists(int id);
        Task<List<PatientViewModel>> PatientList();
        Task<List<PatientViewModel>> ActivePatientList();

    }
}
