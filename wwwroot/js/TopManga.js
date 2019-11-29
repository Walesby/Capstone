$(document).ready(function () {
    $(".addmanga").click(function () {
        var mangaId = $(this).attr("value");
        var userRating = 0;
        var userProgress = 0;
        var userViewingStatus = 2;
        var addToList = {
            MangaItemId: mangaId,
            UserRating: userRating,
            UserProgress: userProgress,
            CompleteStatus: userViewingStatus
        }
        var jsonAddToList = JSON.stringify(addToList);
        var closestTd = $(this).closest('td');
        $.ajax({
            url: "https://localhost:44319/api/crud/postusermangalist",
            type: "POST",
            contentType: "application/json",
            data: jsonAddToList,
            success: function (response) {
                closestTd.html("Reading");
            }
        });
    });
});