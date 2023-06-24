using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogSyStem.Demo.Filters
{
    /// <summary>
    /// 过滤器 登录验证如果没有 邮箱就是登录页面
    /// </summary>
    public class BlogSyStemAuthAttribute: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            ///当用户存储在cookies为空时 将cookies的值赋给Session
            if (filterContext.HttpContext.Session["loginName"] == null&& filterContext.HttpContext.Request.Cookies["loginName"] != null)
            {
                filterContext.HttpContext.Session["loginName"] = filterContext.HttpContext.Request.Cookies["loginName"].Value;
                filterContext.HttpContext.Session["loginUserId"] = filterContext.HttpContext.Request.Cookies["loginUserId"].Value;
                IBLL.IUserManager userManager = new BLL.UserManager();
                Dto.UserInfoDto user = userManager.GetUserById(Guid.Parse(filterContext.HttpContext.Request.Cookies["loginUserId"].Value));
                filterContext.HttpContext.Session["loginUser"] = user;
            }
            //base.OnAuthorization(filterContext); 当登录名不存在时就进入Login登录页面
            if (!(filterContext.HttpContext.Session["loginName"] !=null||filterContext.HttpContext.Request.Cookies["loginName"] !=null))
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary()
                {
                    {"controller","Home" },
                    {"action","Login" }
                });
            }
        }
    }
}