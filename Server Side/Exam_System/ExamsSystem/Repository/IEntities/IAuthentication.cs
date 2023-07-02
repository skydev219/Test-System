using ExamsSystem.DTO;

namespace ExamsSystem.Repository.IEntities
{
    public interface IAuthentication<t>
    {
        public Task<t> Login(LoginDTO loginDTO);

    }
}
