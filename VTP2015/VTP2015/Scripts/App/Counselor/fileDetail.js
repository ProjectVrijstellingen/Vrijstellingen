$(document).ready(function () {

    $("#btnShowOverview").click(showFileOverview);

    $(".nextEvidence").click(function (e) {
        e.preventDefault(); 
        showNextEvidence(this);
    });

    $(".previousEvidence").click(function(e) {
        e.preventDefault();
        showPreviousEvidence(this);
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

function showNextEvidence(sender) {

    var currentEvidenceIndexSpan = $(sender).parent().find(".currentEvidence");
    var amountOfEvidence = $(sender).parent().find(".amountOfEvidence").text();

    var currentEvidenceIndex = parseInt(currentEvidenceIndexSpan.text());

    var request = $(sender).parent().parent().parent().parent();
    var newCurrentEvidenceIndex;

    if (currentEvidenceIndex < amountOfEvidence) 
        newCurrentEvidenceIndex = currentEvidenceIndex + 1;
    else 
        newCurrentEvidenceIndex = 1;
    
    hideEvidence(request, currentEvidenceIndex);
    showEvidence(request, newCurrentEvidenceIndex);

    currentEvidenceIndexSpan.text(newCurrentEvidenceIndex);

}

function showPreviousEvidence(sender) {

    var currentEvidenceIndexSpan = $(sender).parent().find(".currentEvidence");
    var amountOfEvidence = $(sender).parent().find(".amountOfEvidence").text();

    var currentEvidenceIndex = parseInt(currentEvidenceIndexSpan.text());

    var request = $(sender).parent().parent().parent().parent();
    var newCurrentEvidenceIndex;
    
    if (currentEvidenceIndex > 1)
        newCurrentEvidenceIndex = currentEvidenceIndex - 1;
    else
        newCurrentEvidenceIndex = amountOfEvidence;

    hideEvidence(request, currentEvidenceIndex);
    showEvidence(request, newCurrentEvidenceIndex);

    currentEvidenceIndexSpan.text(newCurrentEvidenceIndex);

}
// "public" function for the fileoverview
function selectFileById(file) {
    $(".request").addClass("hide");

    $("#details").removeClass("hide");
    $("#name").text(file.name);

    var selectedIndex = 1; //starts at one for simplicity

    $(".request").each(function (index, value) {

        if ($(value).data("fileid") === file.fileId) {

            $(value).removeClass("hide");

            if (isEven(selectedIndex)) {
                $(value).addClass("evenRowColor");
            } else {
                $(value).addClass("oddRowColor");
            }

            showEvidence(value, 1); //show first evidence for request

            selectedIndex++;
        }

    });

}