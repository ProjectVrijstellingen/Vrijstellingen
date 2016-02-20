$(document).ready(function () {

    $("#addBewijs-form").find("button").click(function () {
        var model = new FormData();
        model.append("File", document.getElementById("File").files[0]);
        model.append("Description", $("#Description").val());
        $.ajax({
            url: $("#addBewijs-form").data("url"),
            data: model,
            type: "POST",
            dataType: "json",
            processData: false,
            contentType: false,
            success: function (data) {
                if (data[0] === "Finish") {
                    location.reload();
                }
                else {
                    var errorList = $(document.getElementById("addBewijs-form")).find("ul");
                    errorList.empty();
                    data.forEach(function(error) {
                        errorList.append("<li>" + error + "</li>");
                    });
                }
            },
            error: function(data) {
                errorList.empty();
                errorList.append("<li>Connectie error</li>");
            }
        });
    });

    $("#addEducation-form").find("button").click(function () {
        var model = new FormData();
        model.append("Education", $("#Education").val());
        $.ajax({
            url: $("#addEducation-form").data("url"),
            data: model,
            type: "POST",
            dataType: "json",
            processData: false,
            contentType: false,
            success: function (data) {
                if (data[0] === "Finish") {
                    $("#Education").val("");
                    location.reload();
                }
                else {
                    var errorList = $(document.getElementById("addEducation-form")).find("ul");
                    errorList.empty();
                    data.forEach(function (error) {
                        errorList.append("<li>" + error + "</li>");
                    });
                }
            },
            error: function (data) {
                errorList.empty();
                errorList.append("<li>Connectie error</li>");
            }
        });
    });

    $("#bewijzenList").on("click", ".glyphicon-remove", function () {
        console.log("Delete bewijs");
        var that = $(this).parent();
        var url = "Student/DeleteEvidence";
        var data = { bewijsId: $(that).data("bewijsid") };
        $.ajax({
            url: url,
            data: data,
            type: "POST",
            success: function (data) {
                that.remove();
                console.log(data);
            }
        });
    });

    $("#educationList").on("click", ".glyphicon-remove", function () {
        console.log("Delete education");
        var that = $(this).parent();
        var url = "Student/DeleteEducation";
        var data = { educationId: $(that).data("educationid") };
        $.ajax({
            url: url,
            data: data,
            type: "POST",
            success: function (data) {
                that.remove();
                console.log(data);
            }
        });
    });
});

$(document).on("click", "#btnIndienen", function () {
    console.log("Dossier indienen");
    savePartimdetails();
    $.ajax({
        url: $("#infoSide").data("url"),
        data: {},
        type: "POST",
        success: function (data) {
            if (data[0] === "Done!") {
                location.reload();
            }
            else {
                var errorList = $(document.getElementById("indienErrors"));
                errorList.empty();
                data.forEach(function (error) {
                    errorList.append("<li>" + error + "</li>");
                });
            }
        }
    });
});