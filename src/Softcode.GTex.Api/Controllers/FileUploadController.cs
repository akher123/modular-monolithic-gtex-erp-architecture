using Softcode.GTex.Api.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace Softcode.GTex.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/shared/file-upload")]
    public class FileUploadController : BaseController<FileUploadController>
    { 
        /// <summary>
        /// get configuration menu
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("upload-photo")]
        [AllowAnonymous]
        public IActionResult UploadPhoto()
        {
            ResponseMessage<string> response = new ResponseMessage<string>();

            try
            {
                IFormFile file = Request.Form.Files[0];
                string folderName = "images";
                string fileName="";
                string webRootPath = HostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    fileName = Guid.NewGuid().ToString().Substring(0, 8) + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                response.Result = fileName;
                response.Message = "Upload Successful.";
                return Ok(response);
            }
            catch (Exception)
            {                
                response.Message = "Image cannot be uploaded.";
                return Ok(response);
            }
        }
    }
}