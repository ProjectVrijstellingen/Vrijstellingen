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

// "public" function for the fileoverview
function selectFileById(file) {
    $(".request").addClass("hide");

    $("#details").removeClass("hide");
    $("#name").text(file.name);

    var selectedIndex = 1; //starts at one for simplicity
    var amountOfApprovedRequests = 0;
    var amountOfDeniedRequests = 0;
    var amountOfUntreatedRequests = 0;

    $(".request").each(function (index, value) {

        if ($(value).data("fileid") === file.fileId) {

            $(value).removeClass("hide");

            if (isEven(selectedIndex)) {
                $(value).addClass("evenRowColor");
            } else {
                $(value).addClass("oddRowColor");
            }

            switch($(value).data("status")) {
                case "Approved":
                    amountOfApprovedRequests++;
                    break;
                case "Rejected":
                    amountOfDeniedRequests++;
                    break;
                case "Untreated":
                    amountOfUntreatedRequests++;
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