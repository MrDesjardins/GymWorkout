//Global object for the Application with global values
var Application = {
    Localization: {
        Language: "fr"
    },
    Messages: {}
};


//Initialize every thing that need to be loaded when the ui start. This method
//should initialize UI elements
$(document).ready(function () {
    initUiControls();
});

//Attach the date picker to all date picker fields
function initUiControls() {
    $(".ui-date-picker").datepicker({ showButtonPanel: true });
    var regionalSetting = $.datepicker.regional[window.Application.Localization.Language];
    var localisation = 'en-GB'; //Because English is the default one and is not inside the i18n file
    if (typeof (regionalSetting) != "undefined") {
        localisation = window.Application.Localization.Language; 
    }
    $.datepicker.setDefaults($.datepicker.regional[localisation]);
    $.datepicker.setDefaults({ dateFormat: $.datepicker.regional[localisation].dateFormat });
}

// ------------------ message box -----------------------

function confirmMessage(message, buttons) {
    noty({
        text:message,
        type: 'warning',
        dismissQueue: false, /*No repeat*/
        modal: true, /*Block double click*/
        force:true,
        layout: 'bottomLeft',
        theme: 'defaultTheme',
        buttons: buttons,
        
    });
}

function errorMessage(message) {
    noty({
        text: message,
        type: 'error',
        dismissQueue: true,
        layout: 'bottomLeft',
        theme: 'defaultTheme',
        timeout: 5000
    });
}

function warningMessage(message) {
    noty({
        text: message,
        type: 'warning',
        dismissQueue: true,
        layout: 'bottomLeft',
        theme: 'defaultTheme',
        timeout: 5000
    });
}

function infoMessage(message) {
    noty({
        text: message,
        type: 'information',
        dismissQueue: true,
        layout: 'bottomLeft',
        theme: 'defaultTheme',
        timeout: 5000
    });
}

function successMessage(message) {
    noty({
        text: message,
        type: 'success',
        dismissQueue: true,
        layout: 'bottomLeft',
        theme: 'defaultTheme',
        timeout: 5000  
    });
}