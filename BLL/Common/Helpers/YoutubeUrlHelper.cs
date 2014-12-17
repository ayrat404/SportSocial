using System;
using System.Security.Policy;
using System.Web;

namespace BLL.Common.Helpers
{
    static public class YoutubeUrlHelper
    {
        public const string ImageUrlStart = "http://i1.ytimg.com/vi";
        public const string MaxResImageName = "maxresdefault.jpg";

        public static bool UrlIsValid(string youtubeUrl)
        {
            if (string.IsNullOrEmpty(youtubeUrl))
                return true;
            Uri url;
            try
            {
                url = new Uri(youtubeUrl);
            }
            catch
            {
                return false;
            }
            var videoId = HttpUtility.ParseQueryString(url.Query)["v"];
            if ((url.Host.Contains("youtube.") && !string.IsNullOrEmpty(videoId))
                || url.Host.Contains("youtu.be"))
            {
                return true;
            }
            return false;
        }

        public static string EmbeddedYoutubeUrl(string youtubeUrl)
        {
            if (string.IsNullOrEmpty(youtubeUrl))
                return youtubeUrl;
            var url = new Uri(youtubeUrl);
            string videoId;
            videoId = url.Host.Contains("youtu.be") ? url.Segments[1] : HttpUtility.ParseQueryString(url.Query)["v"];
            return "//www.youtube.com/embed/" + videoId;
        }

        public static string MaxResolutionImageUrl(string youtubeUrl)
        {
            var url = new Uri(youtubeUrl);
            string videoId = url.Host.Contains("youtu.be") ? url.Segments[1] : HttpUtility.ParseQueryString(url.Query)["v"];
            return string.Format("{0}/{1}/{2}", ImageUrlStart, videoId, MaxResImageName);
        }
    }
}