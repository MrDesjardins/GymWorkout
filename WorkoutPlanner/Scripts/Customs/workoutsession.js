$(document).ready(function () {

    $(".sortable tbody").sortable({
        placeholder: "ui-state-highlight-table"
        , handle: ".draghandle"
        , cursorAt: { top: 0, left: 0 }
        , tolerance: "pointer"
        , helper: fixHelper
    });
    $(".sortable tbody").disableSelection();

});

var fixHelper = function (e, ui) {
    ui.children().each(function () {
        $(this).width($(this).width());
    });
    return ui;
};

$(document).on("click", "#btnSaveOrder", function () {
    var button = $(this);
    $(button).attr('disabled', 'disabled');
    $('.loadingAnimationfixed').show();
    var listIdOrdered = [];
    $('#sessiongrid tbody tr').each(function () {
        listIdOrdered.push($(this).attr("data-workoutsessionid"));
    });

    /*var toSave = {
        'OrderedWorkoutSessionList': listIdOrdered
        ,'WorkoutId': $('#Id').val()
    }; 
    */


    var workoutSessionOrder = {
        WorkoutId: $('#Id').val(),
        OrderedWorkoutSessionList: listIdOrdered
    };
    $.ajax(
        {
            url: "http://localhost:9999/WorkoutSession/SaveWorkoutSessionOrder",
            type: "POST",
            data: JSON.stringify(workoutSessionOrder),
            contentType: 'application/json',
            success: function (response, status, xhr) {
                successMessage(response.Status);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (errorThrown == "Forbidden") {
                    errorMessage("Forbidden access");
                } else {
                    errorMessage('An error occurred please retry.');
                }
            }
            , complete: function () {
                $(button).removeAttr('disabled');
                $('.loadingAnimationfixed').hide();
            }
        }
    );

    return false; //Prevent button to submit the form
});

$(document).on("click", ".deleteButton", function (e) {
    var urlToDelete = $(this).parent().attr('href');
    confirmMessage("Are you sure you want to delete this workout session?",
         [
            {
                addClass: 'btn btn-primary', text: 'I confirm I want to delete', onClick: function ($noty) {
                    location.href = urlToDelete;
                    $noty.close();
                }
            },
            {
                addClass: 'btn btn-danger', text: 'Cancel, no delete', onClick: function ($noty) {
                    $noty.close();
                }
            }
         ]
    );
    return false;
});