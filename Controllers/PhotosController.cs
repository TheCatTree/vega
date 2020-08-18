using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using vega.Controllers.Resources;
using vega.Core;
using vega.Core.Models;

namespace vega.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IWebHostEnvironment host;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly PhotoSettings photoSettings;
        private readonly IMapper mapper;
        private readonly IPhotoRepository photoRepository;
        public PhotosController(IWebHostEnvironment host, IVehicleRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> options, IPhotoRepository photoRepository)
        {
            this.photoRepository = photoRepository;
            this.photoSettings = options.Value;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.host = host;

        }

        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId)
        {
            var photos = await photoRepository.GetPhotos(vehicleId);

            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            System.Diagnostics.Debug.WriteLine("vehicleId");
            System.Diagnostics.Debug.WriteLine(vehicleId);
            var vehicle = await this.repository.GetVehicleAsync(vehicleId, includeRelated: false);
            if (vehicle == null)
                return NotFound();

            if (file == null) return BadRequest("Null file");

            if (file.Length == 0) return BadRequest("Empty file");

            if (file.Length > photoSettings.MaxBytes) return BadRequest("Max file size exceeded");

            System.Diagnostics.Debug.Write("file: ");
            System.Diagnostics.Debug.WriteLine(file);
            System.Diagnostics.Debug.Write("file.name: ");
            System.Diagnostics.Debug.WriteLine((file.Name).ToString());
            System.Diagnostics.Debug.Write("file.FileName: ");
            System.Diagnostics.Debug.WriteLine(file.FileName);
            System.Diagnostics.Debug.Write("file.Length: ");
            System.Diagnostics.Debug.WriteLine(file.Length);
            System.Diagnostics.Debug.Write("Path.GetExtension(file.FileName): ");
            System.Diagnostics.Debug.WriteLine(Path.GetExtension(file.FileName));
            System.Diagnostics.Debug.Write("Path.GetFileName(file.FileName): ");
            System.Diagnostics.Debug.WriteLine(Path.GetFileName(file.FileName));

            if (!photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid File Type");

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filepath = Path.Combine(uploadsFolderPath, fileName);
            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            return Ok(mapper.Map<Photo, PhotoResource>(photo));

        }
    }
}