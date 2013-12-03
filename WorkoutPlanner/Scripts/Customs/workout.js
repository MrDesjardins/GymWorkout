$(document).on("click", "#deletebutton", function (e) {
    var urlToDelete = $(this).parent().attr('href');
    confirmMessage("Are you sure you want to delete this workout?",
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


$(document).ready(function () {
  

});