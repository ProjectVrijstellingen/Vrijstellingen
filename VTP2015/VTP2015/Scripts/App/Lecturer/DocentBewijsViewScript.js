var studentId;
var huidigeStudent;
var alleStudenten;
var huidigeAanvragen;
var huidigeBewijzen;
var huidigeAanvraag;
var huidigBewijs;

$(document).ready(function () {
        
    stopStudentenInArray();

    selectStudent(alleStudenten[0]);

    $(".studentpointer").click(function (event) {
        //$(".studentpointer").click(function (event) {
        //    if ($(document).width() <= 990) {
        //        scroll_to($(".aanvraagcontainer"));
        //    }
        //});
        //$(alleStudenten[huidigeStudent]).removeClass("active");
        //selectStudent(this);

        var active = $(this).hasClass("active");

        $(this).parent().find(".active").removeClass("active");

        if (!active) {
            $(this).addClass("active");
            $(".aanvraag").addClass("hide");
            if ($('.partimpointer.active').length > 0)
            {
                $("#" + $(".partimpointer.active").data("supercode")).find('[data-studentid="' + $(this).data("studentid") + '"]').removeClass("hide");
            }
            else
            {
                $('[data-studentid="' + $(this).data("studentid") + '"]').removeClass("hide");
            }
        }
        else {
            if ($('.partimpointer.active').length > 0) {
                $(".aanvraag").addClass("hide");
                $("#" + $(".partimpointer.active").data("supercode")).find(".aanvraag").removeClass("hide");
            }
            else {
                $(".aanvraag").removeClass("hide");
            }
        }
    });

    $(".partimpointer").click(function (event) {
        var active = $(this).hasClass("active");
        $(this).parent().find(".active").removeClass("active");

        if (!active)
        {
            $(this).addClass("active");
            $(".aanvraag").addClass("hide");
            if ($('.studentpointer.active').length > 0) {
                $("#" + $(this).data("supercode")).find('[data-studentid="' + $(".studentpointer.active").data("studentid") + '"]').removeClass("hide");
            }
            else {
                $("#" + $(this).data("supercode")).find(".aanvraag").removeClass("hide");
            }
        }
        else
        {
            if ($('.studentpointer.active').length > 0) {
                $(".aanvraag").addClass("hide");
                $('[data-studentid="' + $(".studentpointer.active").data("studentid") + '"]').removeClass("hide");
            }
            else {
                $(".aanvraag").removeClass("hide");
            }
        }
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
            $($(this).parent().children()[1]).removeClass("hide");
        else
            $($(this).parent().children()[1]).addClass("hide");

    });


    $(".vorigBewijs").click(function(event) {
        event.preventDefault();
        $(huidigeBewijzen[huidigBewijs]).addClass("hide");
        if (huidigBewijs > 0) {
            huidigBewijs--;
        } else {
            huidigBewijs = huidigeBewijzen.length - 1;
        }
        $(huidigeAanvragen[huidigeAanvraag]).find(".huidigBewijs").text(huidigBewijs + 1);
        $(huidigeBewijzen[huidigBewijs]).removeClass("hide");
    });

    $(".volgendBewijs").click(function(event) {
        event.preventDefault();
        $(huidigeBewijzen[huidigBewijs]).addClass("hide");
        if (huidigBewijs < huidigeBewijzen.length - 1) {
            huidigBewijs++;
        } else {
            huidigBewijs = 0;
        }
        $(huidigeAanvragen[huidigeAanvraag]).find(".huidigBewijs").text(huidigBewijs + 1);
        $(huidigeBewijzen[huidigBewijs]).removeClass("hide");
    });

    $("#vorigeAanvraag").click(function() {
        vorigeAanvraag(event);
    });

    $("#volgendeAanvraag").click(function() {
        volgendeAanvraag(event);
    });

    $(".approveButton").click(function (event) {
        var aanvraagId = $(huidigeAanvragen[huidigeAanvraag]).data("aanvraagid");
        $.ajax({
            url: "Lecturer/ApproveAanvraag",
            data: { aanvraagID: aanvraagId },
            type: "POST",
            success: function (data) {
                removeCurrentAanvraag();
                console.log(data)
            }
        });
    });

    $(".dissapproveButton").click(function (event) {
        var aanvraagId = $(huidigeAanvragen[huidigeAanvraag]).data("aanvraagid");
        $.ajax({
            url: "Lecturer/DissapproveAanvraag",
            data: { aanvraagID: aanvraagId },
            type: "POST",
            success: function (data) {
                removeCurrentAanvraag();
                console.log(data);
            }

        });
    });

    $(document).keydown(function (e) {
        switch (e.which) {
            case 37: vorigeAanvraag(e);
                break; //left

            case 38:
                if (huidigeStudent != 0) {
                    $(alleStudenten[huidigeStudent]).removeClass("active");
                    $(selectStudent(alleStudenten[huidigeStudent - 1]));
                }
                break; //up

            case 39: volgendeAanvraag(e);
                break; //right

            case 40:
                if (huidigeStudent != (alleStudenten.length - 1)) {

                    $(alleStudenten[huidigeStudent]).removeClass("active");
                    $(selectStudent(alleStudenten[huidigeStudent + 1]));
                }
                break; //down

            default: return; 
        }
        e.preventDefault(); // prevent the default action (scroll / move caret)
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

});


function scroll_to(selectorOfElementToScrollTo) {
    $("html, body").animate({
        scrollTop: $(selectorOfElementToScrollTo).offset().top
    }, 750);
}

function removeCurrentAanvraag() {
    huidigeAanvragen[huidigeAanvraag].remove();
    huidigeAanvragen.splice(huidigeAanvraag, 1);
    if (huidigeAanvragen.length > 0) {
        $(huidigeAanvragen[huidigeAanvraag]).removeClass("hide");
        stopBewijzenVanHuidigeAanvraagInArrayEnToonDeEerste();
        $("#aantalAanvragen").text(huidigeAanvragen.length);
    } else {
        $(".studentpointer[data-studentid='" + studentId + "']").remove();
        alleStudenten.splice(huidigeStudent, 1);
        selectStudent(alleStudenten[huidigeStudent]);
    }
}

function volgendeAanvraag(event) {
    event.preventDefault();
    $(huidigeAanvragen[huidigeAanvraag]).addClass("hide");
    if (huidigeAanvraag < huidigeAanvragen.length - 1) {
        huidigeAanvraag++;
    } else {
        huidigeAanvraag = 0;
    }
    $("#huidigeAanvraag").text(huidigeAanvraag + 1);
    stopBewijzenVanHuidigeAanvraagInArrayEnToonDeEerste();
    $(huidigeAanvragen[huidigeAanvraag]).removeClass("hide");
}

function vorigeAanvraag(event) {
    event.preventDefault();
    $(huidigeAanvragen[huidigeAanvraag]).addClass("hide");
    if (huidigeAanvraag > 0) {
        huidigeAanvraag--;
    } else {
        huidigeAanvraag = huidigeAanvragen.length - 1;
    }
    $("#huidigeAanvraag").text(huidigeAanvraag + 1);
    stopBewijzenVanHuidigeAanvraagInArrayEnToonDeEerste();
    $(huidigeAanvragen[huidigeAanvraag]).removeClass("hide");

}

function stopBewijzenVanHuidigeAanvraagInArrayEnToonDeEerste() {
    huidigeBewijzen = $(huidigeAanvragen[huidigeAanvraag]).find(".evidence").toArray(); //Alle bewijzen van een bepaalde aanvraag in een array stoppen
    $(huidigeBewijzen[huidigBewijs]).removeClass("hide"); //Toont het eerste evidence van de geselecteerde aanvraag
}

function stopStudentenInArray() {
    alleStudenten = $(".studentpointer").toArray();
}

function selectStudent(student) {
    $(".aanvraag").addClass("hide"); //hide alle aanvragen
    huidigeAanvraag = 0;
    huidigBewijs = 0;
    $(".huidigBewijs").text("1");
    $(student).addClass("active");
    studentId = $(student).data("studentid");
    huidigeAanvragen = $(".aanvraag[data-studentid='" + studentId + "']").toArray(); //Alle aanvragen van een bepaalde student in een array stoppen
    stopBewijzenVanHuidigeAanvraagInArrayEnToonDeEerste();
    $("#aantalAanvragen").text(huidigeAanvragen.length);
    $("#huidigeAanvraag").text(huidigeAanvraag + 1);
    $(huidigeAanvragen[huidigeAanvraag]).removeClass("hide"); //Toont de eerste aanvraag van de geselecteerde student
    for (var i = 0; i < alleStudenten.length; i++) {
        if (alleStudenten[i] === student) {
            huidigeStudent = i;
            return;
        }
    }
}

