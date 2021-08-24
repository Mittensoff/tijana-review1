$(document).ready(function () {
    var lat = document.getElementById("Latitude");
    var lng = document.getElementById("Longitude");
    function getLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
        }
        else {
            lat.value = "";
            lng.value = "";
        }
    }

    function showPosition(position) {
        lat.value = position.coords.latitude;
        lng.value = position.coords.longitude;
    }
    getLocation();
});