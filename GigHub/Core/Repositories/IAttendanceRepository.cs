using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(string attendeeId, int gigId);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
    }
}