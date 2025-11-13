using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Softcode.GTex.Api.Dms.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicationService.Dms;
using Softcode.GTex.ApplicationService.Dms.Models;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Dms.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/dms/document")]
    public class DocumentController : BaseController<DocumentController>
    {
        private readonly IFileService fileService;
        private readonly IBusinessProfileService businessProfileService;
        private readonly ICustomCategoryService customCategoryService;
        
       /// <summary>
       /// 
       /// </summary>
       /// <param name="businessProfileService"></param>
       /// <param name="customCategoryService"></param>
       /// <param name="fileService"></param>
        public DocumentController(IBusinessProfileService businessProfileService
            , ICustomCategoryService customCategoryService
            , IFileService fileService)
        {
            this.fileService = fileService;
            this.businessProfileService = businessProfileService;
            this.customCategoryService = customCategoryService;
        }

        /// <summary>
        /// Get Documents Async
        /// </summary>
        /// <param name="loadOptions"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-documents")]
        [ActionAuthorize(ApplicationPermission.Document.ShowDocumentList)]
        public async Task<IActionResult> GetDocumentsAsync(DataSourceLoadOptions loadOptions)
        {
            return Ok(new ResponseMessage<LoadResult> { Result = await fileService.GetDocumentsAsync(loadOptions) });
        }

        /// <summary>
        /// Get Document By Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-document/{id:int}")]
        [ActionAuthorize(ApplicationPermission.Document.ViewDocumentDetails)]
        public async Task<IActionResult> GetDocumentByIdAsync([Range(0, int.MaxValue)]int id)
        {
            ResponseMessage<UploadFileViewModel> response = new ResponseMessage<UploadFileViewModel>
            {
                Result = new UploadFileViewModel
                {
                    DocumentTypeSelectItems = await customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.DocumentType),
                    DocumentForSelectItems = customCategoryService.GetEntityTypeListAsync().Result,
                    BusinessProfileSelectItems = businessProfileService.GetUserBusinessProfileSelectItemsAsync().Result,
                    DocumentMetadata = fileService.GetDocumentByIdAsync(id).Result,
                    IsDefaultBusinessProfile = LoggedInUser.IsDefaultBusinessProfile
                }
            };

            return Ok(response);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityTypeId"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-documents-by-entity/{entityTypeId:int}/{entityId:int}")]
        [ActionAuthorize(ApplicationPermission.Document.GetDocumentsByEntity)]
        public async Task<IActionResult> GetDocumentsByEntityAsync([Range(1, int.MaxValue)]int entityTypeId, [Range(1, int.MaxValue)]int entityId)
        {

            ResponseMessage<DocumentFileStoreViewModel> response = new ResponseMessage<DocumentFileStoreViewModel>
            {
                Result = new DocumentFileStoreViewModel
                {
                    Files = await fileService.GetDocumentsByEntityAsync(entityTypeId, entityId)
                }
            };

            return Ok(response);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityTypeId"></param>
        /// <param name="entityId"></param>
        /// <param name="models"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("save-documents-by-entity/{entityTypeId:int}/{entityId:int}")]
        [ActionAuthorize(ApplicationPermission.Document.SaveDocumentsByEntity)]
        public async Task<IActionResult> SavecumentsByEntityAsync([Range(1, int.MaxValue)]int entityTypeId, [Range(1, int.MaxValue)]int entityId, [FromBody] List<AttachedFileModel> models)
        {
            ResponseMessage<bool> response = new ResponseMessage<bool>
            {
                Result = await fileService.SaveDocumentsByEntityAsync(entityTypeId, entityId, models)
            };

            return Ok(response);

        }

        /// <summary>
        /// Download File By Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-download-file-name-by-id/{id:int}")]
        [ActionAuthorize(ApplicationPermission.Document.DownloadDocument)]
        public async Task<IActionResult> GetDownloadFileNameByIdAsync([Range(1, int.MaxValue)]int id)
        {
            return Ok(new ResponseMessage<string> { Result = await fileService.GetDownloadFileNameAsync(id) });
        }

        /// <summary>
        /// Download File By Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("download-lastest-file/{id:int}")]
        [ActionAuthorize(ApplicationPermission.Document.DownloadDocument)]
        public async Task<IActionResult> DownloadLatestFileByIdAsync([Range(1, int.MaxValue)]int id)
        {
            DownloadFileModel downloadFileModel = await fileService.DownloadLatestFileAsync(id);

            return File(downloadFileModel.File, downloadFileModel.MimeType, downloadFileModel.FileName);
        }

        /// <summary>
        /// Download File By Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("download-file-by-id/{id:int}")]
        [ActionAuthorize(ApplicationPermission.Document.DownloadDocument)]
        public async Task<IActionResult> DownloadFileByIdAsync([Range(1, int.MaxValue)]int id)
        {
            DownloadFileModel downloadFileModel = await fileService.DownloadFileAsync(id);

            return File(downloadFileModel.File, downloadFileModel.MimeType, downloadFileModel.FileName);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-public-key/{id:int}/{key:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicKeyAsync([Range(1, int.MaxValue)]int id, Guid key)
        {
            DocumentPublicKeyModel model = new DocumentPublicKeyModel { Id = id.ToString(), DocumentKey = key.ToString() };

            string publicKey = await Task.Run(() => Utilities.Encrypt(JsonConvert.SerializeObject(model), true));
            return Ok(new ResponseMessage<string> { Result = publicKey });
        }

        /// <summary>
        /// Download Async
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("download/{key}")]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadAsync(string key)
        {
            return Ok(await fileService.DownloadPublicFileAsync(key));
        }


        /// <summary>
        /// Download File By Id Async
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-protected-file-name/{key:guid}")]
        [ActionAuthorize(ApplicationPermission.Document.DownloadDocument)]
        public async Task<IActionResult> GetProtectedFileNameAsync(Guid key)
        {
            return Ok(new ResponseMessage<string> { Result = await fileService.GetDownloadFileNameAsync(key) });
        }

        /// <summary>
        /// Download Async
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("download-protected/{key}")]
        [ActionAuthorize(ApplicationPermission.Document.DownloadDocument)]
        public async Task<IActionResult> DownloadProtectedAsync(Guid key)
        {
            DownloadFileModel downloadFileModel = await fileService.DownloadFileAsync(key);

            return File(downloadFileModel.File, downloadFileModel.MimeType, downloadFileModel.FileName);
        }

        /// <summary>
        /// Upload File
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("upload-file")]
        //TODO: [ActionAuthorize(ApplicationPermission.Document.UploadDocument)]
        [AllowAnonymous]
        public IActionResult UploadFile()
        {
            ResponseMessage<string> response = new ResponseMessage<string>();

            try
            {
                IFormFile file = Request.Form.Files[0];
                string folderName = "tempfilestore";
                string fileName = "";
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
                response.Message = "File cannot be uploaded.";
                return Ok(response);
            }
        }


        /// <summary>
        /// Save Uploaded Documents Async
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("save-documents")]
        [ActionAuthorize(ApplicationPermission.Document.CreateDocument)]
        public async Task<IActionResult> SaveDocumentsAsync([FromBody] DocumentMetadataModel document)
        {
            ResponseMessage<bool> response = new ResponseMessage<bool>
            {
                Result = await fileService.SaveDocumentsAsync(0, document)
            };

            return Ok(response);
        }



        /// <summary>
        /// Save Uploaded Documents Async
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("update-document/{id:int}")]
        [ActionAuthorize(ApplicationPermission.Document.UpdateDocument)]
        public async Task<IActionResult> UpdateDocumentAsync(int id, [FromBody] DocumentMetadataModel document)
        {
            ResponseMessage<bool> response = new ResponseMessage<bool>
            {
                Result = await fileService.SaveDocumentsAsync(id, document)
            };

            return Ok(response);
        }

        /// <summary>
        /// delete contacts by id list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-document/{id:int}")]
        [ActionAuthorize(ApplicationPermission.Document.DeleteDocument)]
        public async Task<IActionResult> DeleteDocumentAsync(int id)
        {
            return Ok(await fileService.DeleteDocumentAsync(id));
        }

        /// <summary>
        /// delete contacts by id list
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete-documents")]
        [ActionAuthorize(ApplicationPermission.Document.DeleteDocument)]
        public async Task<IActionResult> DeleteDocumentsAsync([FromBody]List<int> ids)
        {
            return Ok(await fileService.DeleteDocumentsAsync(ids));
        }
    }
}
