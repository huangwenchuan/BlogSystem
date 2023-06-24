using BlogSyStem.IDAL;
using BlogSyStem.Models;

namespace BlogSyStem.DAL
{
    public class RealTimeVerificationService : BaseService<Models.RealTimeVerification>, IRealTimeVerificationService
    {
        public RealTimeVerificationService() : base(new BlogContext())
        {

        }
    }
}
