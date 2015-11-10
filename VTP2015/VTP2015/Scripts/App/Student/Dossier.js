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
    $(partimdetail).find("#status").text("wachten...");
    var cons = false;
    $.each(partimdetails, function() {
        if ($(this).attr("id") === $(partimdetail).attr("id")) cons = true;
    });
    if (!cons) partimdetails.push($(partimdetail));
    timer = setTimeout(savePartimdetails, saveTime);
}

function savePartimdetails() {
    clearTimeout(timer);
    var bewijzen = [];
    var dossierId = document.URL.split("/")[document.URL.split("/").length - 1];
    $.each(partimdetails, function () {
        var that = this;
        var supercode = $(this).attr("id");
        var argumentatie = $(this).find("#argumentatie").val();
        $(this).find("li").each(function () {
            bewijzen.push($(this).data("bewijsid"));
        });

        var aanvraagViewModel = {
            dossierId: dossierId,
            supercode: supercode,
            argumentatie: argumentatie,
            bewijzen: bewijzen
        }
        $.ajax({
            url: $("#aanvraagDetail").data("url"),
            data: $.toDictionary(aanvraagViewModel),
            type: "POST",
            success: function (data) {
                $(that).find("#status").text(data);
            }
        });
    });
    partimdetails = [];
}

function addBewijs(that) {
    var newParent = $("#aanvraagDetail").find(".nothidden").find("ul");

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

$(document).on("keyup", ".form-control", function() {
    changed($("#aanvraagDetail").find(".nothidden"));
});

$(document).on("click", ".partim", function () {
    console.log("partim clicked");
    var beschikbarePartims = document.getElementById("beschikbarePartimsColumn");
    var aanvraagDetail = document.getElementById("aanvraagDetail");
    var parentDiv = $(this).parent().parent()[0];
    var moduleid = $(parentDiv).data("moduleid");
    var supercode = $(this).data("supercode");
    var moduleNaam = $(parentDiv).find(".h4").text();
    var newParent;

    if ($.contains(beschikbarePartims, this)) {
        if ($("#aangevraagdePartimsColumn div[data-moduleid=" + moduleid + "]").length === 0) $("#aangevraagdePartimsColumn .panel-body").html($("#aangevraagdePartimsColumn .panel-body").html() + "<div data-moduleid=\"" + moduleid + "\"><h4>" + moduleNaam + "</h4><ul class=\"list-group\"></ul></div>");

        newParent = $("#aangevraagdePartimsColumn div[data-moduleid=" + moduleid + "] ul");
        $(this).detach();
        $(newParent).parent().removeClass("hide");
        $(this).appendTo($(newParent));
        $(this).find(".btn").removeClass("hide");

        $(parentDiv).children("span:first").removeClass("module");
        if ($(parentDiv).find("ul").children().length === 0) $(parentDiv).remove();

        var clon = $("#dummy").clone();
        clon.find("h3").text(moduleNaam);
        clon.find("h4").text("");
        clon.find("h4").append($(this).children("span:first").clone());
        clon.attr("id", supercode);
        clon.appendTo("section");
        changed($(clon));
    } else {
        if ($(this).hasClass("active")) {
            Return();
            return;
        } else {
            hideShown();
            var show = $(aanvraagDetail).find("#" + $(this).data("supercode"));
            $(this).addClass("active");
            show.removeClass("hide");
            show.addClass("nothidden");
        }
        if (!$(beschikbarePartims).hasClass("hide")) toSecondView();
    }
    $('[data-toggle="tooltip"]').tooltip();
    
});

$(document).on("click", ".module", function () {
    console.log("module clicked");
    var beschikbarePartims = document.getElementById("beschikbarePartimsColumn");
    var aanvraagDetail = document.getElementById("aanvraagDetail");
    var parentDiv = $(this).parent()[0];
    var moduleid = $(parentDiv).data("moduleid");
    var moduleNaam = $(parentDiv).find(".h4").text();

    if ($.contains(beschikbarePartims, this)) {
        $(parentDiv).find("ul").addClass("hide");

        $(parentDiv).detach();
        $("#aangevraagdePartimsColumn").find(".panel-body").append($(parentDiv));
        $(parentDiv).find(".btn").removeClass("hide");

        var clon = $("#dummy").clone();
        clon.find("h3").text(moduleNaam);
        clon.find("h4").text("");
        clon.attr("id", moduleid);
        clon.appendTo("section");
        changed($(clon));
    } else {
        if ($(this).hasClass("active")) {
            Return();
            return;
        } else {
            hideShown();
            var show = $(aanvraagDetail).find("#" + moduleid);
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
    var that = $(this).parent();
    var supercode;
    if ($(that).is("li")) {
        console.log("partim...")
        var parentDiv = $(that).parent().parent()[0];
        var moduleid = $(parentDiv).data("moduleid");
        var moduleNaam = $(parentDiv).find(".h4").text();
        if ($("#beschikbarePartimsColumn div[data-moduleid=" + moduleid + "]").length === 0) $("#beschikbarePartimsColumn .panel-body").append("<div data-moduleid=\"" + moduleid + "\"><span class=\"name h4 module\">" + moduleNaam + "</span><span class=\"glyphicon glyphicon-remove btn badge hide\"> </span><ul class=\"list-group\"></ul></div>");
        var newParent = $("#beschikbarePartimsColumn").find("div[data-moduleid=" + moduleid + "] ul");
        var aanvraag = $("#aanvraagDetail").find("#" + $(that).data("supercode"));
        supercode = $(that).data("supercode");

        $(that).detach();
        $(that).removeClass("active");
        $(that).appendTo($(newParent));
        $(that).find(".btn").addClass("hide");

        if ($(parentDiv).find("ul").children().length === 0) {
            parentDiv.remove();
            $("#beschikbarePartimsColumn").find("div[data-moduleid=" + moduleid + "]").children("span:first").addClass("module");
        }
        if ($(aanvraag).hasClass("nothidden")) toFirstView();

        aanvraag.remove();
    } else {
        console.log("module...")
        var beschikbarePartims = document.getElementById("beschikbarePartimsColumn");
        var aanvraagDetail = document.getElementById("aanvraagDetail");
        var newParent = $(beschikbarePartims).find(".panel-body")
        var parentDiv = $(that).parent()[0];
        var moduleid = $(parentDiv).data("moduleid");
        var moduleNaam = $(parentDiv).find(".h4").text();
        var aanvraag = $("#aanvraagDetail").find("#" + moduleid)
        supercode = moduleid;

        $(parentDiv).find("ul").removeClass("hide");

        $(that).detach();
        $(that).removeClass("active");
        $(that).appendTo($(newParent));
        $(that).find(".btn").addClass("hide");

        if ($(aanvraag).hasClass("nothidden")) toFirstView();

        aanvraag.remove();
    }
    var dossierId = document.URL.split("/")[document.URL.split("/").length - 1];
    savePartimdetails();
    $.ajax({
        url: $("#aangevraagdePartimsColumn").data("url"),
        data: {
            dossierId: dossierId,
            supercode: supercode
        },
        type: "POST",
        success: function (data) {
            console.log(data);
        }
    });
});

$(document).ready(function () {
    $("bewijzenColumn").addClass("hide");
    $('[data-toggle="tooltip"]').tooltip();
});

//$(document).on("hover", '[data-toggle="tooltip"]', function () {
//    $(this).tooltip();
//});