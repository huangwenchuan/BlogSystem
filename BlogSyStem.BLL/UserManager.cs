using BlogSyStem.Dto;
using BlogSyStem.IBLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.BLL
{
    public class UserManager : IUserManager
    {
        public async Task<List<UserInfoDto>> GetAllUserList()
        {
            IBLL.IFansManager fansManager = new BLL.FansManager();
            IDAL.IUserService userService = new DAL.UserService();
            List<UserInfoDto> tempList = await userService.GetAllAsync().Where(m => !m.IsRemoved).Select(m => new UserInfoDto()
            {
                Id = m.Id,
                Email = m.Email,
                ImagePath = m.ImagePath,
                CreateTime = m.CreateTime,
                NickName = m.NickName
            }).ToListAsync();
            return tempList;
        }

        /// <summary>
        /// 推荐用户推荐没有关注的用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserInfoDto>> GetCancelFollowUserList(Guid userId)
        {

            IBLL.IFansManager fansManager = new BLL.FansManager();
            string[] tempListId = fansManager.GetFollowUserIdList(userId);
            IDAL.IUserService userService = new DAL.UserService();
            List<UserInfoDto> tempList = await userService.GetAllAsync().Where(m => !m.IsRemoved).Select(m => new UserInfoDto()
            {
                Id = m.Id,
                Email = m.Email,
                ImagePath = m.ImagePath,
                CreateTime = m.CreateTime,
                NickName = m.NickName
            }).ToListAsync();
            using (userService)
            {
                foreach (var item in tempListId)
                {
                    UserInfoDto model = await userService.GetAllAsync().Where(m => m.Id.ToString() == item).Select(m => new UserInfoDto()
                    {
                        Id = m.Id,
                        Email = m.Email,
                        ImagePath = m.ImagePath,
                        CreateTime = m.CreateTime,
                        NickName = m.NickName
                    }).FirstAsync();
                    if (model != null)
                    {
                        tempList.Remove(model);
                    }
                }
            }
            return tempList;
        }

        /// <summary>
        /// 获取粉丝列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserInfoDto>> GetFansUserList(Guid userId)
        {
            IBLL.IFansManager fansManager = new BLL.FansManager();
            string[] tempListId = fansManager.GetFansUserIdList(userId);
            List<UserInfoDto> tempList = null;
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                foreach (var item in tempListId)
                {
                    UserInfoDto model = await userService.GetAllAsync().Where(m => m.Id.ToString() == item).Select(m => new UserInfoDto()
                    {
                        Id = m.Id,
                        Email = m.Email,
                        ImagePath = m.ImagePath,
                        CreateTime = m.CreateTime,
                        NickName = m.NickName
                    }).FirstAsync();
                    if (model != null)
                    {
                        tempList.Add(model);
                    }
                }
            }
            return tempList;
        }

        /// <summary>
        /// 获取关注列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserInfoDto>> GetFollowUserList(Guid userId)
        {
            IBLL.IFansManager fansManager = new BLL.FansManager();
            string[] tempListId = fansManager.GetFollowUserIdList(userId);
            List<UserInfoDto> tempList = null;
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                foreach (var item in tempListId)
                {
                    UserInfoDto model = await userService.GetAllAsync().Where(m => m.Id.ToString() == item).Select(m => new UserInfoDto()
                    {
                        Id = m.Id,
                        Email = m.Email,
                        ImagePath = m.ImagePath,
                        CreateTime = m.CreateTime,
                        NickName = m.NickName
                    }).FirstAsync();
                    if (model != null)
                    {
                        tempList.Add(model);
                    }
                }
            }
            return tempList;
        }

        public UserInfoDto GetUserByEmail(string email)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                var temp = userSvc.GetAllAsync().Any(arg => arg.Email == email);
                if (temp)
                {
                    return userSvc.GetAllAsync().Where(arg => arg.Email == email).Select(arg => new UserInfoDto()
                    {
                        Id = arg.Id,
                        Email = arg.Email,
                        ImagePath = arg.ImagePath,
                        CreateTime = arg.CreateTime,
                        NickName = arg.NickName
                    }).FirstOrDefault();
                }
                else
                {
                    //返回一个异常
                    throw new ArgumentException("邮箱地址不存在");
                }
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="email">账号</param>
        /// <param name="pwd">密码</param>
        /// <param name="userId">输出一个Id 这个时候方法上就不能加异步 但是接口方法是异步方法 所以用 Wait() 叫异步的同步化</param>
        /// <returns></returns>
        public bool Login(string email, string pwd, out Guid userId)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                var user = userSvc.GetAllAsync().FirstOrDefaultAsync(arg => arg.Email == email && arg.PassWord == pwd);
                user.Wait();
                var data = user.Result;
                if (data == null)
                {
                    userId = new Guid();
                    return false;
                }
                else
                {
                    userId = data.Id;
                    return true;
                }

            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="email">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public async Task Register(string email, string pwd)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                await userSvc.CreateAsync(new Models.User()
                {
                    Email = email,
                    PassWord = pwd
                });
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        public async Task UpdatePwd(string email, string oldPwd, string newPwd)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                if (await userSvc.GetAllAsync().AnyAsync(arg => arg.Email == email && arg.PassWord == oldPwd))
                {
                    var user = await userSvc.GetAllAsync().FirstAsync(arg => arg.Email == email);
                    user.PassWord = newPwd;
                    await userSvc.EditAsync(user);
                }
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="nickName">昵称</param>
        /// <param name="imagePath">头像地址</param>
        /// <returns></returns>
        public async Task UpdateUserInfo(string email, string nickName)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                if (await userSvc.GetAllAsync().AnyAsync(arg => arg.Email == email))
                {
                    var user = await userSvc.GetAllAsync().FirstAsync(arg => arg.Email == email);
                    user.NickName = nickName;
                    await userSvc.EditAsync(user);
                }
            }
        }

        /// <summary>
        /// 测试自定义SQL
        /// </summary>
        /// <returns></returns>
        public string GetUser()
        {
            string sql = "select * from Users";
            String result = JsonConvert.SerializeObject(Maticsoft.DBUtility.DbHelperSQL.QueryTable(sql));
            return result;
        }

        /// <summary>
        /// 测试EF自定义SQL
        /// </summary>
        /// <returns></returns>
        public string GetUserInfo()
        {
            DAL.UserService userService = new DAL.UserService();
            return userService.GetUserInfo();
        }

        /// <summary>
        /// 根据Id获得用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserInfoDto GetUserById(Guid userId)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                if (userSvc.GetAllAsync().Any(arg => arg.Id == userId))
                {
                    var tempUser = userSvc.GetAllAsync().Where(arg => arg.Id == userId).Select(arg => new UserInfoDto()
                    {
                        Id = arg.Id,
                        Email = arg.Email,
                        ImagePath = arg.ImagePath,
                        CreateTime = arg.CreateTime,
                        NickName = arg.NickName
                    }).First();
                    IDAL.IFansService fansService = new DAL.FansService();
                    tempUser.FocusCount = fansService.GetAllAsync().Where(arg => arg.UserId == tempUser.Id).Count();
                    tempUser.FansCount = fansService.GetAllAsync().Where(arg => arg.FocusUserId == tempUser.Id).Count();
                    return tempUser;
                }
                else
                {
                    //返回一个异常
                    throw new ArgumentException("用户不存在");
                }
            }
        }

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="imagePath">头像地址</param>
        /// <returns></returns>
        public async Task UpdateHeadPortrait(Guid userId, string imagePath)
        {
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                var user = await userService.GetModelByIdAsync(userId);
                user.ImagePath = imagePath;
                await userService.EditAsync(user);
            }
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task CreateCode(string email)
        {
            var temp = GetUserByEmail(email);
            if (temp == null)
            {
                await SendCode(email, Guid.Empty);
                
            }
            else if (temp != null)
            {
                await SendCode(email, temp.Id);
            }
        }

        public async Task SendCode(string email, Guid userid)
        {
            int randomParam = new Random().Next(100000, 999999);
            //验证码
            // 设置发送方的邮件信息,例如使用腾讯的smtp
            string smtpServer = "smtp.qq.com"; //SMTP服务器
            string mailFrom = "2414275966@qq.com"; //登陆用户名
            string userPassword = "bgnfktiljngjeabi";//登陆密码,如果使用的是腾讯的 用的是授权码

            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpServer; //指定SMTP服务器

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码

            // 发送邮件设置       
            MailMessage mailMessage = new MailMessage(mailFrom, email); // 发送人和收件人
            mailMessage.Subject = "验证码";//主题
            mailMessage.Body = "验证码：" + randomParam.ToString() + "有效时间五分钟，请把握时间。";//内容
            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.Low;//优先级
            try
            {
                smtpClient.Send(mailMessage); // 发送邮件
                using (IDAL.IRealTimeVerificationService service = new DAL.RealTimeVerificationService())
                {
                    await service.CreateAsync(new Models.RealTimeVerification()
                    {
                        UserId = userid,
                        Code = randomParam.ToString(),
                        Email = email
                    });
                }
            }
            catch (SmtpException ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public string GetCode(string email)
        {
            if (email.ToString() == "")
            {
                return "";
            }
            else
            {
                string sql = "select top 1 Code from RealTimeVerifications where Email='" + email + "' and CreateTime between DATEADD(minute,-5,GETDATE()) and getdate() order by CreateTime desc";
                var data = Maticsoft.DBUtility.DbHelperSQL.QueryTable(sql);
                if (data.Rows.Count > 0)
                {
                    return data.Rows[0]["Code"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        public UserInfoDto Login(string email, string code)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                var user = GetUserByEmail(email);
                if (user != null && user.ToString() != "")
                {
                    string _code = GetCode(email);
                    if (code == _code)
                    {
                        var model = GetUserById(user.Id);
                        return model;
                    }
                    else
                    {
                        return new UserInfoDto();
                    }
                }
                else
                {
                    return new UserInfoDto();
                }
            }
        }

    }
}
