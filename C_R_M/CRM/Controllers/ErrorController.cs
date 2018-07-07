using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class ErrorController : Controller
    {
        private HttpException exception;

        public ErrorController(HttpException exception)
        {
            this.exception = exception;
        }

        public ActionResult InternalServerError()
        {
            ViewBag.Error = exception.InnerException + exception.StackTrace;
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}
