using GigHub.Core.DTOs;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using GigHub.Core;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendanceController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            var existentAttendance = _unitOfWork.Attendances.GetAttendance(userId, dto.GigId);

            if (existentAttendance != null)
            {
                return BadRequest("The attendance already exists!");
            }

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _unitOfWork.Attendances.Add(attendance);

            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult RemoveAttendance(int id)
        {
            var attendance = _unitOfWork.Attendances.GetAttendance(User.Identity.GetUserId(), id);

            if (attendance == null)
                return NotFound();

            _unitOfWork.Attendances.Remove(attendance);

            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
