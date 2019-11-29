$(document).ready(function () {
    var animeId;
    var scoreTd;
    var progressTd;
    var maxProgress;
    $(".editList").click(function () {
        animeId = $(this).val();
        scoreTd = $(this).closest('td').prev().prev();
        progressTd = $(this).closest('td').prev();
        maxProgress = progressTd.html().split("/");
        maxProgress = maxProgress[1].trim();
        $("#userrating option:first").prop('selected', true);
        $("#userprogress").attr({"max":maxProgress, "min": 0}).val("");
    });

    $("#submitupdate").click(function () {       
        var userRating = $("#userrating").children("option:selected").val();
        var userProgress = $("#userprogress").val();
        var completeStatus = 2;
        var animeList = {
            UserRating: userRating,
            UserProgress: userProgress,
            AnimeItemId: animeId,
            CompleteStatus: completeStatus
        }
        var jsonAnimeList = JSON.stringify(animeList);

        $.ajax({
            url: "https://localhost:44319/api/crud/putuseranimelist",
            type: "PUT",
            contentType: "application/json",
            data: jsonAnimeList,
            success: function (response) {
                scoreTd.html(userRating);
                progressTd.html(userProgress + " / " + maxProgress);
            }
        });
    });

    $(".removeList").click(function () {
        var animeId = $(this).val();
        $(this).closest('tr').remove();
        $.ajax({
            url: "https://localhost:44319/api/crud/deleteuseranimelist/" + animeId,
            type: "DELETE",
            contentType: "application/json",
            success: function (response) {
            }
        });
    });
});