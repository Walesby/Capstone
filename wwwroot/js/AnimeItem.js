$(document).ready(function () {
    $("#submitreview").click(function () {
        var userRating = $("#rating").children("option:selected").val();
        var userReview = $("#review").val();
        var animeId = $("#animeId").val();
        var review = {
            rating: userRating,
            review: userReview,
            animeItemId: animeId
        }
        var jsonReview = JSON.stringify(review); 

        $.ajax({
            url: "https://localhost:44319/api/crud/postreview",
            type: "POST",
            contentType: "application/json",
            data: jsonReview,
            success: function (response) {
                $.ajax({
                    url: "https://localhost:44319/api/crud/getreviews/" + animeId,
                    type: "GET",
                    contentType: "application/json",
                    success: function (response) {
                        var responseLength = response.usernameList.length;
                        for (var i = 1; i <= responseLength; i++) {
                            $(`#review${i}`).empty();
                            addReview(response.reviewsList[i - 1], `#review${i}`, response.usernameList[i - 1]);
                        }
                    }
                });
            }
        });
    });

    $("#addremovefromlist").click(function () {
        var addOrRemoveStatus = $("#addremovefromlist").html();
        if (addOrRemoveStatus == "Add to list") {
            $("#addremovefromlist").html("Remove from list");
            var animeId = $("#animeId").val();
            var userRating = 0;
            var userProgress = 0;
            var userViewingStatus = 2;
            var addToList = {
                AnimeItemId: animeId,
                UserRating: userRating,
                UserProgress: userProgress,
                CompleteStatus: userViewingStatus
            }

            var jsonAddToList = JSON.stringify(addToList);
            $.ajax({
                url: "https://localhost:44319/api/crud/postuseranimelist",
                type: "POST",
                contentType: "application/json",
                data: jsonAddToList,
                success: function (response) {
                    alert("Ye");
                }
            });
        }
        else if (addOrRemoveStatus == "Remove from list") {
            $("#addremovefromlist").html("Add to list");
            var animeId = $("#animeId").val();
            $.ajax({
                url: "https://localhost:44319/api/crud/deleteuseranimelist/" + animeId,
                type: "DELETE",
                success: function (response) {
                    console.log(response);
                }
            });
        }
    });
});

function addReview(review, divId, username) {
    $(`${divId}`).append("<div class=\"reviewinfo col-12\"></div>");
    $(`${divId}`).append("<div class=\"reviewbody col-12\"></div>");
    $(`${divId} > .reviewinfo`).append(`<a href="/Profile/User?username=${username}">${username}</a>`);
    $(`${divId} > .reviewinfo`).append(`<p>Date: ${review.postDate}`);
    $(`${divId} > .reviewinfo`).append(`<p>Rating: ${review.rating} / 10`);
    $(`${divId} > .reviewbody`).append(`<p>${review.review}`);
}