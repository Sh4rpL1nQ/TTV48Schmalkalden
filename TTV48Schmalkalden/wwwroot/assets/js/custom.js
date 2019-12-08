

/*===================================================================================*/
/*	CUSTOM JS/JQUERY SCRIPTS
/*===================================================================================*/

// Insert your own scripts in here!
$(document).ready(function () {
    $(document).on("click", ".modalLink", function () {

        var passedID = $(this).data('id');
        $(".modal-body .hiddenid").val(passedID);
    });
});