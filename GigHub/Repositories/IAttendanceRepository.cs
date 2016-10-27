﻿using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(string attendeeId, int gigId);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
    }
}