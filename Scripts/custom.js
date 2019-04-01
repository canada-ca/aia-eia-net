initWetCtrls();

// postback call
var prm = Sys.WebForms.PageRequestManager.getInstance();
prm.add_endRequest(initWetCtrls);

// init function
function initWetCtrls() {

    // specific class for disabling validation
    window['wb-frmvld'] = { 'ignore': '.ignore' };

    // wrap form with class for validation
    var frmvldCount = $('div.wb-frmvld').length;
    if (frmvldCount === 0) {
        $('form').wrap('<div class="wb-frmvld"></div>');
    }

    // event for checkboxlist and radiobuttonlist
    $('.checkbox, .checkbox-inline, .radio, radio-inline').click(function () {
        var parent = $(this).closest('fieldset');
        var input = $(this).find('input');
        var count = parent.find('input:checked').length;
        var firstInput = parent.find('input').eq(0);
        var error = parent.find('strong.error');
        if (count > 0) {
            firstInput.addClass('ignore');
            error.hide();
        }
        else {
            firstInput.removeClass('ignore');
            error.show();
        }
    });
}