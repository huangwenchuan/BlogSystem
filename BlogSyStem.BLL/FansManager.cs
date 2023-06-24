using BlogSyStem.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.BLL
{
    public class FansManager : IFansManager
    {
        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="followUserId">用户取消关注Id</param>
        /// <returns></returns>
        public async Task CancelFollow(Guid userId, Guid followUserId)
        {
            using (IDAL.IFansService fansService = new DAL.FansService())
            {
                var temp = fansService.GetAllAsync().FirstOrDefault(m => m.UserId == userId && m.FocusUserId == followUserId);
                if (temp != null)
                {
                    await fansService.RemoveAsync(temp);
                }
            }
        }

        /// <summary>
        /// 关注
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="followUserId">用户关注人Id</param>
        /// <returns></returns>
        public async Task Follow(Guid userId, Guid followUserId)
        {
            using (IDAL.IFansService fansService = new DAL.FansService())
            {
                await fansService.CreateAsync(new Models.Fans()
                {
                    UserId = userId,
                    FocusUserId = followUserId
                });
            }

        }

        /// <summary>
        /// 获取粉丝Id
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public string[] GetFansUserIdList(Guid userId)
        {
            string[] temp = null;
            using (IDAL.IFansService fansService= new DAL.FansService())
            {
                var userList = fansService.GetAllAsync().Where(m => m.FocusUserId == userId).ToList();
                foreach (var item in userList)
                {
                    temp.Append(item.UserId.ToString());
                }
            }
            return temp;
        }

        /// <summary>
        /// 获取关注的人的Id
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public string[] GetFollowUserIdList(Guid userId)
        {
            string[] temp = null;
            using (IDAL.IFansService fansService = new DAL.FansService())
            {
                var userList = fansService.GetAllAsync().Where(m => m.UserId == userId).ToList();
                foreach (var item in userList)
                {
                    temp.Append(item.FocusUserId.ToString());
                }
            }
            return temp;
        }
    }
}
