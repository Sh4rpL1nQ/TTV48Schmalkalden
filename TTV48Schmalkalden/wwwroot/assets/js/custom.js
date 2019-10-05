

/*===================================================================================*/
/*	CUSTOM JS/JQUERY SCRIPTS
/*===================================================================================*/

// Insert your own scripts in here!
$(document).ready(function () {
    $(document).on("click", ".modalLink", function () {

        var passedID = $(this).data('id');
        $(".modal-body .hiddenid").val(passedID);
    });

    $('.chosen-select').chosen({ width: '100%' });

    var isMobile = {
        Android: function () {
            return navigator.userAgent.match(/Android/i);
        },
        BlackBerry: function () {
            return navigator.userAgent.match(/BlackBerry/i);
        },
        iOS: function () {
            return navigator.userAgent.match(/iPhone|iPad|iPod/i);
        },
        Opera: function () {
            return navigator.userAgent.match(/Opera Mini/i);
        },
        Windows: function () {
            return navigator.userAgent.match(/IEMobile/i) || navigator.userAgent.match(/WPDesktop/i);
        },
        any: function () {
            return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
        }
    };

    if (isMobile) {
        $('.chosen-select').css("display", "none");
    } else {
        $('.chosen-select').css("display","block");
    }

    $('.closedAtBeginning').click();
});