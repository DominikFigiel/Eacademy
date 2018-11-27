using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EacademyApp.API.Data;
using EacademyApp.API.Dtos;
using EacademyApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD

namespace EacademyApp.API.Controllers
{
    //[Authorize]
=======
 
namespace EacademyApp.API.Controllers
{
    [Authorize]
>>>>>>> e351c261f616061baedf560f82083e5a5de553f6
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
<<<<<<< HEAD

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
    }
}
=======
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
    }
}
>>>>>>> e351c261f616061baedf560f82083e5a5de553f6
