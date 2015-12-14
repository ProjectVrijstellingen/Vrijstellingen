$(document).ready(function () {
    if (!$("#btnGreen").hasClass("btn-success")) {
        $(".panel-success-vtp").parent().addClass("hidden");
        $(".success").addClass("hidden");
    }
    if (!$("#btnOrange").hasClass("btn-warning")) {
        $(".panel-warning-vtp").parent().addClass("hidden");
        $(".warning").addClass("hidden");
    }
    if (!$("#btnRed").hasClass("btn-danger")) {
        $(".panel-danger-vtp").parent().addClass("hidden");
        $(".danger").addClass("hidden");
    }

    $(".badge-success").text($("#blok").find(".panel-success-vtp").length);
    $(".badge-warning").text($("#blok").find(".panel-warning-vtp").length);
    $(".badge-danger").text($("#blok").find(".panel-danger-vtp").length);

    //cookies
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

function toggle(panel) {
    if (panel.hasClass("hidden")) {
        panel.removeClass("hidden");
    } else {
        panel.addClass("hidden");
    }
}

function toggleGreen() {
    toggle($(".panel-success-vtp").parent());
    toggle($(".success"));
}

function toggleOrange() {
    toggle($(".panel-warning-vtp").parent());
    toggle($(".warning"));
}

function toggleRed() {
    toggle($(".panel-danger-vtp").parent());
    toggle($(".danger"));
}