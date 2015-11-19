"option strict";

var files = [];
var type = document.cookie.split("=")[1];

$(document).ready(function () {

    appendFilesToArray();

    $("#searchQuery").keyup(SearchQueryKeyupEventHandler);

    $(".btnShowOverview").click(function (sender) { showFileDetails(sender); });

    if (!$("#btnGreen").hasClass("btn-success")) {
        $(".panel-success-vtp").parent().addClass("hidden");
        $(".success").addClass("hidden");
    }
    if ($("#btnOrange").hasClass("btn-warning")) {
        $(".panel-warning-vtp").parent().addClass("hidden");
        $(".warning").addClass("hidden");
    }
    if ($("#btnRed").hasClass("btn-danger")) {
        $(".panel-danger-vtp").parent().addClass("hidden");
        $(".danger").addClass("hidden");
    }

    $(".badge-success").text($("#blok").find(".panel-success-vtp").length);
    $(".badge-warning").text($("#blok").find(".panel-warning-vtp").length);
    $(".badge-danger").text($("#blok").find(".panel-danger-vtp").length);
    
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

    $('[data-toggle="tooltip"]').tooltip();
});

function showFileDetails(sender) {

    $("#overview").addClass("hide");

    var file = {
        fileId: $(sender.currentTarget).data("fileid"),
        name: $(sender.currentTarget).data("name")
    }

    selectFileById(file);
}

var SearchQueryKeyupEventHandler = function () {
    console.log(files);

    $.each(files, function (key, value) {
        var name = $(value).data("name");
        var prename = $(value).data("prename");
        if (name != undefined) {
            if (searchQueryContains(name) || searchQueryContains(prename) || searchQueryContains(name + " " + prename) || searchQueryContains(prename + " " + name)) {
                $(value).show();
            } else {
                $(value).hide();
            }
        }
    });
};

var appendFilesToArray = function () {
    $(".file").each(function () {
        files.push(this);
    });
};

var searchQueryContains = function (string) {
    return string.toLowerCase().indexOf($("#searchQuery").val().toLowerCase()) >= 0;
};

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