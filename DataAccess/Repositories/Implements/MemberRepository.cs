using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implements
{
    public class MemberRepository : BaseRepository<Member>, IMemberRepository
    {
        public MemberRepository(PRN231_AS1Context context) : base(context)
        {
        }
    }
}
