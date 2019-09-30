

/*===================================================================================*/
/*	CUSTOM JS/JQUERY SCRIPTS
/*===================================================================================*/

// Insert your own scripts in here!
$(document).ready(function () {
    $(document).on("click", ".modalLink", function () {
        console.log("HI");
        var passedID = $(this).data('id');
        $(".modal-body .hiddenid").val(passedID);
    });

    $('.chosen-select').chosen({ width: '100%' });
});