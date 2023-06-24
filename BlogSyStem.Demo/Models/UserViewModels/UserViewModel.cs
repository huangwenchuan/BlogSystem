using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogSyStem.Dto;
namespace BlogSyStem.Demo.Models.UserViewModels
{
    public class UserViewModel
    {
        /// <summary>
        /// 关注你的用户
        /// </summary>
        public List<UserInfoDto> User_FansList { get; set; }

        /// <summary>
        /// 你关注的用户
        /// </summary>
        public List<UserInfoDto> User_FollowList { get; set; }

        /// <summary>
        /// 推荐的用户（你没有关注的用户）
        /// </summary>
        public List<UserInfoDto> User_RecommendList { get; set; }
    }
}