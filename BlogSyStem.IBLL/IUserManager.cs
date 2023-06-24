using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.IBLL
{
    public interface IUserManager
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="email">账号</param>
        /// <param name="pwd">密码</param>
        Task Register(string email, string pwd);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="email">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        bool Login(string email, string pwd,out Guid userId);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="email">账号</param>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        Task UpdatePwd(string email, string oldPwd, string newPwd);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="email">账号</param>
        /// <param name="nickName">昵称</param>
        /// <returns></returns>
        Task UpdateUserInfo(string email, string nickName);

        Dto.UserInfoDto GetUserByEmail(string email);

        /// <summary>
        /// 获取关注列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Dto.UserInfoDto>> GetFollowUserList(Guid userId);

        /// <summary>
        /// 推荐用户(未关注)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Dto.UserInfoDto>> GetCancelFollowUserList(Guid userId);

        /// <summary>
        /// 获取粉丝列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Dto.UserInfoDto>> GetFansUserList(Guid userId);

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        Task<List<Dto.UserInfoDto>> GetAllUserList();

        /// <summary>
        /// 根据Id获得用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Dto.UserInfoDto GetUserById(Guid userId);

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="imagePath">头像地址</param>
        /// <returns></returns>
        Task UpdateHeadPortrait(Guid userId, string imagePath);

        Task CreateCode(string email);

        string GetCode(string email);

        Dto.UserInfoDto Login(string email, string code);
    }
}
