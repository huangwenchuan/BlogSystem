using BlogSyStem.Demo.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogSyStem.Demo.Controllers
{
    public class HomeController : Controller
    {
        [BlogSyStemAuth]
        public ActionResult Index()
        {
            // Server.MapPath(""); //获得当前的绝对路径
            // 比如F5调试就是D:\Users\24142\Desktop\开发实例\ASP.NET MVC5\BlogSyStem\BlogSyStem.Demo
            // 如果是网站就是网站的绝对路径 D:\MyWebSite\BlogSystem
            //var temp = Server.MapPath("");
            return View();
        }

        [BlogSyStemAuth]
        public ActionResult About()
        {
            ViewBag.Message = "关于我们";
            return View();
        }

        [BlogSyStemAuth]
        public ActionResult Contact()
        {
            ViewBag.Message = "联系我们";
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Models.UserViewModels.RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IUserManager userManager = new BLL.UserManager();
                await userManager.Register(model.Email, model.PassWord);
                return Content("注册成功");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.UserViewModels.LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IUserManager userManager = new BLL.UserManager();
                Guid userId;
                if (userManager.Login(model.Email, model.PassWord, out userId))
                {
                    var user = userManager.GetUserById(userId);
                    //跳转
                    //判断是用session还是cookie
                    if (model.RememberMe)
                    {
                        Response.Cookies.Add(new HttpCookie("loginName")
                        {
                            //Expires=DateTime.Now.AddDays(7)当前时间往后七天不用登录 AddMinutes(5)当前时间往后五分钟
                            Value = model.Email,
                            Expires = DateTime.Now.AddDays(7)
                        });
                        Response.Cookies.Add(new HttpCookie("loginUserId")
                        {
                            //Expires=DateTime.Now.AddDays(7)当前时间往后七天不用登录
                            Value = userId.ToString(),
                            Expires = DateTime.Now.AddDays(7)
                        });
                    }
                    else
                    {
                        Session["loginName"] = model.Email;
                        Session["loginUserId"] = userId;
                        Session["loginUser"] = user;
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "您的账号密码有误");
                }
            }
            return View(model);
        }

        [HttpPost]
        public void GetCode(string email)
        {
            IBLL.IUserManager userManager = new BLL.UserManager();
            userManager.CreateCode(email);
        }

        //[HttpPost]
        //public ActionResult LoginByEmailCode(string email, string code)
        //{
        //    string coded = Request.Cookies["loginName"].Value;
        //    if (ModelState.IsValid)
        //    {
        //        IBLL.IUserManager userManager = new BLL.UserManager();
        //        Guid userId;
        //    }
        //}

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Clean()
        {
            HttpCookie hc = Request.Cookies["loginName"];
            if (hc == null)
            {
                return RedirectToAction("Login");
            }
            hc.Expires = DateTime.Now.AddDays(-100);
            Response.AppendCookie(hc);
            Session["loginName"] = null;
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 关注
        /// </summary>
        /// <param name="foucsUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Foucs(Guid foucsUserId)
        {
            var userid = Guid.Parse(Session["loginUserId"].ToString());
            IBLL.IFansManager fansManager = new BLL.FansManager();
            await fansManager.Follow(userid, foucsUserId);
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="foucsUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task CancelFoucs(Guid foucsUserId)
        {
            var userid = Guid.Parse(Session["loginUserId"].ToString());
            IBLL.IFansManager fansManager = new BLL.FansManager();
            await fansManager.CancelFollow(userid, foucsUserId);
        }

        [HttpPost]
        public ActionResult LoginByCode(string _Email, string _Code, bool _RememberMe)
        {
            IBLL.IUserManager userManager = new BLL.UserManager();
            var user = userManager.Login(_Email, _Code);
            //跳转
            //判断是用session还是cookie
            if (user.Id != null && user.Id.ToString() != Guid.Empty.ToString())
            {
                if (_RememberMe)
                {
                    Response.Cookies.Add(new HttpCookie("loginName")
                    {
                        //Expires=DateTime.Now.AddDays(7)当前时间往后七天不用登录 AddMinutes(5)当前时间往后五分钟
                        Value = _Email,
                        Expires = DateTime.Now.AddDays(7)
                    });
                    Response.Cookies.Add(new HttpCookie("loginUserId")
                    {
                        //Expires=DateTime.Now.AddDays(7)当前时间往后七天不用登录
                        Value = user.Id.ToString(),
                        Expires = DateTime.Now.AddDays(7)
                    });
                }
                else
                {
                    Session["loginName"] = _Email;
                    Session["loginUserId"] = user.Id.ToString();
                    Session["loginUser"] = user;
                }
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return View();
            }
        }
    }
}