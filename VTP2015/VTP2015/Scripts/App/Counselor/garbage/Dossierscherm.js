var files = [];

$(document).ready(function () {
    appendFilesToArray();
    $("#searchQuery").keyup(searchFilesForName);

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

var appendFilesToArray = function () {
    $("dossier").each(function () {
        files.push(this);
    });
};

var searchQueryContains = function (string) {
    return string.toLowerCase().indexOf($("#searchQuery").val().toLowerCase()) >= 0;
};

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});