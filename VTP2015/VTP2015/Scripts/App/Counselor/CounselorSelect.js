
$(document).ready(function () {
    $('.panel-groen').parent().addClass('hidden');

});

function toggle(panel) {
    if (panel.hasClass('hidden')) {
        panel.removeClass('hidden');
    } else {
        panel.addClass('hidden');
    }
}

function toggleGreen() {
    toggle($('.panel-groen').parent());
}

function toggleOrange() {
    toggle($('.panel-oranje').parent());
}

function toggleRed() {
    toggle($('.panel-rood').parent());
}