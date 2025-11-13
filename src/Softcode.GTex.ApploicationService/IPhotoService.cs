using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IPhotoService
    {
        byte[] GetImageFile(string fileName);
    }
}
