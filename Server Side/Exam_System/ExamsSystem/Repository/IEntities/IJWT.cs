using System.Security.Claims;

namespace ExamsSystem.Repository.IEntities
{
    public interface IJWT
    {
        public string GenentateToken(ICollection<Claim> claims);
    }
}
