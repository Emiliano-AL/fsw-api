using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace fsw_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public FileUploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public Task<bool> UploadFile()
        {
            bool result = false;

            var file = Request.Form.Files[0];
            string folderName = "/Files/";
            string path = _env.ContentRootPath;

            string fullPath = path + folderName;

            if(!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            if(file.Length > 0)
            {
                string fileName = file.FileName;
                string fullPathFile = Path.Combine(fullPath, fileName);

                using ( var stream = new FileStream(fullPathFile, FileMode.Create))
                {
                    file.CopyTo(stream);
                    result = true;
                }
            }

            return Task.FromResult(result);
        }
    }
}
