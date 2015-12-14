﻿$(document).ready(function () {
    $(".aantal").addClass("hide");

    $($(".approvedaanvraagcontainer").children()[0]).click(function (event) {
        if ($($(".approvedaanvraagcontainer").children()[1]).hasClass("hide"))
            $($(".approvedaanvraagcontainer").children()[1]).removeClass("hide")
        else
            $($(".approvedaanvraagcontainer").children()[1]).addClass("hide")
    });

    $($(".rejectedaanvraagcontainer").children()[0]).click(function (event) {
        if ($($(".rejectedaanvraagcontainer").children()[1]).hasClass("hide"))
            $($(".rejectedaanvraagcontainer").children()[1]).removeClass("hide")
        else
            $($(".rejectedaanvraagcontainer").children()[1]).addClass("hide")
    });

    $($(".partimcontainer").children()[0]).click(function (event) {
        if ($("#partimPointerDiv").hasClass("hide"))
            $("#partimPointerDiv").removeClass("hide")
        else
            $("#partimPointerDiv").addClass("hide");
    });

    $($(".studentcontainer").children()[0]).click(function (event) {
        if ($("#aanvraagPointerDiv").hasClass("hide"))
            $("#aanvraagPointerDiv").removeClass("hide")
        else
            $("#aanvraagPointerDiv").addClass("hide")
    });

    $(".studentpointer").click(function (event) {
        var active = $(this).hasClass("active");

        $(this).parent().find(".active").removeClass("active");

        if (!active) {
            $(this).addClass("active");
            $(".modulecontainer").addClass("hide");
            if ($('.partimpointer.active').length > 0) {
                $("[data-super=" + $(".partimpointer.active").data("supercode") + "][data-studentid='" + $(".studentpointer.active").data("studentid") + "']").removeClass("hide");
            }
            else {
                $("[data-studentid='" + $(".studentpointer.active").data("studentid") + "']").removeClass("hide");
            }
        }
        else {
            if ($('.partimpointer.active').length > 0) {
                $(".modulecontainer").addClass("hide");
                $("[data-super=" + $(".studentpointer.active").data("supercode") + "]").removeClass("hide");
            }
            else {
                $(".modulecontainer").removeClass("hide");
            }
        }
        scroll_to($(".aanvraagcontainer"));
    });

    $(".partimpointer").click(function (event) {
        var active = $(this).hasClass("active");
        $(this).parent().find(".active").removeClass("active");

        if (!active) {
            $(this).addClass("active");
            $(".modulecontainer").addClass("hide");
            if ($('.studentpointer.active').length > 0) {
                $("[data-super=" + $(this).data("supercode") + "][data-studentid='" + $(".studentpointer.active").data("studentid") + "']").removeClass("hide");

            }
            else {
                $("[data-super=" + $(this).data("supercode") + "]").removeClass("hide");
            }
        }
        else {
            if ($('.studentpointer.active').length > 0) {
                $(".modulecontainer").addClass("hide");
                $("[data-studentid='" + $(".studentpointer.active").data("studentid") + "']").removeClass("hide");
            }
            else {
                $(".modulecontainer").removeClass("hide");
            }
        }
        scroll_to($(".aanvraagcontainer"));
    });

    $("#filter").bind("keyup", function () {
        for (i = 0; i < $("#studentlist").children().length; i++) {
            if (!$($("#studentlist").children()[i]).data("studentid").substr(0, $($("#studentlist").children()[i]).data("studentid").indexOf('@')).contains($(this).val().replace(" ", ".").toLowerCase())) {
                $($("#studentlist").children()[i]).addClass("hide");
                $($("#studentlist").children()[i]).removeClass("active");
            }
            else {
                $($("#studentlist").children()[i]).removeClass("hide");
            }
        }
    });


    $(".argumentatie").click(function (event) {
        if ($($(this).parent().children()[1]).hasClass("hide")) {
            $($(this).parent().children()[1]).removeClass("hide");
            $($(this).parent().children()[2]).removeClass("hide");
            $($(this).parent().children()[3]).removeClass("hide");
            $('[data-bewijsid="' + $(this).data("bewijsid") + '"]').css("font-size", "17px").css("font-weight", "bold");
        }
        else {
            $($(this).parent().children()[1]).addClass("hide");
            $($(this).parent().children()[2]).addClass("hide");
            $($(this).parent().children()[3]).addClass("hide");
            $('[data-bewijsid="' + $(this).data("bewijsid") + '"]').css("font-size", "15px").css("font-weight", "normal");
        }

    });


});