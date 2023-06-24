using BlogSyStem.Demo.Filters;
using BlogSyStem.Demo.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using System.Threading;

namespace BlogSyStem.Demo.Controllers
{
    [BlogSyStemAuth]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditUserHeadPortrait(Guid? id)
        {
            IBLL.IUserManager userManager = new BLL.UserManager();
            var data = userManager.GetUserById(id.Value);
            return View(data);
        }

        [HttpPost]
        public async Task<ActionResult> EditUserHeadPortrait(string _userId, HttpPostedFileBase imagePath) 
        {
            Guid userId = Guid.Parse(_userId);
            IBLL.IUserManager userManager = new BLL.UserManager();
            var data = userManager.GetUserById(userId);
            var webPath = Server.MapPath(string.Format("~/{0}", "HeadPortrait\\UserHeadPortait"));
            if (data.ImagePath!=""&& data.ImagePath!= "/HeadPortrait/personal.png")
            {
                //删除原有静态文件
                string uploadFolder = Path.Combine(webPath);
                var uniqueFileName = data.ImagePath;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                System.IO.File.Delete(filePath);

                //添加现有文件
                var fileName = Guid.NewGuid().ToString() + "_" + imagePath.FileName;//图片名
                imagePath.SaveAs(Path.Combine(webPath, fileName));
                await userManager.UpdateHeadPortrait(userId, fileName);
                await userManager.UpdateHeadPortrait(userId, fileName);
                return RedirectToAction("EditUserHeadPortrait",new { id= userId });
            }
            else
            {
                //添加现有文件
                var fileName = Guid.NewGuid().ToString()+"_" +imagePath.FileName;//图片名
                imagePath.SaveAs(Path.Combine(webPath, fileName));
                await userManager.UpdateHeadPortrait(userId, fileName);
                return RedirectToAction("EditUserHeadPortrait", new { id = userId });
            }
        }
    }
}