using ApiProject.Dtos;

namespace ApiProject.Core.Interface
{
    public interface ICriminalSrvice
    {
        Task AddCriminalAsync(CriminalDto criminal);
    }
}
