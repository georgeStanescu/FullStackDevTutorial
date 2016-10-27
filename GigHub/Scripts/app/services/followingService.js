var FollowingService = function () {
    var createFollowing = function (userId, onDone, onFail) {
        $.post("/api/following", { "followeeId": userId })
            .done(onDone)
            .fail(onFail);
    };

    var deleteFollowing = function (followeeId, onDone, onFail) {
        $.ajax({
            url: "/api/following/" + followeeId,
            method: "DELETE"
        })
        .done(onDone)
        .fail(onFail)
    };

    return {
        createFollowing: createFollowing,
        deleteFollowing: deleteFollowing
    }
}();