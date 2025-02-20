using Agazaty.Models;
using Agazaty.Models.DTO;
using Agazaty.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agazaty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermitLeaveController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IMapper _mapper { get; }

        public PermitLeaveController(IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("{leaveID:int}",Name = "GetPermitLeave")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PermitLeaveDTO> GetPermitLeave(int leaveID)
        {
            var permitLeave = _unitOfWork.PermitLeave.Get(c => c.Id == leaveID);
            if(permitLeave==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PermitLeaveDTO>(permitLeave));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PermitLeaveDTO>> GetAllPermitLeave()
        {
            var permitLeaves = _unitOfWork.PermitLeave.GetAll().ToList();
            return Ok(_mapper.Map<IEnumerable<PermitLeaveDTO>>(permitLeaves));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PermitLeave> CreatePermitLeave([FromForm] CreatePermitLeaveDTO model,[FromForm] List<IFormFile>? files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var permitLeave = _mapper.Map<PermitLeave>(model);

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (files != null)
            {
                foreach (var file in files)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string permitLeavePath = @"images\PermitLeaves\PermitLeaveUser-" + permitLeave.Id;
                    string finalPath = Path.Combine(wwwRootPath, permitLeavePath);

                    if (!Directory.Exists(finalPath))
                    {
                        Directory.CreateDirectory(finalPath);
                    }
                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    PermitLeaveImage permitLeaveImage = new PermitLeaveImage()
                    {
                        ImageUrl = @"\" + permitLeavePath + @"\" + fileName,
                        LeaveId = permitLeave.Id
                    };


                    if (permitLeave.PermitLeaveImages == null)
                    {
                        permitLeave.PermitLeaveImages = new List<PermitLeaveImage>();
                    }
                    permitLeave.PermitLeaveImages.Add(permitLeaveImage);
                }
            }
            _unitOfWork.PermitLeave.Add(permitLeave);
            _unitOfWork.Save();

            return CreatedAtRoute("GetPermitLeave", new { leaveID =permitLeave.Id},permitLeave);
        }
        [HttpPut("{leaveID:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePermitLeave(int leaveID, [FromForm] UpdatePermitLeaveDTO model, [FromForm] List<IFormFile>? files)
        {
            if (leaveID == 0 || leaveID == null)
            {
                return BadRequest();
            }
            var permitLeave = _unitOfWork.PermitLeave.Get(c => c.Id == leaveID);
            if (permitLeave == null)
            {
                return NotFound();
            }
            permitLeave = _mapper.Map<PermitLeave>(model);
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (files != null)
            {
                foreach (var file in files)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string permitLeavePath = @"images\PermitLeaves\PermitLeaveUser-" + permitLeave.Id;
                    string finalPath = Path.Combine(wwwRootPath, permitLeavePath);

                    if (!Directory.Exists(finalPath))
                    {
                        Directory.CreateDirectory(finalPath);
                    }
                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    PermitLeaveImage permitLeaveImage = new PermitLeaveImage()
                    {
                        ImageUrl = @"\" + permitLeavePath + @"\" + fileName,
                        LeaveId = permitLeave.Id
                    };


                    if (permitLeave.PermitLeaveImages == null)
                    {
                        permitLeave.PermitLeaveImages = new List<PermitLeaveImage>();
                    }
                    permitLeave.PermitLeaveImages.Add(permitLeaveImage);
                }
            }
            _unitOfWork.PermitLeave.Update(permitLeave);
            _unitOfWork.Save();
            return NoContent();
        }
        [HttpDelete("{leaveID:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePermitLeave(int leaveID)
        {
            if (leaveID == 0 || leaveID == null)
            {
                return BadRequest();
            }
            var permitLeave = _unitOfWork.PermitLeave.Get(c => c.Id == leaveID);
            if (permitLeave == null)
            {
                return NotFound();
            }
            string permitLeavePath = @"images\Permitleaves\PermitLeaveUser-" +permitLeave.Id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, permitLeavePath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }
                Directory.Delete(finalPath);
            }

            _unitOfWork.PermitLeave.Remove(permitLeave);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
