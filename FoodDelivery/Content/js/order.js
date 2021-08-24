$(document).ready(function () {
    $("#order_now").click(function (e) {
        e.preventDefault();
        var food_id = document.getElementById("order_food").value;
        var user_id = document.getElementById("logged_in_user");
        var username = "";
        if (user_id != null) {
            username = user_id.innerText.replace('Hi ', '');
        }
        console.log(username);
        $.ajax({
            type: "POST",
            url: '/Food/Order',
            data: {
                food_id: food_id,
                username: username
            },
            success: function (response) {
                console.log(response);
                $("html").html(response);
                //$(".result").html(response);
            }
        });
    });
});