$(document).ready(function () {

    $("#btnShowOverview").click(showFileOverview);

    $(".nextEvidence").click(function (e) {
        e.preventDefault(); 
        switchEvidence(this, "next");
    });

    $(".previousEvidence").click(function(e) {
        e.preventDefault();
        switchEvidence(this, "previous");
    });

    $("#btnApprovedRequests").click(function(e) {
        e.preventDefault();
        
        toggle($("[data-status=Approved]"));
    });

    $("#btnDeniedRequests").click(function(e) {
        e.preventDefault();

        toggle($("[data-status=Rejected]"));
    });

    $("#btnUntreatedRequests").click(function(e) {
        e.preventDefault();

        toggle($("[data-status=Untreated]"));
    });

    $("#partimNameSearchQuery").keyup(function (e) {
        searchFilesForPartimName(this);
    });

    $("#files").on("click", ".btnSelectPartim", function (e) {
        var fileId = $(e.currentTarget).data("fileid");
        var index = $(e.currentTarget).data("index");

        showPartimDetail(fileId, index);
    });

});

function showFileOverview() {

    $("#details").addClass("hide");
    //todo: needs to go to fileOverview
    $("#overview").removeClass("hide");
}

function isEven(number) {
    return number % 2 === 0;
}

function showEvidence(request, requestIndex) {
    var evidence = $(request).find("[data-evidenceindex=" + requestIndex + "]");
    evidence.removeClass("hide");
}

function hideEvidence(request, requestIndex) {
    var evidence = $(request).find("[data-evidenceindex=" + requestIndex + "]");
    evidence.addClass("hide");
}

function switchEvidence(sender, direction) {
    var currentEvidenceIndexSpan = $(sender).parent().find(".currentEvidence");
    var amountOfEvidence = $(sender).parent().find(".amountOfEvidence").text();

    var currentEvidenceIndex = parseInt(currentEvidenceIndexSpan.text());

    var request = $(sender).parent().parent().parent().parent();
    var newCurrentEvidenceIndex = 0;

    switch(direction) {
        case "next":
        {
            if (currentEvidenceIndex < amountOfEvidence)
                newCurrentEvidenceIndex = currentEvidenceIndex + 1;
            else
                newCurrentEvidenceIndex = 1;
            break;
        }
        case "previous":
        {
            if (currentEvidenceIndex > 1)
                newCurrentEvidenceIndex = currentEvidenceIndex - 1;
            else
                newCurrentEvidenceIndex = amountOfEvidence;
            break;
        }

    }

    hideEvidence(request, currentEvidenceIndex);
    showEvidence(request, newCurrentEvidenceIndex);

    currentEvidenceIndexSpan.text(newCurrentEvidenceIndex);

}

function searchFilesForPartimName(sender) {
    $(".request").each(function (key, value) {
        
        var name = $(value).data("partimname");

        if (name != undefined) {
            if (searchQueryContains(name, sender)) {
                $(value).show();
            } else {
                $(value).hide();
            }
        }

    });
};
    
function loadFileById(fileId) {

    $.getJSON("/counselor/GetFileDetailsById/" + fileId, function (data) {
        var newFile = $("#dummyFile").clone();
        newFile.removeAttr("id");
        newFile.attr("data-fileid", fileId);
        newFile.find(".studentName").text(data.StudentName);

        var partimList = newFile.find(".partimList");

        $(data.Modules).each(function (moduleIndex, module) {
            console.log("current module: " + moduleIndex);

            var newModule = $("#dummyModule").clone();
            newModule.removeAttr("id");
            newModule.find(".moduleName").text(module.Name);
            partimList.append(newModule);

            $(module.Partims).each(function (partimIndex, partim) {
                console.log("current partim: " + partimIndex);
                var index = "" + moduleIndex + partimIndex;

                var newPartim = $("#dummyPartim").clone();
                newPartim.removeAttr("id"); 
                newPartim.find(".partimName").text(partim.Name);
                newPartim.attr("data-fileid", partim.FileId);
                newPartim.attr("data-requestId", partim.RequestId);
                newPartim.attr("data-status", partim.Status);
                newPartim.attr("data-index", index);
                partimList.append(newPartim);

                var newPartimDetail = $("#dummyRequestDetail").clone();
                newPartimDetail.removeAttr("id");
                newPartimDetail.attr("data-index", index);
                newPartimDetail.find(".argumentation").text(partim.Argumentation);
                newPartimDetail.find(".amountOfEvidence").text(partim.Evidence.length);

                $(partim.Evidence).each(function (evidenceIndex, evidence) {
                    console.log("current evidence: " + evidenceIndex);

                    var newEvidence = $("#dummyEvidence").clone();
                    newEvidence.removeAttr("id");
                    newEvidence.attr("data-evidenceindex", evidenceIndex);
                    newEvidence.find(".argumentation").text(evidence.Argumentation);

                    newEvidence.find(".downloadLink").attr("href", evidence.Path);
                    if(evidence.Type === "pdf")
                        newEvidence.find(".pdf").attr("src", evidence.Path);
                    else
                        newEvidence.find(".image").attr("src", evidence.Path);

                    console.log(evidence.Path);
                    newPartimDetail.find(".evidenceContainer").append(newEvidence);
                    //console.log("new evidence: ");
                    //console.log(newEvidence);
                });

                newFile.find(".partimDetails").append(newPartimDetail);

            });
        });

        $("#files").append(newFile);

    });
}

function showPartimDetail(fileId, index) {
    //TODO: Kan efficienter
    $(".fileDetail").addClass("hide");
    $(".partimDetail").addClass("hide");

    var file = $(".fileDetail[data-fileid='" + fileId + "']");
    var partim = file.find(".partimDetail[data-index='" + index + "']");
    file.removeClass("hide");
    partim.removeClass("hide");
}

// "public" function for the fileoverview
function selectFileById(file) {
    console.log(file);

    $(".request").addClass("hide");

    $("#details").removeClass("hide");
    $("#name").text(file.name);

    $(".fileDetail").each(function (index, value) {
        if ($(value).data("fileid") === file.fileId) {
            $(value).removeClass("hide");
        }
    });

    var selectedIndex = 1; //starts at one for simplicity
    var amountOfApprovedRequests = 0;
    var amountOfDeniedRequests = 0;
    var amountOfUntreatedRequests = 0;

    $(".request").each(function (index, value) {

        if ($(value).data("fileid") === file.fileId) {

            $(value).removeClass("hide");

            //if (isEven(selectedIndex)) {
            //    $(value).addClass("evenRowColor");
            //} else {
            //    $(value).addClass("oddRowColor");
            //}

            switch($(value).data("status")) {
                case "Approved":
                    amountOfApprovedRequests++;
                    $(value).addClass("approvedRow");
                    break;
                case "Rejected":
                    amountOfDeniedRequests++;
                    $(value).addClass("deniedRow");
                    break;
                case "Untreated":
                    amountOfUntreatedRequests++;
                    $(value).addClass("untreatedRow");
                    break;
            }

            showEvidence(value, 1); //show first evidence for request

            selectedIndex++;
        }

    });

    $("#spnAmountOfApprovedRequests").text(amountOfApprovedRequests);
    $("#spnAmountOfDeniedRequests").text(amountOfDeniedRequests);
    $("#spnAmountOfUntreatedRequests").text(amountOfUntreatedRequests);


}

function loadFiles(fileIds) {
    console.log("loading files:");
    $(fileIds).each(function (index, value) {
        loadFileById(value);
    });
}