using BlogSyStem.IDAL;
using BlogSyStem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.DAL
{
    public class UserService : BaseService<Models.User>, IUserService
    {
        /// <summary>
        /// 继承的父类的构造函数需要一个上下这里就new BlogContext()
        /// </summary>
        public UserService():base(new BlogContext())
        {

        }
        
        /// <summary>
        /// EF自定义SQL
        /// </summary>
        /// <returns></returns>
        public string GetUserInfo()
        {
            string result = "";
            using (var context = new BlogContext())
            {
                var blogs = context.Users.SqlQuery("SELECT * FROM dbo.Users").ToList();
                result = JsonConvert.SerializeObject(blogs);
            }
            return result;
        }
    }
}
