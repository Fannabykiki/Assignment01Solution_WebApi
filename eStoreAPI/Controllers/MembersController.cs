using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using BusinessObject.MemberService;
using Common.DTO.Member;

namespace eStoreAPI.Controllers
{
    [Route("api/member-management")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            var result = await _memberService.GetAllMemberAsync();

            if (result == null) return StatusCode(500);

            return Ok(result);
        }

        [HttpGet("members/{id}")]
        public async Task<ActionResult<Member>> GetMemberById(int id)
        {
            var result = await _memberService.GetMemberByIdAsync(id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPut("members/{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] UpdateMemberRequest updateMemberRequest)
        {
            var result = await _memberService.UpdateAsync(id, updateMemberRequest);

            if (result == null) return StatusCode(500);

            return Ok(result);
        }

        [HttpPost("members")]
        public async Task<ActionResult<Member>> CreateMember(AddMemberRequest addMemberRequest)
        {
            var result = await _memberService.CreateAsync(addMemberRequest);

            if (result == null) return StatusCode(500);

            return Ok(result);
        }

        [HttpDelete("members/{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var result = await _memberService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
