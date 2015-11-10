
$(document).ready(function () {
    if (!$('#btnGreen').hasClass('btn-success')) {
        $('.panel-groen').parent().addClass('hidden');
        $('.success').addClass('hidden');
    }
    if (!$('#btnOrange').hasClass('btn-warning')) {
        $('.panel-oranje').parent().addClass('hidden');
        $('.warning').addClass('hidden');
    }
    if (!$('#btnRed').hasClass('btn-danger')) {
        $('.panel-rood').parent().addClass('hidden');
        $('.danger').addClass('hidden');
    }
    
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
    toggle($('.success'));
}

function toggleOrange() {
    toggle($('.panel-oranje').parent());
    toggle($('.warning'));
}

function toggleRed() {
    toggle($('.panel-rood').parent());
    toggle($('.danger'));
}