using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Social
{
    public class ElmahExceptionLogger: ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var httpContext = HttpContext.Current;
            var signal = Elmah.ErrorSignal.FromContext(httpContext);
            signal.Raise(context.Exception, httpContext);
        }
    }
}