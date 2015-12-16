
$(document).ready(function () {

    $(".studentpointer").click(function (event) {

        var active = $(this).hasClass("active");

        $(this).parent().find(".active").removeClass("active");

        if (!active) {
            $(this).addClass("active");
            $(".modulecontainer").addClass("hide");
            if ($('.partimpointer.active').length > 0)
            {
                $("#" + $(".partimpointer.active").data("supercode")).find('[data-studentid="' + $(this).data("studentid") + '"]').parent().parent().removeClass("hide");
            }
            else
            {
                $('[data-studentid="' + $(this).data("studentid") + '"]').parent().parent().removeClass("hide");
            }
        }
        else {
            if ($('.partimpointer.active').length > 0) {
                $(".modulecontainer").addClass("hide");
                $("#" + $(".partimpointer.active").data("supercode")).find(".aanvraag").parent().parent().removeClass("hide");
            }
            else {
                $(".modulecontainer").removeClass("hide");
            }
        }
    });

    $(".partimpointer").click(function (event) {
        var active = $(this).hasClass("active");
        $(this).parent().find(".active").removeClass("active");

        if (!active)
        {
            $(this).addClass("active");
            $(".modulecontainer").addClass("hide");
            if ($('.studentpointer.active').length > 0) {
                $("#" + $(this).data("supercode")).find('[data-studentid="' + $(".studentpointer.active").data("studentid") + '"]').parent().parent().removeClass("hide");
            }
            else {
                $("#" + $(this).data("supercode")).find(".aanvraag").parent().parent().removeClass("hide");
            }
        }
        else
        {
            if ($('.studentpointer.active').length > 0) {
                $(".modulecontainer").addClass("hide");
                $('[data-studentid="' + $(".studentpointer.active").data("studentid") + '"]').parent().parent().removeClass("hide");
            }
            else {
                $(".modulecontainer").removeClass("hide");
            }
        }
    });

    $($(".partimcontainer").children()[0]).click(function (event) {
        if ($("#partimPointerDiv").hasClass("hide"))
            $("#partimPointerDiv").removeClass("hide")
        else
            $("#partimPointerDiv").addClass("hide");
    });
    


    $("#filter").bind("keyup", function () {
       for(i=0;i<$("#studentlist").children().length;i++)
        {
           if (!$($("#studentlist").children()[i]).data("studentid").substr(0, $($("#studentlist").children()[i]).data("studentid").indexOf('@')).contains($(this).val().replace(" ",".").toLowerCase()))
            {
               $($("#studentlist").children()[i]).addClass("hide");
               $($("#studentlist").children()[i]).removeClass("active");
            }
            else
            {
                $($("#studentlist").children()[i]).removeClass("hide");
            }
        }
    });


    $(".argumentatie").click(function (event) {
        if ($($(this).parent().children()[1]).hasClass("hide"))
        {
            $($(this).parent().children()[1]).removeClass("hide");
            $($(this).parent().children()[2]).removeClass("hide");
            $($(this).parent().children()[3]).removeClass("hide");
            $('[data-bewijsid="' + $(this).data("bewijsid") + '"]').css("font-size", "17px").css("font-weight", "bold");
        }
        else
        {
            $($(this).parent().children()[1]).addClass("hide");
            $($(this).parent().children()[2]).addClass("hide");
            $($(this).parent().children()[3]).addClass("hide");
            $('[data-bewijsid="' + $(this).data("bewijsid") + '"]').css("font-size", "15px").css("font-weight", "normal");
        }

    });


    $(".approveButton").click(function (event) {
        var aanvraagId = $(this).parent().parent().data("aanvraagid");
        var supercode = $(this).parent().parent().parent().parent().attr("id");
        var motivationId = $("#aanvraagform-" + aanvraagId + " option:checked").val();

        if (motivationId > 1)
        {
            $("#Aantal_" + supercode).html(parseInt($("#Aantal_" + supercode).html()) - 1);

            $.ajax({
                url: "ApproveAanvraag",
                data: { aanvraagID: aanvraagId, motivationID: motivationId },
                type: "POST",
                success: function (data) {
                    $('[data-aanvraagid="' + aanvraagId + '"]').addClass("hide");
                    console.log(data)
                }
            });
        }
        else
        {
            alert("Het is verplicht om een motivatie mee te geven");
        }
    });

    $(".dissapproveButton").click(function (event) {
        var aanvraagId = $(this).parent().parent().data("aanvraagid");
        var supercode = $(this).parent().parent().parent().parent().attr("id");
        var motivationId = $("#aanvraagform-" + aanvraagId + " option:checked").val();
        if (motivationId > 1) {
            $("#Aantal_" + supercode).html(parseInt($("#Aantal_" + supercode).html()) - 1);
            $.ajax({
                url: "DissapproveAanvraag",
                data: { aanvraagID: aanvraagId, motivationID: motivationId },
                type: "POST",
                success: function (data) {
                    $('[data-aanvraagid="' + aanvraagId + '"]').addClass("hide");
                    console.log(data);
                }
            });
        }
        else {
            alert("Het is verplicht om een motivatie mee te geven");
        }
    });

    $('[data-toggle="tooltip"]').tooltip();


    $($(".studentcontainer").children()[0]).click(function (event) {
        if ($("#aanvraagPointerDiv").hasClass("hide"))
            $("#aanvraagPointerDiv").removeClass("hide")
        else
            $("#aanvraagPointerDiv").addClass("hide")
    });


    $($(".aanvraagcontainer").children()[0]).click(function (event) {
        if ($($(".aanvraagcontainer").children()[1]).hasClass("hide"))
            $($(".aanvraagcontainer").children()[1]).removeClass("hide")
        else
            $($(".aanvraagcontainer").children()[1]).addClass("hide")
    });

    $(".remove").click(function (event) {
        var supercode = $(this).attr("Id");
        supercode = supercode.substr(supercode.indexOf("_")+1, supercode.length);
        
        $.ajax({
            url: "RemovePartimLecturer",
            data: { supercodeID: supercode },
            type: "POST",
            success: function (data) {
                $("#Verwijder_"+supercode).parent().addClass("hide");
                console.log(data);
            }
        }).fail(function (data) {
            console.log("Error");
            console.log(data);
        });
    });


});


function scroll_to(selectorOfElementToScrollTo) {
    $("html, body").animate({
        scrollTop: $(selectorOfElementToScrollTo).offset().top
    }, 750);
}
