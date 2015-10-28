$(document).on("click", ".glyphicon-ok", function () {
    var that = $(this);
    var email = that.parent().siblings(":first").text();
    $.ajax({
        url: "Admin/RemoveUserFromRole",
        data: { email: email, role: that.attr("data-role") },
        type: "POST",
        success: function (data) {
            if (data === "False") alert("U kan uw eigen roles niet wijzigen!");
            else {
                that.removeClass("glyphicon-ok");
                that.addClass("glyphicon-remove");
            }
        }
    });
});

$(document).on("click", ".glyphicon-remove", function () {
    var that = $(this);
    var email = that.parent().siblings(":first").text();
    $.ajax({
        url: "Admin/AddUserToRole",
        data: { email: email, role: that.attr("data-role") },
        type: "POST",
        success: function (data) {
            if (data === "False") alert("U kan uw eigen roles niet wijzigen!");
            else {
                that.removeClass("glyphicon-remove");
                that.addClass("glyphicon-ok");
            }
        }
    });
});

$(document).on("click", ".btn", function () {
    var model = new FormData();
    model.append("InfoMailFrequency", $("#InfoMailFrequency").val());
    model.append("WarningMailFrequency", $("#WarningMailFrequency").val());
    model.append("StartVrijstellingDayMonth", $("#StartVrijstellingDayMonth").val());
    model.append("EindeVrijstellingDayMonth", $("#EindeVrijstellingDayMonth").val());

    $.ajax({
        url: "Admin/SubmitConfig",
        data: model,
        type: "POST",
        dataType: "json",
        processData: false,
        contentType: false,
        success: function (data) {
            if (data[0] === "Finish") location.reload();
            else {
                var errorList = $(".text-danger").find("ul");
                errorList.empty();
                data.forEach(function (error) {
                    errorList.append("<li>" + error + "</li>");
                });
            }
        },
        error: function (data) {
            errorList.empty();
            errorList.append("<li>RequestPartimInformation failed</li>");
        }
    });
});