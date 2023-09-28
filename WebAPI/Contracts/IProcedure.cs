using WebAPI.ViewModels.ViewModels;

namespace WebAPI.Contracts
{
    public interface IProcedure
    {
        Task<List<ProcedureViewModel>> ProcedureList();
    }
}
