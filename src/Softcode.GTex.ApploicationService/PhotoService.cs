using System.Drawing;
using System.IO;

namespace Softcode.GTex.ApploicationService
{
    public class PhotoService : BaseService<PhotoService>, IPhotoService
    {
        public PhotoService(IApplicationServiceBuilder applicationServiceBuilder) 
            : base(applicationServiceBuilder)
        {
        }
        
        /// <summary>
        /// Get image file by file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] GetImageFile(string fileName)
        {
            byte[] imageBytes = null;
            string folderName = "images";
            string webRootPath = HostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            if (Directory.Exists(newPath))
            {
                string fullPath = Path.Combine(newPath, fileName);
                using (Image image = Image.FromFile(fullPath))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        imageBytes = m.ToArray();
                        //base64String = Convert.ToBase64String(imageBytes);
                    }
                }
            }

            return imageBytes;
        }


    }
}
