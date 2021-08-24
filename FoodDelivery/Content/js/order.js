$(document).ready(function () {
    $("#order_now").click(function (e) {
        e.preventDefault();
        document.getElementById("no_user").style.visibility = 'hidden';
        var food_id = document.getElementById("order_food").value;
        if (document.getElementById("logged_in_user") == null) {
            document.getElementById("no_user").style.visibility = 'visible';
        }
        else {
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
                    $("html").html(response);
                    //$(".result").html(response);
                }
            });
        }
    });
});