var saveTime = 2000; //in ms
var partimdetails = [];
var timer;

function Return() {
    var shown = $("#aanvraagDetail").find(".nothidden");
    shown.addClass("hide");
    shown.removeClass("nothidden");
    $(document.getElementsByClassName("active")).removeClass("active");
    toFirstView();
}

function toFirstView() {
    $("#aangevraagdePartimsColumn").removeClass("col-md-4");
    $("#aangevraagdePartimsColumn").addClass("col-md-6");
    $("#bewijzenColumn").addClass("hide");
    $("#aanvraagDetail").addClass("hide");
    $("#beschikbarePartimsColumn").removeClass("hide");
}

function toSecondView() {
    $("#beschikbarePartimsColumn").addClass("hide");
    $("#aangevraagdePartimsColumn").removeClass("col-md-6");
    $("#aangevraagdePartimsColumn").addClass("col-md-4");
    $("#bewijzenColumn").removeClass("hide");
    $("#aanvraagDetail").removeClass("hide");
}

function hideShown() {
    var shown = $("#aanvraagDetail").find(".nothidden");
    shown.addClass("hide");
    $(document.getElementsByClassName("active")).removeClass("active");
    shown.removeClass("nothidden");
}

function changed(partimdetail) {
    clearTimeout(timer);
    var cons = false;
    $.each(partimdetails, function() {
        if ($(this).data("code") === $(partimdetail).data("code")) cons = true;
    });
    if (!cons) partimdetails.push($(partimdetail));
    timer = setTimeout(savePartimdetails, saveTime);
}

function addRequest(code) {
    var dossierId = document.URL.split("/")[document.URL.split("/").length - 1];
    var viewModel = {
        FileId: dossierId,
        Code: code
    };
    $.ajax({
        url: $("#aanvraagDetail").data("url2"),
        data: $.toDictionary(viewModel),
        type: "POST",
        success: function (data) {
            if (data === "Fake!") location.reload();
            $("#aanvraagDetail").find("[data-code=\"" + code + "\"]").attr("data-requestid", data);
            $("#aanvraagDetail").find("[data-code=\"" + code + "\"]").attr("id", data);
        }
    });
}

function savePartimdetails() {
    clearTimeout(timer);
    var bewijzen = [];
    var dossierId = document.URL.split("/")[document.URL.split("/").length - 1];
    $.each(partimdetails, function () {
        var that = this;
        var requestid = $(this).data("requestid");
        console.log(requestid);
        var argumentatie = $(this).find("#argumentatie").val();
        $(this).find("li").each(function () {
            bewijzen.push($(this).data("bewijsid"));
        });

        var viewModel = {
            FileId: dossierId,
            RequestId: requestid,
            Argumentation: argumentatie,
            Evidence: bewijzen
        };
        $.ajax({
            url: $("#aanvraagDetail").data("url"),
            data: $.toDictionary(viewModel),
            type: "POST",
            success: function (data) {
            }
        });
    });
    partimdetails = [];
}

function addBewijs(that) {
    var newParent = $("#aanvraagDetail").find(".nothidden").find("#bewijzen");

    var clon = $(that).clone();
    clon.removeAttr("id");
    clon.attr("draggable", "false");
    clon.find(".hide").removeClass("hide");
    clon.find(".glyphicon-plus").remove();
    $(clon).appendTo($(newParent));
    changed($("#aanvraagDetail").find(".nothidden"));
}

$(document).on("click", ".glyphicon-minus", function () {
    $(this).parent().remove();
    changed($("#aanvraagDetail").find(".nothidden"));
});

$(document).on("keyup", "#argumentatie", function() {
    changed($("#aanvraagDetail").find(".nothidden"));
});

