(function ($) {

    $('#addCommentForm').submit(function (e) {
        e.preventDefault();
        $.ajax({
            type: 'POST',
            url: '/Home/AddComment',
            data: $('#addCommentForm').serialize(),
            success: function(data) {
                $('#addCommentError').hide();
                $('#addCommentSuccess').hide();
                $('#addCommentExists').hide();
                if (data && data.success) {
                    $('#addCommentSuccess').show();
                } else if (data && data.isSame) {
                    $('#addCommentExists').show();
                } else {
                    $('#addCommentError').show();
                }
            }
        });
        return false; // prevent default action
    });

    $('#callMeForm').submit(function (e) {
        e.preventDefault();
        $.ajax({
            type: 'POST',
            url: '/Home/CallMe',
            data: $('#callMeForm').serialize(),
            success: function(data) {
                $('#addCallMeError').hide();
                $('#addCallMeSuccess').hide();
                $('#addCallMeExists').hide();
                if (data && data.success) {
                    $('#addCallMeSuccess').show();
                } else if (data && data.isSame) {
                    $('#addCallMeExists').show();
                } else {
                    $('#addCallMeError').show();
                }
            }
        });
        return false; // prevent default action
    });

})(jQuery);