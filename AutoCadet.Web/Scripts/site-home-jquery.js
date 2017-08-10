(function($) {
    $(function() {
        $('[data-toggle="popover"]').popover();
    });

    $("#addCommentForm").submit(function(e) {
        e.preventDefault();
        $.ajax({
            type: "POST",
            url: "/AddComment",
            data: $("#addCommentForm").serialize(),
            success: function(data) {
                $("#addCommentError").hide();
                $("#addCommentSuccess").hide();
                $("#addCommentExists").hide();
                if (data && data.success) {
                    $("#addCommentSuccess").show();
                } else if (data && data.isSame) {
                    $("#addCommentExists").show();
                } else {
                    $("#addCommentError").show();
                }
            }
        });
        return false; // prevent default action
    });

    $("#callMeForm").submit(function(e) {
        e.preventDefault();
        $.ajax({
            type: "POST",
            url: "/CallMe",
            data: $("#callMeForm").serialize(),
            success: function(data) {
                $("#addCallMeError").hide();
                $("#addCallMeSuccess").hide();
                $("#addCallMeExists").hide();
                if (data && data.success) {
                    $("#addCallMeSuccess").show();
                } else if (data && data.isSame) {
                    $("#addCallMeExists").show();
                } else {
                    $("#addCallMeError").show();
                }
            }
        });
        return false; // prevent default action
    });

    $("#showCallMe").click(function() {
        $("#callMeModal").modal("show");

        $.ajax({
            type: "GET",
            url: "/getcallme",
            success: function(data) {
                if (data) {
                    const place = $($("#callMePlaceholder")[0]);
                    place.append(data);
                }
            }
        });
    });


    $("body").on("click",
        "#callMeFormBtn",
        function(e) {
            e.preventDefault();
            $.ajax({
                type: "POST",
                url: "/CallMe",
                data: $("#callMeFormModal").serialize(),
                success: function(data) {
                    $("#addCallMeErrorModal").hide();
                    $("#addCallMeSuccessModal").hide();
                    $("#addCallMeExistsModal").hide();
                    if (data && data.success) {
                        $("#addCallMeSuccessModal").show();
                    } else if (data && data.isSame) {
                        $("#addCallMeExistsModal").show();
                    } else {
                        $("#addCallMeErrorModal").show();
                    }
                    return false;
                }
            });
            return false; // prevent default action
        });

    $.ajax({
        type: "GET",
        url: "/getakciya",
        success: function(data) {
            if (data) {
                $($("#akciyaLink")[0]).append(data);
            }
        }
    });

    $.ajax({
        type: "GET",
        url: "/getphonenumber",
        success: function(data) {
            if (data) {
                $($("#phoneNumber")[0]).append(data);
            }
        }
    });
})(jQuery);