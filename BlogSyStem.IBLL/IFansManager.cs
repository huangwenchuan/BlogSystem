using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.IBLL
{
    public interface IFansManager
    {
        Task Follow(Guid userId, Guid followUserId);

        Task CancelFollow(Guid userId, Guid followUserId);

        string[] GetFollowUserIdList(Guid userId);

        string[] GetFansUserIdList(Guid userId);
    }
}
