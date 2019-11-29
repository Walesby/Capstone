$(document).ready(function () {
    $(".addanime").click(function () {
        var animeId = $(this).attr("value");
        var userRating = 0;
        var userProgress = 0;
        var userViewingStatus = 2;
        var addToList = {
            AnimeItemId: animeId,
            UserRating: userRating,
            UserProgress: userProgress,
            CompleteStatus: userViewingStatus
        }
        var closestTd = $(this).closest('td');

        var jsonAddToList = JSON.stringify(addToList);
        $.ajax({
            url: "https://localhost:44319/api/crud/postuseranimelist",
            type: "POST",
            contentType: "application/json",
            data: jsonAddToList,
            success: function (response) {
                closestTd.html("Watching");
            }
        });
    });
});