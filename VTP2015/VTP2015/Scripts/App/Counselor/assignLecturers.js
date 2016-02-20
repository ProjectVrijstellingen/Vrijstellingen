$(".email").bind("enterKey", function (e) {
    console.log("enter");
    var item = $(this).parent();
    var model = new FormData();
    model.append("SuperCode", $(item).attr("id"));
    model.append("Email", $(this).val());
    $.ajax({
        url: $("#assignList").data("url"),
        data: model,
        type: "POST",
        dataType: "json",
        processData: false,
        contentType: false,
        success: function (data) {
            if (data[0] === "Finish") {
                $(item).remove();
            }
            else {
                var errorList = $(item).find("ul");
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
$(".email").keyup(function (e) {
    if (e.keyCode === 13) {
        $(this).trigger("enterKey");
    }
});
