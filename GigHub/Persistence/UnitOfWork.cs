using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork
    {

        private readonly ApplicationDbContext _context;

        private readonly AttendanceRepository _attendanceRepository;
        private readonly GigRepository _gigRepository;
        private readonly FollowingRepository _followingRepository;
        private readonly GenreRepository _genreRepository;

        public GigRepository Gigs { get; private set; }

        public AttendanceRepository Attendances { get; private set; }

        public FollowingRepository Followings { get; private set; }

        public GenreRepository Genres { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            Attendances = new AttendanceRepository(_context);
            Gigs = new GigRepository(_context);
            Followings = new FollowingRepository(_context);
            Genres = new GenreRepository(_context);

            _context = context;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}