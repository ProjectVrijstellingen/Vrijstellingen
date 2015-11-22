﻿"option strict";

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

    $("#spnFinishedFiles").text($("#overviewBlok").find(".panel-success-vtp").length);
    $("#spnBusyFiles").text($("#overviewBlok").find(".panel-warning-vtp").length);
    $("#spnNewFiles").text($("#overviewBlok").find(".panel-danger-vtp").length);
    
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
    $.each(files, function (key, value) {
        var name = $(value).data("name");
        var prename = $(value).data("firstname");
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
    $(".d").each(function () {
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
    $("#overviewBlok").removeClass("hidden");
    $("#overviewList").addClass("hidden");
    $("#rbList").removeClass("active");
    $("#rbBlok").addClass("active");
}

function showList() {
    $("#overviewList").removeClass("hidden");
    $("#overviewBlok").addClass("hidden");
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