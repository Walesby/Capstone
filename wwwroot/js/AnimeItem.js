$(document).ready(function () {
    $("#submitreview").click(function () {
        var userRating = $("#rating").val();
        var userReview = $("#review").val();
        var animeId = $("#animeId").val();
        var review = {
            rating: userRating,
            review: userReview,
            animeItemId: animeId,
            userId: "aa82990c-d170-4b88-d95f-08d768947136"
        }
        var jsonReview = JSON.stringify(review); 
        console.log(review);


        $.ajax({
            url: "https://localhost:44319/api/crud/postreview",
            type: "POST",
            contentType: "application/json",
            data: jsonReview,
            success: function (response) {
                alert("Ye");
            }
        });
    });
});