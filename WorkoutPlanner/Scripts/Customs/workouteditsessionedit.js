var config = {
    connectWith: "#garbage-container",
    placeholder: "ui-state-highlight",
    cursorAt: { top: 0, left: 0 },
    tolerance: "pointer",
    handle: ".draghandle",
    receive: function (event, ui) {


        $(this).children('li.ui-draggable').each(function () {

            $(this).removeClass('ui-draggable');
            $(this).children('.draghandle').after('<span class="opendetailworkoutexercise"/>');
            /*
            $(this).removeAttr('style');
            $(this).closest('ul').sortable();
            */
        });
    }
};

$(document).ready(function () {

    // Let you drag the exercise from the right side to left side (will do a copy)
    $(".exercises-container-source li").draggable({
          helper: "clone"
        , connectToSortable: ".workout-session-exercise-container"
        , placeholder: "ui-state-highlight"
        , handle: ".draghandle"
        , cursorAt: { top: 0, left: 0 }
        , tolerance: "pointer"
    });

    // Let drag left exercise to it's session
    $('.workout-session-exercise-container').each(function () {
        var configuration = $.extend({ connectWith: '#' + $(this).attr('id') }, config);
        $(this).sortable(configuration);
    });

    $('#btnSave').click(save);
    if (Application.Messages.SavedMessage!="") {
        successMessage(Application.Messages.SavedMessage);
    }
});

// Let you drag for the garbage
$("#garbage-container").sortable({
    connectWith: "#garbage-container"
    , placeholder: "ui-state-highlight-garbage"
    , handle: ".draghandle"
    , cursorAt: { top: 0, left: 0 }
    , tolerance: "pointer"
    , receive: function (event, ui) {
            $(this).html('');
    }
});

function save() {
    var json = createJsonForSessionExercises();
    $('#SessionsString').val(JSON.stringify(json));
}

function createJsonForSessionExercises() {
    var json = {sessions:[]};
    $('.workout-session-container ul').each(function () {
        var idSession = $(this).attr('data-id');
        var session = { id: idSession, exercises:[] };

        $(this).find('li').each(function () {
            var exercise = { idexercise: $(this).attr('data-exercise-id'), idsessionexercise: $(this).attr('data-session-exercise-id') };
            session.exercises.push(exercise);
        });

        json.sessions.push(session);
    });
    return json;
}

$(document).on('click', '.opendetailworkoutexercise', function () {
    var toSetId = $(this).parent();
    var sessionExerciseId = toSetId.attr('data-session-exercise-id');
    var exerciseId = toSetId.attr('data-exercise-id');
    var workoutsessionId = $(this).parent().parent().attr('data-id');

    if ($('#detailExercise').is(':visible') && sessionExerciseId == $('#detailExercisePartial #Id').val()) {
        $('#detailExercise').slideUp('slow');
        return;
    } else {

    }
    if (sessionExerciseId == "") {
        //Nouveau
    } else {
        //Modifier
    }
    $('#right-panel-designer-workout .loadingAnimation').fadeIn();
    $.ajax(
            {
                url: "/WorkoutSessionExercise/GetWorkoutSessionExercisePartial",
                type: "POST",
                data: { 'idSessionExercise': sessionExerciseId, 'idExercise': exerciseId, 'workoutSessionId': workoutsessionId },
                success: function (response, status, xhr) {
                    var jqContainer = $('#detailExercisePartial');
                    jqContainer.html(response.Content);
                    if (response.IsNew == true) {
                        $(toSetId).attr('data-session-exercise-id', response.IdWorkoutSessionExercise);
                    }
                    $('#detailExercise').slideDown('slow');
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    errorMessage(textStatus);
                },
                complete: function () {
                    $('#right-panel-designer-workout .loadingAnimation').fadeOut();
                }
            }
        );
});

$(document).on('click', '#btnSaveSessionExerciseDetail', function () {
    var toJson = BuildJsonForWorkoutSessionExercises();
    var button = $(this);
    $(button).attr('disabled', 'disabled');
    $('#detailExercisePartial p').append('<div class="loadingAnimation" />');
    $.ajax(
            {
                url: "/WorkoutSessionExercise/SaveWorkoutSessionExercisePartial",
                type: "POST",
                data: toJson,
                success: function (response, status, xhr) {
                    if (response.Status == "Saved") {
                        successMessage("Sessions saved");
                        $('#detailExercise').slideUp('slow');
                    } else {
                        errorMessage("Sessions have not been saved");
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    if (errorThrown == "Forbidden") {
                        errorMessage("Forbidden access");
                    } else {
                        errorMessage('An error occurred please retry.');
                    }
                },
                complete: function () {
                    $(button).removeAttr('disabled');
                    $('#detailExercisePartial p div').removeClass('loadingAnimation');
                }
            }
        );
    return false;//Prevent button to submit the form
});

function BuildJsonForWorkoutSessionExercises() {
    var json = {
        "Id": $('#detailExercisePartial [name="Id"]').val()
        , "Repetitions": $('#detailExercisePartial [name="Repetitions"]').val()
        , "Weights": $('#detailExercisePartial [name="Weights"]').val()
        , "Tempo": $('#detailExercisePartial [name="Tempo"]').val()
        , "RestBetweenSetTicks": $('#detailExercisePartial [name="RestBetweenSetTicks"]').val()
    };
    return json;
}