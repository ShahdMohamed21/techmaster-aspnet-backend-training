using Microsoft.AspNetCore.Mvc;
using TrainngCenter.Api.DTOs.Tracks;
using TrainngCenter.Api.Services;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Controllers
{
    [ApiController]
    [Route("api/tracks")]
    public class TrainingTracksController : Controller
    {
     
        private readonly ITrackService _service;

        public TrainingTracksController(ITrackService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll( string? keyword,string? level, string? status,int? instructorId)
        {
            var result = await _service.GetAllAsync(keyword,level, status,instructorId);
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound(new
                {
                    success = false,
                    message = "Training track not found"
                });

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateTrackRequest request)
        {
            var result = await _service.CreateAsync(request);

            if (!result.success)
                return BadRequest(new
                {
                    success = false,
                    message = result.message
                });

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Data!.TrainingTrackId },
                new
                {
                    success = true,
                    message = result.message,
                    data = result.Data
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update( int id,  UpdateTrackRequest request)
        {
            var result = await _service.UpdateAsync(id, request);

            if (!result.success)
            {
                if (result.message == "Training track not found")
                    return NotFound(new
                    {
                        success = false,
                        message = result.message
                    });

                return BadRequest(new
                {
                    success = false,
                    message = result.message
                });
            }

            return Ok(new
            {
                success = true,
                message = result.message
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.success)
            {
                if (result.message == "Training track not found")
                    return NotFound(new
                    {
                        success = false,
                        message = result.message
                    });

                return BadRequest(new
                {
                    success = false,
                    message = result.message
                });
            }

            return Ok(new
            {
                success = true,
                message = result.message
            });
        }
        [HttpGet("{id}/students")]
        public async Task<IActionResult> GetStudents(int id)
        {
            var result = await _service.GetStudentsAsync(id);

            if (result == null)
                return NotFound(new
                {
                    success = false,
                    message = "Training track not found"
                });

            return Ok(new
            {
                success = true,
                data = result
            });
        }
       
    }
}

