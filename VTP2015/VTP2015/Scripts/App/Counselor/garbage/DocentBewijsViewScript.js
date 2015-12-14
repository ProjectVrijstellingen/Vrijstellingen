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
        console.log(event);
        $(".studentpointer").click(function (event) {
            if ($(document).width() <= 990) {
                scroll_to($(".aanvraagcontainer"));
            }
        });
        $(alleStudenten[huidigeStudent]).removeClass("active");
        selectStudent(this);
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
            url: "Docent/ApproveAanvraag",
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
            url: "Docent/DissapproveAanvraag",
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
    huidigeBewijzen = $(huidigeAanvragen[huidigeAanvraag]).find(".evidence").toArray(); //Alle bewijzen van een bepaalde RequestPartimInformation in een array stoppen
    $(huidigeBewijzen[huidigBewijs]).removeClass("hide"); //Toont het eerste evidence van de geselecteerde RequestPartimInformation
}

function stopStudentenInArray() {
    alleStudenten = $(".studentpointer").toArray();
}

function selectStudent(student) {
    $(".RequestPartimInformation").addClass("hide"); //hide alle aanvragen
    huidigeAanvraag = 0;
    huidigBewijs = 0;
    $(".huidigBewijs").text("1");
    $(student).addClass("active");
    studentId = $(student).data("studentid");
    huidigeAanvragen = $(".RequestPartimInformation[data-studentid='" + studentId + "']").toArray(); //Alle aanvragen van een bepaalde student in een array stoppen
    stopBewijzenVanHuidigeAanvraagInArrayEnToonDeEerste();
    $("#aantalAanvragen").text(huidigeAanvragen.length);
    $("#huidigeAanvraag").text(huidigeAanvraag + 1);
    $(huidigeAanvragen[huidigeAanvraag]).removeClass("hide"); //Toont de eerste RequestPartimInformation van de geselecteerde student
    for (var i = 0; i < alleStudenten.length; i++) {
        if (alleStudenten[i] === student) {
            huidigeStudent = i;
            return;
        }
    }
}

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});