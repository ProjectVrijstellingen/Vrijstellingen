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

function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}

function changed(partimdetail) {
    clearTimeout(timer);
    $(partimdetail).find("#status").text("Saving...");
    var cons = false;
    $.each(partimdetails, function() {
        if ($(this).attr("id") === $(partimdetail).attr("id")) cons = true;
    });
    if (!cons) partimdetails.push($(partimdetail));
    timer = setTimeout(savePartimdetails, saveTime);
}

function savePartimdetails() {
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

function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    var that = document.getElementById(data);
    addBewijs(that);
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
    var moduleId = $(parentDiv).data("moduleid");
    var supercode = $(this).data("supercode");
    var moduleNaam = parentDiv.getElementsByTagName("h4")[0].innerHTML;
    var newParent;

    if ($.contains(beschikbarePartims, this)) {
        if ($("#aangevraagdePartimsColumn div[data-moduleId=" + moduleId + "]").length === 0) $("#aangevraagdePartimsColumn .panel-body").html($("#aangevraagdePartimsColumn .panel-body").html() + "<div data-moduleid=\"" + moduleId + "\"><h4>" + moduleNaam + "</h4><ul class=\"list-group\"></ul></div>");

        newParent = $("#aangevraagdePartimsColumn div[data-moduleId=" + moduleId + "] ul");
        $(this).detach();
        $(newParent).parent().removeClass("hide");
        $(this).appendTo($(newParent));
        $(this).addClass("active");
        $(this).find(".btn").removeClass("hide");

        if ($(parentDiv).find("ul").children().length === 0) $(parentDiv).remove();

        var clon = $("#dummy").clone();
        clon.find("h3").text(moduleNaam);
        clon.find("h4").text($(this).text());
        clon.attr("id", supercode);
        clon.removeClass("hide");
        clon.addClass("nothidden");
        clon.appendTo("section");
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
    }
    if (!$(beschikbarePartims).hasClass("hide")) toSecondView();
});

$(document).on("click", ".glyphicon-plus", function () {
    var that = $(this).parent();
    addBewijs(that);
});

$(document).on("click", ".glyphicon-remove", function (e) {
    console.log("remove Request");
    e.stopPropagation();
    var that = $(this).parent();
    var parentDiv = $(that).parent().parent()[0];
    var moduleId = $(parentDiv).data("moduleid");
    var moduleNaam = parentDiv.getElementsByTagName("h4")[0].innerHTML;
    if ($("#beschikbarePartimsColumn div[data-moduleId=" + moduleId + "]").length === 0) $("#beschikbarePartimsColumn .panel-body").html($("#beschikbarePartimsColumn .panel-body").html() + "<div data-moduleid=\"" + moduleId + "\"><h4>" + moduleNaam + "</h4><ul class=\"list-group\"></ul></div>");
    var newParent = $("#beschikbarePartimsColumn").find("div[data-moduleId=" + moduleId + "] ul");
    var aanvraag = $("#aanvraagDetail").find("#" + $(that).data("supercode"));

    $(that).detach();
    $(that).removeClass("active");
    $(that).appendTo($(newParent));
    $(that).find(".btn").addClass("hide");

    if ($(parentDiv).find("ul").children().length === 0) parentDiv.remove();

    if ($(aanvraag).hasClass("nothidden")) toFirstView();

    aanvraag.remove();

    var supercode = $(that).data("supercode");
    var dossierId = document.URL.split("/")[document.URL.split("/").length - 1];
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
});