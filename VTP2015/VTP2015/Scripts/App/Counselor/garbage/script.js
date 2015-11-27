var files = [];
var requests = [];
var currentFileSelected;

$(document).ready(function() {
    appendFilesToArray();
    $("#searchQuery").focus();
    $("#searchQuery").keyup(searchFilesForName);
    $(".herinnering").click(function (sender) {
        console.log($(sender.target).parent().data("aanvraagid"));
        $.ajax({
            url: "Counselors/SendReminder",
            data:{aanvraagId : $(sender.target).parent().data("aanvraagid")},
            type: "POST",
            success: function (data) {
                console.log(data);
            }
        });

    });

    $(".dossier").click(function (sender) {
        $("#overview").addClass("hide");

        var currentTarget = $(sender.currentTarget);
        
        var naam = currentTarget.find(".naam").text();
        var academieJaar = currentTarget.find(".academiejaar").text();
        var afstudeerRichting = currentTarget.find(".afstudeerrichting").text();

        console.log(academieJaar);

        $("#naam").text(naam);
        $("#academiejaar").text(academieJaar);
        $("#afstudeerrichting").text(afstudeerRichting);
        
        $("#details").removeClass("hide");

    });

    $("#tooverview").click(function(sender) {
        $("#details").addClass("hide");
        $("#overview").removeClass("hide");
    });
});




var searchFilesForName = function () {
    $.each(files, function (key, value) {
        var name = $(value).data("name");
        var prename = $(value).data("prename");
        if (name != undefined) {
            if (searchQueryContains(name) || searchQueryContains(prename) || searchQueryContains(name + " " + prename) || searchQueryContains(prename + " " + name)) {
                $(value).show();
            } else {
                $(value).hide();
            }
        }
    });
};

var appendFilesToArray = function() {
    $(".dossier").each(function() {
        files.push(this);
    });
};

var searchQueryContains = function(string) {
    return string.toLowerCase().indexOf($("#searchQuery").val().toLowerCase()) >= 0;
};
