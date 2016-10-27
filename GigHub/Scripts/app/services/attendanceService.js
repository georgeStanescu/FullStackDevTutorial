var AttendanceService = function () {
    var createAttendance = function (gigId, onDone, onFail) {
        $.post("/api/attendance", { "gigId": gigId })
           .done(onDone)
           .fail(onFail);
    };

    var deleteAttendance = function (gigId, onDone, onFail) {
        $.ajax({
            url: "/api/attendance/" + gigId,
            method: "DELETE"
        })
        .done(onDone)
        .fail(onFail)
    };

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}();