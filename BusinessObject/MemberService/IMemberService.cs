using Common.DTO.Member;
using Common.DTO.Product;
using DataAccess.Models;

namespace BusinessObject.MemberService
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllMemberAsync();
        Task<AddMemberResponse> CreateAsync(AddMemberRequest addMemberRequest);
        Task<AddMemberResponse> UpdateAsync(int id, UpdateMemberRequest updateMemberRequest);
        Task<bool> DeleteAsync(int id);
        Task<MemberViewModel> GetMemberByIdAsync(int id);
    }
}
