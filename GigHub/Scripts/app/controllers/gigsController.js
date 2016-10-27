var GigsController = function (attendanceService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (event) {
        button = $(event.target);

        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, onDone, onFail);
        else
            attendanceService.deleteAttendance(gigId, onDone, onFail);
    };

    var onFail = function () {
        alert("Something failed");
    };

    var onDone = function () {
        var text = (button.text() === "Going") ? "Going?" : "Going";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    return {
        init: init
    }
}(AttendanceService);