$(document).ready(function () {
    $("#addremovefriend").click(function () {
        var addOrRemove = $(this).html();
        var userId = $(this).val();
        var newFriendObject = {
            Id: userId
        }
        var jsonUserId = JSON.stringify(newFriendObject);

        if (addOrRemove === "Remove Friend") {
            $.ajax({
                url: "https://localhost:44319/api/crud/deletefriend",
                type: "DELETE",
                contentType: "application/json",
                data: jsonUserId,
                success: function (response) {
                    $("#addremovefriend").html("Add Friend");
                }
            });
        }

        else if (addOrRemove === "Add Friend") {
            $.ajax({
                url: "https://localhost:44319/api/crud/addfriend",
                type: "POST",
                contentType: "application/json",
                data: jsonUserId,
                success: function (response) {
                    $("#addremovefriend").html("Remove Friend");
                }
            });

        }

    });
    $('.userupdates').lightSlider({
        item: 4.5,
        loop: true,
        slideMove: 1,
        slideMargin: 10,
        easing: 'cubic-bezier(0.25, 0, 0.25, 1)',
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    item: 1.8,
                    slideMove: 1,
                    slideMargin: 10,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    item: 3,
                    slideMove: 1,
                    slideMargin: 0,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    item: 3,
                    slideMove: 1
                }
            },
            {
                breakpoint: 576,
                settings: {
                    item: 3,
                    slideMove: 1
                }
            }
        ]
    });
});