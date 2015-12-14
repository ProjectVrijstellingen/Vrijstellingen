"option strict";

var files = [];
var type = document.cookie.split("=")[1];

$(document).ready(function () {

    appendFilesToArray();

    $("#nameSearchQuery").keyup(searchFilesForName);

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

    console.log("filesId's:");
    var fileIds = [];
    $(files).find(".btnShowOverview").each(function (index, value) {
        fileIds.push(parseInt($(value).data("fileid")));
    });
    console.log(fileIds);

    loadFiles(fileIds);
});

function showFileDetails(sender) {

    $("#overview").addClass("hide");

    var file = {
        fileId: $(sender.currentTarget).data("fileid"),
        name: $(sender.currentTarget).data("name")
    }

    selectFileById(file);
}

var searchFilesForName = function () {
    $.each(files, function (key, value) {
        var name = $(value).data("name");
        var prename = $(value).data("firstname");

        var control = $("#nameSearchQuery");

        if (name != undefined) {
            if (searchQueryContains(name, control) || searchQueryContains(prename, control) || searchQueryContains(name + " " + prename, control) || searchQueryContains(prename + " " + name, control)) {
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

var searchQueryContains = function (string, control) {
    return string.toLowerCase().indexOf($(control).val().toLowerCase()) >= 0;
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

$("#select-education").on("change", function () {
    $.ajax({
        url: "Counselors/ChangeEducation",
        data: { opleiding: $("#select-education option:selected").text() },
        type: "POST",
        success: function (data) {
            location.reload();
        }
    });
});
