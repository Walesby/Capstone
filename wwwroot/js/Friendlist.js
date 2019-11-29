$(document).ready(function () {
    $(".removelist").click(function () {
        var userId = $(this).val();
        var newFriendObject = {
            Id: userId
        }
        var jsonUserId = JSON.stringify(newFriendObject);
        $(this).closest('tr').remove();
        $.ajax({
            url: "https://localhost:44319/api/crud/deletefriend",
            type: "DELETE",
            contentType: "application/json",
            data: jsonUserId,
            success: function (response) {
                
            }
        });
    });
});