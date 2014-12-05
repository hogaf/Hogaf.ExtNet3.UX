using System.Web.Mvc;
using Ext.Net;
using Ext.Net.MVC;
using Hogaf.UI.Web.Models;

namespace Hogaf.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View(ExtNetModel.Data);
        }

        public ActionResult SampleAction(string message)
        {
            X.Msg.Notify(new NotificationConfig
            {
                Icon = Icon.Accept,
                Title = "Working",
                Html = message
            }).Show();

            return this.Direct();
        }
    }
}