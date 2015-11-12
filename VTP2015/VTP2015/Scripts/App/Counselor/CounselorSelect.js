$(document).ready(function () {
    if (!$("#btnGreen").hasClass("btn-success")) {
        $(".panel-success").parent().addClass("hidden");
        $(".success").addClass("hidden");
    }
    if (!$("#btnOrange").hasClass("btn-warning")) {
        $(".panel-warning").parent().addClass("hidden");
        $(".warning").addClass("hidden");
    }
    if (!$("#btnRed").hasClass("btn-danger")) {
        $(".panel-danger").parent().addClass("hidden");
        $(".danger").addClass("hidden");
    }
});

function toggle(panel) {
    if (panel.hasClass("hidden")) {
        panel.removeClass("hidden");
    } else {
        panel.addClass("hidden");
    }
}

function toggleGreen() {
    toggle($(".panel-success").parent());
    toggle($(".success"));
}

function toggleOrange() {
    toggle($(".panel-warning").parent());
    toggle($(".warning"));
}

function toggleRed() {
    toggle($(".panel-danger").parent());
    toggle($(".danger"));
}