$(document).on("click", ".partim", function () {
    console.log("partim clicked");
    if (isIngediend()) return;
    var beschikbarePartims = document.getElementById("beschikbarePartimsColumn");
    var parentDiv = $(this).parent().parent()[0];
    var moduleid = $(parentDiv).data("moduleid");
    var supercode = $(this).data("supercode");
    var moduleNaam = $(parentDiv).find(".h4").text();
    var semesterDiv = $(this).closest(".semesterDiv");
    var newParent;

    if ($.contains(beschikbarePartims, this)) {
        var semester = $(semesterDiv).data("semester");
        if($("#aangevraagdePartimsColumn div[data-semester=\"" + semester + "\"]").length === 0)
            $("#aangevraagdePartimsColumn .panel-body").html($("#aangevraagdePartimsColumn .panel-body").html() + "<div class=\"semesterDiv\" data-semester=\"" + semester + "\"><div class=\"semester\">" + $(semesterDiv).find(".semester").html() + "</div><div class=\"nohide\"></div></div>");
        var newSemesterDiv = $("#aangevraagdePartimsColumn div[data-semester=\"" + semester + "\"]");
        if ($(newSemesterDiv).find("div[data-moduleid=\"" + moduleid + "\"]").length === 0) {
            $(newSemesterDiv).children("div:last").html($(newSemesterDiv).children("div:last").html() + "<div data-moduleid=\"" + moduleid + "\"><span class=\"h4\">" + moduleNaam + "</span><ul class=\"list-group\"></ul></div>");
        }

        newParent = $(newSemesterDiv).children("div:last").find("div[data-moduleid=\"" + moduleid + "\"] ul");
        $(this).detach();
        $(newParent).parent().removeClass("hide");
        $(this).appendTo($(newParent));
        $(this).find(".btn").removeClass("hide");

        $(parentDiv).children("span:first").removeClass("module");
        if ($(parentDiv).find("ul").children().length === 0) $(parentDiv).remove();
        if ($(semesterDiv).children("div:last").children().length === 0) $(semesterDiv).remove();

        var clon = $("#dummy").clone();
        clon.find("h3").text(moduleNaam);
        clon.find("h4").text("");
        clon.find("h4").append($(this).children("span:first").clone());
        clon.attr("data-code", supercode);
        addRequest(supercode);
        clon.appendTo("section");
    } else {
        if ($(this).hasClass("active")) {
            Return();
            return;
        } else {
            hideShown();
            var show = $("#aanvraagDetail").find("[data-code=\"" + supercode + "\"]");
            $(this).addClass("active");
            show.removeClass("hide");
            show.addClass("nothidden");
        }
        if (!$(beschikbarePartims).hasClass("hide")) toSecondView();
    }
    //($(".tooltip ").addClass("hide"));
    $(".tooltip ").remove();
    $('[data-toggle="tooltip"]').tooltip();
});

$(document).on("click", ".module", function () {
    console.log("module clicked");
    if (isIngediend()) return;
    var semesterDiv = $(this).closest(".semesterDiv");
    var beschikbarePartims = document.getElementById("beschikbarePartimsColumn");
    var parentDiv = $(this).parent()[0];
    var moduleid = $(parentDiv).data("moduleid");
    console.log(moduleid);
    var moduleNaam = $(parentDiv).find(".h4").text();

    if ($.contains(beschikbarePartims, this)) {
        var semester = $(semesterDiv).data("semester");
        if ($("#aangevraagdePartimsColumn div[data-semester=\"" + semester + "\"]").length === 0)
            $("#aangevraagdePartimsColumn .panel-body").html($("#aangevraagdePartimsColumn .panel-body").html() + "<div class=\"semesterDiv\" data-semester=\"" + semester + "\"><div class=\"semester\">" + $(semesterDiv).find(".semester").html() + "</div><div class=\"nohide\"></div></div>");
        var newSemesterDiv = $("#aangevraagdePartimsColumn div[data-semester=\"" + semester + "\"]");

        $(parentDiv).find("ul").addClass("hide");

        $(parentDiv).detach();
        $(newSemesterDiv).children("div:last").append($(parentDiv));
        $(parentDiv).find(".btn").removeClass("hide");

        if ($(semesterDiv).children("div:last").children().length === 0) $(semesterDiv).remove();

        var clon = $("#dummy").clone();
        clon.find("h3").text(moduleNaam);
        clon.find("h4").text("");
        clon.attr("data-code", moduleid);
        addRequest(moduleid);
        clon.appendTo("section");
    } else {
        if ($(this).hasClass("active")) {
            Return();
            return;
        } else {
            hideShown();
            var show = $("#aanvraagDetail").find("[data-code=\"" + moduleid + "\"]");
            $(this).addClass("active");
            show.removeClass("hide");
            show.addClass("nothidden");
        }
        if (!$(beschikbarePartims).hasClass("hide")) toSecondView();
    }
});

$(document).on("click", ".glyphicon-plus", function () {
    var that = $(this).parent();
    addBewijs(that);
});

