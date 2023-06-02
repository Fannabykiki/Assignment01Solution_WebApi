using Common.DTO.Member;
using Common.DTO.Product;
using DataAccess.Models;
using DataAccess.Repositories.Implements;
using DataAccess.Repositories.Interfaces;

namespace BusinessObject.MemberService
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public async Task<AddMemberResponse> CreateAsync(AddMemberRequest addMemberRequest)
        {
            try
            {
                var member = new Member
                {
                    City = addMemberRequest.City,
                    CompanyName = addMemberRequest.CompanyName,
                    Country = addMemberRequest.Country,
                    Email = addMemberRequest.Email,
                    Password = addMemberRequest.Password,
                };
                await _memberRepository.CreateAsync(member);
                _memberRepository.SaveChanges();

                return new AddMemberResponse
                {
                    IsSucced = true
                };
            }
            catch (Exception)
            {
                return new AddMemberResponse
                {
                    IsSucced = false,
                };
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var product = await _memberRepository.GetAsync(s => s.MemberId == id);
                if (product == null)
                {
                    return false;
                }
                _memberRepository.DeleteAsync(product);
                _memberRepository.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Member>> GetAllMemberAsync()
        {
            return await _memberRepository.GetAllWithOdata(x => true,x => x.Orders);
        }

        public async Task<MemberViewModel?> GetMemberByIdAsync(int id)
        {
            try
            {
                var result = await _memberRepository.GetAsync(c => c.MemberId == id);

                if (result == null)
                {
                    return null;
                }

                return new MemberViewModel
                {
                    MemberId = result.MemberId,
                    Password = result.Password,
                    Email = result.Email,
                    Country = result.Country,
                    CompanyName = result.CompanyName,
                    City = result.City
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<AddMemberResponse> UpdateAsync(int id, UpdateMemberRequest updateMemberRequest)
        {
            try
            {
                var member = await _memberRepository.GetAsync(s => s.MemberId == id);
                if (member == null)
                {
                    return new AddMemberResponse
                    {
                        IsSucced = false,
                    };
                }
                member.City = updateMemberRequest.City;
                member.Country = updateMemberRequest.Country;
                member.Email = updateMemberRequest.Email;
                member.CompanyName = updateMemberRequest.CompanyName;
                member.Password = updateMemberRequest.Password;

                await _memberRepository.UpdateAsync(member);
                _memberRepository.SaveChanges();

                return new AddMemberResponse
                {
                    IsSucced = true,
                };
            }
            catch (Exception)
            {
                return new AddMemberResponse
                {
                    IsSucced = false,
                };
            }
        }
    }
}
