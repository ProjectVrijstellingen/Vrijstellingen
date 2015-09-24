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
                if (data[0] === "Finish") location.reload();
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
                errorList.append("<li>Request failed</li>");
            }
        });
    });
});

$(document).on("click", ".glyphicon-minus", function () {
    var that = $(this).parent();
    $.ajax({
        url: "Student/DeleteEvidence",
        data: { bewijsId: $(that).data("bewijsid") },
        type: "POST",
        success: function(data) {
            that.remove();
            alert(data);
        }
    });
});