$(document).on("click", ".glyphicon-remove", function (e) {
    console.log("remove Request");
    e.stopPropagation();
    savePartimdetails();
    var that = $(this).parent();
    var semesterDiv = $(that).closest(".semesterDiv");
    var aanvraagId, moduleid, moduleNaam, aanvraag, parentDiv, semester, newSemesterDiv;
    if ($(that).is("li")) {
        console.log("partim...");
        parentDiv = $(that).parent().parent()[0];
        moduleid = $(parentDiv).data("moduleid");
        moduleNaam = $(parentDiv).find(".h4").text();
        var supercode = $(that).data("supercode");
        console.log(moduleNaam);
        semester = $(semesterDiv).data("semester");
        if ($("#beschikbarePartimsColumn div[data-semester=\"" + semester + "\"]").length === 0)
            $("#beschikbarePartimsColumn .panel-body").html($("#beschikbarePartimsColumn .panel-body").html() + "<div class=\"semesterDiv\" data-semester=\"" + semester + "\"><div class=\"semester\">" + $(semesterDiv).find(".semester").html() + "</div><div class=\"nohide\"></div></div>");
        newSemesterDiv = $("#beschikbarePartimsColumn div[data-semester=\"" + semester + "\"]");
        if ($(newSemesterDiv).find("div[data-moduleid=\"" + moduleid + "\"]").length === 0) {
            $(newSemesterDiv).children("div:last").html($(newSemesterDiv).children("div:last").html() + "<div data-moduleid=\"" + moduleid + "\"><span class=\"h4\">" + moduleNaam + "</span><ul class=\"list-group\"></ul></div>");
        }

        var newParent = $(newSemesterDiv).children("div:last").find("div[data-moduleid=\"" + moduleid + "\"] ul");
        aanvraag = $("#aanvraagDetail").find("[data-code=\"" + supercode + "\"]");
        aanvraagId = $(aanvraag).data("requestid");

        $(that).detach();
        $(that).removeClass("active");
        $(that).appendTo($(newParent));
        $(that).find(".btn").addClass("hide");

        if ($(parentDiv).find("ul").children().length === 0) {
            parentDiv.remove();
            $(newSemesterDiv).find("div[data-moduleid=\"" + moduleid + "\"]").children("span:first").addClass("module");
        }
        if ($(semesterDiv).children("div:last").children().length === 0) $(semesterDiv).remove();
        if ($(aanvraag).hasClass("nothidden")) toFirstView();

        aanvraag.remove();
    } else {
        console.log("module...");
        semester = $(semesterDiv).data("semester");
        if ($("#beschikbarePartimsColumn div[data-semester=\"" + semester + "\"]").length === 0)
            $("#beschikbarePartimsColumn .panel-body").html($("#beschikbarePartimsColumn .panel-body").html() + "<div class=\"semesterDiv\" data-semester=\"" + semester + "\"><div class=\"semester\">" + $(semesterDiv).find(".semester").html() + "</div><div class=\"nohide\"></div></div>");
        newSemesterDiv = $("#beschikbarePartimsColumn div[data-semester=\"" + semester + "\"]");
        parentDiv = $(that).parent()[0];
        moduleid = $(that).data("moduleid");
        moduleNaam = $(parentDiv).find(".h4").text();
        aanvraag = $("#aanvraagDetail").find("[data-code=\"" + moduleid + "\"]");
        aanvraagId = $(aanvraag).data("requestid");
        $(parentDiv).find("ul").removeClass("hide");


        $(that).detach();
        $(that).removeClass("active");
        $(that).appendTo($(newSemesterDiv).children("div:last"));
        $(that).find(".btn").addClass("hide");

        if ($(semesterDiv).children("div:last").children().length === 0) $(semesterDiv).remove();

        if ($(aanvraag).hasClass("nothidden")) toFirstView();
        aanvraag.remove();
    }
    var fileId = document.URL.split("/")[document.URL.split("/").length - 1];
    $.ajax({
        url: $("#aangevraagdePartimsColumn").data("url"),
        data: {
            fileId: fileId,
            requestId: aanvraagId
        },
        type: "POST",
        success: function (data) {
            console.log(data);
        },
        error: function (data) {
            console.log(aanvraagId);
            console.log(data);
        }
    });
});

$(document).ready(function () {
    $("bewijzenColumn").addClass("hide");
    $("[data-toggle=\"tooltip\"]").tooltip();
});

$(document).on("click", "#btnIndienen", function () {
    console.log("Dossier indienen");
    savePartimdetails();
    var fileId = document.URL.split("/")[document.URL.split("/").length - 1];
    $.ajax({
        url: $("#beschikbarePartimsColumn").data("url"),
        data: {
            fileId: fileId
        },
        type: "POST",
        success: function (data) {
            if(data === "Submitted!") location.reload();
        }
    });
});

$(document).on("click", ".semester", function() {
    var list = $(this).parent().children()[1];
    var glyph = $(this).children()[0];
    if ($(list).hasClass("hide")) {
        $(list).removeClass("hide");
        $(list).addClass("nohide");
        $(glyph).removeClass("glyphicon-triangle-right");
        $(glyph).addClass("glyphicon-triangle-bottom");
    } else {
        $(list).removeClass("nohide");
        $(list).addClass("hide");
        $(glyph).removeClass("glyphicon-triangle-bottom");
        $(glyph).addClass("glyphicon-triangle-right");
    }
});

function isIngediend() {
    var string = $("#aangevraagdePartimsColumn .panel .panel-heading").find("span").text();
    return string === "Ingediend";
}
