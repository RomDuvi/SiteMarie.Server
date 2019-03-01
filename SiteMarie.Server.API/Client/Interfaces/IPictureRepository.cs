using System;
using SiteMarie.Server.API.Client.Database;

namespace SiteMarie.Server.API.Client.Interfaces
{
    public interface IPictureRepository : IBaseRepository<Picture>
    {
         byte[] GetPictureFile(Guid pictureId);
         byte[] DownloadPictureFile(Guid id, int ratio);
    }
}