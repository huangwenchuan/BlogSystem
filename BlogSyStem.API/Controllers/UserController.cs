using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlogSyStem.API.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public List<string> TestInfo()
        {
            List<string> vs =  new List<string>();
            for (int i = 0; i < 4; i++)
            {
                vs.Add("张三" + i.ToString()+"");
            }
            var result = JsonConvert.SerializeObject(vs);
            return vs;
        }

        [HttpGet]
        public async Task<List<Dto.UserInfoDto>> GetAllUser()
        {
            IBLL.IUserManager userManager = new BLL.UserManager();
            return await userManager.GetAllUserList();
        }

        [HttpGet]
        public async Task<string> GetAllUserList()
        {
            IBLL.IUserManager userManager = new BLL.UserManager();
            var list = await userManager.GetAllUserList();
            string result =  JsonConvert.SerializeObject(list); //前端需要反序列化才能使用
            return result;
        }

        /// <summary>
        /// 自定义sql语句
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetUser()
        {
            BLL.UserManager userManager = new BLL.UserManager();
            return userManager.GetUser();
        }

        [HttpGet]
        public string GetUserInfo()
        {
            BLL.UserManager userManager = new BLL.UserManager();
            return userManager.GetUserInfo();
        }

    }
}
