using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using PhotoSauce.MagicScaler;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SiteMarie.Server.API.Client.Database;
using SiteMarie.Server.API.Client.Interfaces;

namespace SiteMarie.Server.API.Client.Repositories
{
    public class PictureRepository : BaseRepository<Picture>, IPictureRepository
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IConfiguration _configuration;

        public PictureRepository(IDbConnectionFactory connectionFactory, ICategoryRepository categoryRepository, IConfiguration configuration) : base(connectionFactory)
        {
            _categoryRepository = categoryRepository;
            _configuration = configuration;
        }
        
        public override Picture Add(Picture picture)
        {
            picture.Id = Guid.NewGuid();
            if(picture.PictureCategories == null){
                picture.PictureCategories = new List<PictureCategory>();
            }

            foreach(var pcat in picture.PictureCategories)
            {
                pcat.Id = Guid.NewGuid();
                pcat.PictureId = picture.Id;
            }
            picture.PictureCategories.Add(new PictureCategory {
                Id = Guid.NewGuid(),
                PictureId = picture.Id,
                CategoryId = new Guid("40000000-0000-0000-0000-000000000001")
            });
            
            picture.Path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", picture.FileName);
            picture.ThumbPath = CreateThumbnail(picture);
            
            using(var outStream = File.Create(picture.Path))
            {
                picture.File.CopyTo(outStream);
                var image = new Bitmap(outStream);
                picture.Width = image.Width;
                picture.Height = image.Height;
            }
            return base.Add(picture);            
        }

        private string CreateThumbnail(Picture picture)
        {
            var photoByte = picture.File;
            var path = Regex.Replace(picture.Path, @"(\.[\w\d_-]+)$", "_thumb$1");

            using(var inStream = new MemoryStream())
            using(var outStream = File.Create(path))
            {
                picture.File.CopyTo(inStream);
                var img = Image.FromStream(inStream);
                inStream.Position = 0;
                //Resize
                var settings = new ProcessImageSettings{ Width = img.Width/4 };
                MagicImageProcessor.ProcessImage(inStream, outStream, settings);
            }
            ApplyWatermark("RizDeLHuile", path);
            return path;
        }


        private void ApplyWatermark(string watermarkText, string path)
        {
            using (var bitmap = Bitmap.FromFile(path))
            {
                using (var tempBitmap = new Bitmap(bitmap.Width, bitmap.Height))
                {
                    using (Graphics grp = Graphics.FromImage(tempBitmap))
                    {
                        grp.DrawImage(bitmap,0,0);
                        bitmap.Dispose();
                        Brush brush = new SolidBrush(Color.FromArgb(150, 255, 255, 255));
                        Font font = new System.Drawing.Font("Segoe UI", 50, FontStyle.Bold, GraphicsUnit.Pixel);
                        SizeF textSize = grp.MeasureString(watermarkText, font);
                        Point position = new Point(((int)textSize.Width/2)+10 , 
                            (tempBitmap.Height/2 - ((int)textSize.Height + 10)));
                        grp.DrawString(watermarkText, font, brush, position);
                        tempBitmap.Save(path);
                    }
                }
            }
        }
        public byte[] GetPictureFile(Guid pictureId)
        {
            var picture = GetById(pictureId);
            if(picture == null)
            {
                throw new ArgumentException("Can't find picture");
            }
            return File.ReadAllBytes(picture.ThumbPath);
        }

        public override void Remove(Guid id)
        {
            var picture = GetById(id);
            if(picture == null)
            {
                throw new ArgumentException("Can't find picture");
            }
            using (var connection = ConnectionFactory.Open())
            {
                connection.DeleteById<Picture>(picture.Id);
                File.Delete(picture.ThumbPath);
                File.Delete(picture.Path);
            }
        }

        public byte[] DownloadPictureFile(Guid id, int ratio)
        {
            var picture = GetById(id);
            if(picture == null)
            {
                throw new ArgumentException("Can't find picture");
            }

            using(var inStream =  new MemoryStream(File.ReadAllBytes(picture.Path)))
            using(var outStream = new MemoryStream())
            using (var img = Image.FromStream(inStream))
            {
                //Resize
                var settings = new ProcessImageSettings{ Width = img.Width * (1/ratio) , Height = img.Height * (1/ratio) };
                MagicImageProcessor.ProcessImage(inStream, outStream, settings);
                return outStream.GetBuffer();
            }
        }
    }
}