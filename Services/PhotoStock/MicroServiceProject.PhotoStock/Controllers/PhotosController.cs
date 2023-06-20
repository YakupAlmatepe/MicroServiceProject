using MicroServiceProject.PhotoStock.Dtos;
using MicroServiceProject.Shared.ControllerBases;
using MicroServiceProject.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MicroServiceProject.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomeBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationTokenSource cancellationToken)
        {
            if (photo!=null& photo.Length>0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroort/photos", photo.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                //await photo.CopyToAsync(stream, cancellationToken);
                await photo.CopyToAsync(stream,cancellationToken);
                var returnPath = photo.FileName;
                //PhotoDto photoDto = new PhotoDto(){ Url = returnPath };
                PhotoDto photoDto = new PhotoDto() { Url = returnPath };
                return CreateActionResultInstance(Response<PhotoDto>.Success(200,photoDto));

            }
            else
            {
                return CreateActionResultInstance(Response<PhotoDto>.Fail("Foto bulunamadı", 400));

                     
            }
        }
    }
}
