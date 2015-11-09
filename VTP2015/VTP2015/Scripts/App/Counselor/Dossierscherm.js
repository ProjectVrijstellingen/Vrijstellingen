var files = [];

$(document).ready(function () {
    appendFilesToArray();
    $("#searchQuery").keyup(SearchQueryKeyupEventHandler);

});

var SearchQueryKeyupEventHandler = function () {
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
    $("#files .row div").each(function () {
        files.push(this);
    });
};

var searchQueryContains = function (string) {
    return string.toLowerCase().indexOf($("#searchQuery").val().toLowerCase()) >= 0;
};

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});