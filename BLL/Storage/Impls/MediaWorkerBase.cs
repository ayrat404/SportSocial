using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using BLL.Storage.Objects.Enums;
using DAL.DomainModel;
using DAL.DomainModel.Achievement;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.JournalEntities;
using NLog;

namespace BLL.Storage.Impls
{
    public abstract class MediaWorkerBase
    {
        protected const string VirtualBlogImagePath = "/Storage/Images/Blog/"; //TODO â AppSettings
        protected const string VirtualJournslImagePath = "/Storage/Images/Journal/"; //TODO â AppSettings
        protected const string VirtualAvatarImagePath = "/Storage/User/Avatars/"; //TODO â AppSettings
        protected const string VirtualAchievementImagePath = "/Storage/User/achievements/"; //TODO â AppSettings

        private readonly string BasePath;

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        protected MediaWorkerBase()
        {
            BasePath = WebConfigurationManager.AppSettings["BasePath"];
        }

        protected Type GetMediaEntity(UploadType type)
        {
            switch (type)
            {
                case UploadType.Avatar:
                    return typeof (UserAvatarPhoto);
                case UploadType.Article:
                    return typeof (BlogImage);
                case UploadType.Journal:
                    return typeof (JournalMedia);
                case UploadType.Achievement:
                    return typeof (AchievementMedia);
            }
            throw new NotSupportedException();
        }

        protected Type GetEntity(UploadType type)
        {
            switch (type)
            {
                case UploadType.Avatar:
                    return typeof (AppUser);
                case UploadType.Article:
                    return typeof (Post);
                case UploadType.Journal:
                    return typeof (Journal);
                case UploadType.Achievement:
                    return typeof (Achievement);
            }
            throw new NotSupportedException();
        }

        protected string GetPath(UploadType type)
        {
            switch (type)
            {
                case UploadType.Avatar:
                    return VirtualAvatarImagePath;
                case UploadType.Article:
                    return VirtualBlogImagePath;
                case UploadType.Journal:
                    return VirtualJournslImagePath;
                case UploadType.Achievement:
                    return VirtualAchievementImagePath;
            }
            throw new NotSupportedException();
        }

        protected MethodInfo GetGenericMethod(string methodName, UploadType uploadType)
        {
            var methos = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            return methos.MakeGenericMethod(GetEntity(uploadType), GetMediaEntity(uploadType));
        }

        protected void SaveFile(Stream fileStream, string filePath)
        {
            using (var stream = new MemoryStream())
            {
                _logger.Info("Base pat = " + BasePath);
                var serverPath = string.IsNullOrEmpty(BasePath) ? HttpContext.Current.Server.MapPath(filePath) 
                                                                : string.Concat(BasePath, filePath);
                _logger.Info("server pat = " + serverPath);
                string dir = Path.GetDirectoryName(serverPath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                fileStream.Position = 0;
                fileStream.CopyTo(stream);
                File.WriteAllBytes(serverPath, stream.ToArray());
            }
        }
    }
}