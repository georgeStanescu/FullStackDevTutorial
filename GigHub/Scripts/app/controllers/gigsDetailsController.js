var GigsDetailsController = function (followingService) {
    var button;

    var init = function () {
        $(".js-toggle-follow").click(toggleFollowing);
    };

    var toggleFollowing = function (event) {
        button = $(event.target);

        var followeeId = button.attr("data-user-id")

        if (button.hasClass("btn-default"))
            followingService.createFollowing(followeeId, onDone, onFail);
        else
            followingService.deleteFollowing(followeeId, onDone, onFail);
        
    };

    var onFail = function () {
        alert("Something failed");
    };

    var onDone = function () {
        var text = (button.text() === "Follow") ? "Following" : "Follow";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    return {
        init: init
    }
}(FollowingService);