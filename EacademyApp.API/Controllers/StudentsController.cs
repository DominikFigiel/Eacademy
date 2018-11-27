using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EacademyApp.API.Data;
using EacademyApp.API.Dtos;
using EacademyApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EacademyApp.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IEacademyRepository _repo;
        private readonly IMapper _mapper;
        public StudentsController(IEacademyRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _repo.GetStudents();

            var studentsToReturn = _mapper.Map<IEnumerable<StudentForListDto>>(students);

            return Ok(studentsToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _repo.GetStudent(id);

            var studentToReturn = _mapper.Map<StudentForDetailedDto>(student);

            return Ok(student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentForUpdateDto studentForUpdateDto)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var studentFromRepo = await _repo.GetStudent(id);

            _mapper.Map(studentForUpdateDto, studentFromRepo);

            if(await _repo.SaveAll())
                return NoContent();

            throw new System.Exception($"Updating student {id} failed on save");
        }
    }
}