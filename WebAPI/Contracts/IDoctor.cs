using WebAPI.ViewModels.ViewModels;

namespace WebAPI.Contracts
{
    public interface IDoctor
    {
        bool IsDoctorExists(int id);
        Task<List<DoctorViewModel>> DoctorList();
        Task<List<DoctorViewModel>> ActiveDoctorList();
    }
}
