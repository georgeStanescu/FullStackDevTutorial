using GigHub.Models;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string followerId, string followeeId)
        {
            return _context.Followings
                    .SingleOrDefault(f => f.FollowerId == followerId && f.FolloweeId == followeeId);
        }
    }
}