$(document).ready(function () {

    $("#btnShowOverview").click(showFileOverview);

    $("#files").on("click", ".nextEvidence", function (e) {
        e.preventDefault(); 
        switchEvidence(this, "next");
    });
            
    $("#files").on("click", ".previousEvidence", function (e) {
        console.log("pas");
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

    $("#files").on("click", ".btnRemovePartim", function (e) {
        var partimInformationId = $(e.currentTarget).data("partiminformationid");
        var fileId = $(e.currentTarget).data("fileid");

        removePartim(partimInformationId, fileId);
    });

    $("#files").on("click", ".btnRemoveFile", function(e) {
        var fileId = $(e.currentTarget).data("fileid");

        removeFile(fileId);
    });

});

function removePartim(partimInformationId, fileId) {
    $.ajax({
        url: "/Counselor/RemovePartimFromFile",
        data: {
            partimInformationId: partimInformationId,
            fileId: fileId
        },
        method: "post"
    }).done(function () {
        var partimsToBeDeleted = $("[data-partiminformationid='" + partimInformationId + "']");
        var moduleGroup = partimsToBeDeleted.closest(".moduleGroup");
        partimsToBeDeleted.remove();

        if (moduleGroup.find("li").length < 2) 
            moduleGroup.parent().remove();
    });
}

function removeFile(fileId) {
    $.ajax({
        url: "/Counselor/DeleteFile",
        data: {
            fileId: fileId
        },
        method: "post"
    }).success(function(data) {
        showFileOverview();
    });
}

function showFileOverview() {

    $("#details").addClass("hide");
    //todo: needs to go to fileOverview
    $("#overview").removeClass("hide");
}

function isEven(number) {
    return number % 2 === 0;
}

function showEvidence(partim, partimIndex) {
    var evidence = $(partim).find("[data-index=" + partimIndex + "]");
    evidence.removeClass("hide");
}

function hideEvidence(partim, partimIndex) {
    var evidence = $(partim).find("[data-index=" + partimIndex + "]");
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
            else {
                newCurrentEvidenceIndex = 1;
            }
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
        newFile.find("#spnAmountOfApprovedRequests").text(data.AmountOfApprovedRequests);
        newFile.find("#spnAmountOfDeniedRequests").text(data.AmountOfDeniedRequests);
        newFile.find("#spnAmountOfUntreatedRequests").text(data.AmountOfUntreatedRequests);
        newFile.find(".btnRemoveFile").attr("data-fileid", fileId);
        var partimList = newFile.find(".partimList");

        $(data.Modules).each(function (moduleIndex, module) {
            var newModule = $("#dummyModule").clone();
            newModule.removeClass("hide");
            newModule.removeAttr("id");
            newModule.addClass("moduleSpace");
            newModule.find(".moduleName").text(module.Name);
            partimList.append(newModule);

            $(module.Partims).each(function (partimIndex, partim) {
                var index = "" + moduleIndex + partimIndex;
                console.log("current partim: " + index);

                var newPartim = $(newModule).find("#dummyPartim").clone();
                newPartim.removeClass("hide");
                newPartim.removeAttr("id"); 
                newPartim.find(".partimName").text(partim.Name);
                console.log("current partim: ");
                console.log(partim);
                newPartim.attr("data-fileid", partim.FileId);
                newPartim.attr("data-requestId", partim.RequestId);
                newPartim.attr("data-status", partim.Status);
                newPartim.attr("data-index", index);
                newPartim.attr("data-partiminformationid", partim.PartimInformationId);
                $(newModule).find("ul").append(newPartim);

                var newPartimDetail = $("#dummyRequestDetail").clone();
                newPartimDetail.removeAttr("id");
                newPartimDetail.attr("data-partiminformationid", partim.PartimInformationId);
                newPartimDetail.attr("data-index", index);
                newPartimDetail.find(".btnRemovePartim").attr("data-partiminformationid", partim.PartimInformationId);
                newPartimDetail.find(".btnRemovePartim").attr("data-fileid", partim.FileId);

                if (partim.PrevEducations.length > 0) {
                    $(partim.PrevEducations).each(function (educationIndex, education) {
                        console.log("current education: " + educationIndex);

                        var newEvidence = $("#educationDummy").clone();
                        newEvidence.removeClass("hide");
                        newEvidence.attr("data-index", educationIndex + 1);
                        newEvidence.text(education.Education);

                        console.log(education.Path);
                        newPartimDetail.find(".opleidingen").append(newEvidence);
                    });
                } else {
                    newPartimDetail.find(".opleidingenDiv").remove();
                }

                if (partim.Evidence.length > 0) {
                    newPartimDetail.find(".amountOfEvidence").text(partim.Evidence.length);
                    $(partim.Evidence).each(function(evidenceIndex, evidence) {
                    console.log("current evidence: " + evidenceIndex);

                    var newEvidence = $("#dummyEvidence").clone();
                    newEvidence.removeAttr("id");
                    newEvidence.attr("data-index", evidenceIndex + 1);
                    newEvidence.find(".argumentation").text(evidence.Argumentation);
                    var path = evidence.Path;
                    console.log(path);
                    var start = path.lastIndexOf("\\", path.lastIndexOf("\\", path.lastIndexOf("\\") - 1) - 1);
                        console.log(start);
                        path = path.substring(start);
                        console.log(path);
                        path = path.replace(/\\/g, "/");
                        console.log(path);
                    newEvidence.find(".downloadLink").attr("href", path);
                        if (evidence.Type === "pdf")
                            newEvidence.find(".pdf").attr("src", path);
                    else
                            newEvidence.find(".image").attr("src", path);

                    console.log(path);
                    newPartimDetail.find(".evidenceContainer").append(newEvidence);
                    //console.log("new evidence: ");
                    //console.log(newEvidence);
                });
                } else {
                    newPartimDetail.find(".evidencePanel").remove();
                }
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

    showEvidence();
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

    showPartimDetail(file.fileId, "00");
}

function loadFiles(fileIds) {
    console.log("loading files:");
    $(fileIds).each(function (index, value) {
        loadFileById(value);
    });
}