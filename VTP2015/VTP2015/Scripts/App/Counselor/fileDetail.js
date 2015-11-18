$(document).ready(function () {

    $("#btnShowOverview").click(showFileOverview);

});

function showFileOverview() {

    $("#details").addClass("hide");
    //todo: needs to go to fileOverview
    $("#overview").removeClass("hide");
}

// "public" function for the fileoverview
function selectFileById(file) {
    $(".request").addClass("hide");

    $("#details").removeClass("hide");
    $("#name").text(file.name);

    $(".request").each(function(index, value) {
        if ($(value).data("fileid") === file.fileId)
            $(value).removeClass("hide");
    });

}