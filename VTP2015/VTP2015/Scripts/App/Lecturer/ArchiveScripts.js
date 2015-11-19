$(document).ready(function () {
    console.log("Test ok");
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


});