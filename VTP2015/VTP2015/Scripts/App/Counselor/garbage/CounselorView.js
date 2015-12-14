var type = document.cookie.split("=")[1];

$(document).ready(function () {
    if (document.cookie.indexOf("type") < 0) {
        createCookie("type", "blok");
    }

    type === "blok" ? showBlok() : showList();

    $(".rechts").hover(
        function () {
            $(this).children(":first").fadeTo(400, 1);
        }, function () {
            $(this).children(":first").fadeTo(400, 0);
        }
    );

});

function createCookie(name, value) {
    var expirationDate = new Date();
    expirationDate.setFullYear(expirationDate.getFullYear() + 1);
    document.cookie = name + "=" + value + "; expires=" + expirationDate + "; path=/";
    type = document.cookie.split("=")[1];
}

function showBlok() {
    $("#blok").removeClass("hidden");
    $("#lijst").addClass("hidden");
    $("#rbList").removeClass("active");
    $("#rbBlok").addClass("active");
}

function showList() {
    $("#lijst").removeClass("hidden");
    $("#blok").addClass("hidden");
    $("#rbBlok").removeClass("active");
    $("#rbList").addClass("active");
}

function onBlok() {
    createCookie("type", "blok");
    showBlok();
}

function onList() {
    createCookie("type", "list");
    showList();
}