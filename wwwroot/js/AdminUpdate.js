$(document).ready(function () {
    $("#updateitem").click(function () {
        var collectionType = $("#collectiontype").children("option:selected").val();
        var id = $("#id").val();
        var title = $("#title").val();
        var episodeCount = $("#episodes").val();
        var status = $("#status").children("option:selected").val();
        var seasonPremiered = $("#seasonpremiered").children("option:selected").val();
        var year = $("#yearpremiered").val();
        var premiered = seasonPremiered + " " + year;
        var source = $("#source").children("option:selected").val();
        var ageRating = $("#agerating").children("option:selected").val();
        var imageUrl = $("#imageurl").val();
        var synopsis = $("#synopsis").val();
        var animeItem = {
            id: id,
            title: title,
            type: collectionType,
            episodecount: episodeCount,
            status: status,
            premiered: premiered,
            source: source,
            recommendedage: ageRating,
            imagepath: imageUrl,
            synopsis: synopsis
        }
        var jsonAnimeItem = JSON.stringify(animeItem);

        if (collectionType == "Anime") {
            $.ajax({
                url: "https://localhost:44319/api/crud/putanime",
                type: "PUT",
                contentType: "application/json",
                data: jsonAnimeItem,
                success: function (response) {
                    console.log(response);
                }
            });
        }
    });
});