$(document).ready(function () {
    $("#submitreview").click(function () {
        var userRating = $("#rating").children("option:selected").val();
        var userReview = $("#review").val();
        var mangaId = $("#mangaId").val();
        var review = {
            rating: userRating,
            review: userReview,
            mangaItemId: mangaId
        }
        var jsonReview = JSON.stringify(review);

        $.ajax({
            url: "https://localhost:44319/api/crud/postmangareview",
            type: "POST",
            contentType: "application/json",
            data: jsonReview,
            success: function (response) {
                $.ajax({
                    url: "https://localhost:44319/api/crud/getmangareviews/" + mangaId,
                    type: "GET",
                    contentType: "application/json",
                    success: function (response) {
                        var responseLength = response.usernameList.length;
                        for (var i = 0; i < responseLength; i++) {
                            addReview(response.reviewsList[i], `${i + 1}`, response.usernameList[i], response.userImageUrl[i]);
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
            var mangaId = $("#mangaId").val();
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
            $.ajax({
                url: "https://localhost:44319/api/crud/postusermangalist",
                type: "POST",
                contentType: "application/json",
                data: jsonAddToList,
                success: function (response) {
                }
            });
        }
        else if (addOrRemoveStatus == "Remove from list") {
            $("#addremovefromlist").html("Add to list");
            var mangaId = $("#mangaId").val();
            $.ajax({
                url: "https://localhost:44319/api/crud/deleteusermangalist/" + mangaId,
                type: "DELETE",
                success: function (response) {
                    console.log(response);
                }
            });
        }
    });

    $("#addremovefromfavorites").click(function () {
        var addOrRemoveStatus = $("#addremovefromfavorites").html();
        if (addOrRemoveStatus == "Make favorite") {
            $("#addremovefromfavorites").html("Remove favorite");
            var mangaId = $("#mangaId").val();

            $.ajax({
                url: "https://localhost:44319/api/crud/putfavoritemanga/" + mangaId,
                type: "PUT",
                contentType: "application/json",
                success: function (response) {
                }
            });
        }

        else if (addOrRemoveStatus == "Remove favorite") {
            $("#addremovefromfavorites").html("Make favorite");
            $.ajax({
                url: "https://localhost:44319/api/crud/putfavoritemanga/" + 0,
                type: "PUT",
                success: function (response) {
                }
            });
        }
    });
});

function addReview(review, divId, username, profileImage) {
    var date = new Date(review.postDate);
    var month = (date.getMonth() + 1);
    var day = '/' + date.getDate();
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var seconds = date.getSeconds();
    var amOrPm = 'PM';
    if (hours % 12 === hours) {
        amOrPm = 'AM';
    }
    else {
        hours = hours % 12;
    }
    if (minutes < 10) {
        minutes = '0' + minutes;
    }
    if (seconds < 10) {
        seconds = '0' + seconds;
    }
    var time = hours + ':' + minutes + ':' + seconds + ' ' + amOrPm;
    var year = '/' + date.getFullYear();
    if (month.length < 2) {
        month = '0' + month;
    }
    if (day.length < 2) {
        month = '0' + month;
    }
    var fullDate = month + day + year + ' ' + time;
    $(`#review${divId}`).css('visibility', 'visible');
    $(`#reviewuser${divId}`).html(`<b>User: </b><a href="/Profile/UserProfile?username=${username}">${username}</a>`);
    $(`#reviewrating${divId}`).html(`<b>Rating: </b> ${review.rating} / 10`);
    $(`#reviewdate${divId}`).html(`<b>Date: </b> ${fullDate}`);
    $(`#reviewbody${divId}`).html(`${review.review}`);
    $(`#reviewimage${divId}`).attr("src", profileImage);
}