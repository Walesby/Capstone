﻿$(document).ready(function () {
    var novelId;
    var scoreTd;
    var progressTd;
    var maxProgress;
    $(".editList").click(function () {
        novelId = $(this).val();
        scoreTd = $(this).closest('td').prev().prev();
        progressTd = $(this).closest('td').prev();
        maxProgress = progressTd.html().split("/");
        maxProgress = maxProgress[1].trim();
        $("#userrating option:first").prop('selected', true);
        $("#userprogress").attr({"max": maxProgress, "min": 0 }).val("");
    });

    $("#submitupdate").click(function () {
        var userRating = $("#userrating").children("option:selected").val();
        var userProgress = $("#userprogress").val();
        var completeStatus = 2;

        var novelList = {
            UserRating: userRating,
            UserProgress: userProgress,
            NovelItemId: novelId,
            CompleteStatus: completeStatus
        }
        var jsonNovelList = JSON.stringify(novelList);

        $.ajax({
            url: "https://localhost:44319/api/crud/putusernovellist",
            type: "PUT",
            contentType: "application/json",
            data: jsonNovelList,
            success: function (response) {
                scoreTd.html(userRating);
                progressTd.html(userProgress + " / " + maxProgress);
            }
        });
    });

    $(".removeList").click(function () {
        var novelId = $(this).val();
        $(this).closest('tr').remove();
        $.ajax({
            url: "https://localhost:44319/api/crud/deleteusernovellist/" + novelId,
            type: "DELETE",
            contentType: "application/json",
            success: function (response) {
            }
        });
    });
});