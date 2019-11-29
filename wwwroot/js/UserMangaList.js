$(document).ready(function () {
    var mangaId;
    var scoreTd;
    var progressTd;
    var maxProgress;
    $(".editList").click(function () {
        mangaId = $(this).val();
        scoreTd = $(this).closest('td').prev().prev();
        progressTd = $(this).closest('td').prev();
        maxProgress = progressTd.html().split("/");
        maxProgress = maxProgress[1].trim();
        $("#userrating option:first").prop('selected', true);
        $("#userprogress").attr({ "max": maxProgress, "min": 0 }).val("");
    });

    $("#submitupdate").click(function () {
        var userRating = $("#userrating").children("option:selected").val();
        var userProgress = $("#userprogress").val();
        var completeStatus = 2;

        var mangaList = {
            UserRating: userRating,
            UserProgress: userProgress,
            MangaItemId: mangaId,
            CompleteStatus: completeStatus
        }
        var jsonMangaList = JSON.stringify(mangaList);

        $.ajax({
            url: "https://localhost:44319/api/crud/putusermangalist",
            type: "PUT",
            contentType: "application/json",
            data: jsonMangaList,
            success: function (response) {
                scoreTd.html(userRating);
                progressTd.html(userProgress + " / " + maxProgress);
            }
        });
    });

    $(".removeList").click(function () {
        var mangaId = $(this).val();
        $(this).closest('tr').remove();
        $.ajax({
            url: "https://localhost:44319/api/crud/deleteusermangalist/" + mangaId,
            type: "DELETE",
            contentType: "application/json",
            success: function (response) {
            }
        });
    });
});