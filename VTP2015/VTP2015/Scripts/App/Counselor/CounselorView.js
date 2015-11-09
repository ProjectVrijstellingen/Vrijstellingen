var type = document.cookie.split('=')[1];

$(document).ready(function () {
    if (document.cookie.indexOf("type") < 0) {
        createCookie("type", "blok");
    }

    type === "blok" ? showBlok() : showList();
});

function createCookie(name, value) {
    var expiration_date = new Date();
    expiration_date.setFullYear(expiration_date.getFullYear() + 1);
    document.cookie = name + "=" + value + "; expires=" + expiration_date + "; path=/";
    type = document.cookie.split('=')[1];
}

function showBlok() {
    $('#blok').removeClass('hidden');
    $('#lijst').addClass('hidden');
    $('#rbList').removeClass('active');
    $('#rbBlok').addClass('active');
}

function showList() {
    $('#lijst').removeClass('hidden');
    $('#blok').addClass('hidden');
    $('#rbBlok').removeClass('active');
    $('#rbList').addClass('active');
}

function onBlok() {
    createCookie("type", "blok");
    showBlok();
}

function onList() {
    createCookie("type", "list");
    showList();
}