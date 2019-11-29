$(document).ready(function () {
    $(".addnovel").click(function () {
        var novelId = $(this).attr("value");
        var userRating = 0;
        var userProgress = 0;
        var userViewingStatus = 2;
        var addToList = {
            NovelItemId: novelId,
            UserRating: userRating,
            UserProgress: userProgress,
            CompleteStatus: userViewingStatus
        }
        var jsonAddToList = JSON.stringify(addToList);
        var closestTd = $(this).closest('td');
        console.log(jsonAddToList);
        $.ajax({
            url: "https://localhost:44319/api/crud/postusernovellist",
            type: "POST",
            contentType: "application/json",
            data: jsonAddToList,
            success: function (response) {
                closestTd.html("Reading");
            }
        });
    });
});