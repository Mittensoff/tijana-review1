$(document).ready(function () {
    $("#order_now").click(function (e) {
        e.preventDefault();
        var food_id = document.getElementById("order_food").value;
        var user_text = document.getElementById("logged_in_user").innerText;
        var username = user_text.replace('Hi ', '');
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
                //$(".result").html(response);
            }
        });
    });
});