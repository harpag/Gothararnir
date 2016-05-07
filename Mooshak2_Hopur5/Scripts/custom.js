//Hnappur til þess að búa til nýtt verkefni
$(document).ready(function () {
    $("#btn-newAssignment").click(function () {
        window.location.href = 'CreateAssignment.cshtml';
    });
});

<<<<<<< HEAD
//til að velja allt í lista 
$(function () {
    $('#selectAll').click(function () {

        $('[id*=UserList]').prop("checked", this.checked);
    });
});
=======
$(document).ready(function () {
    var showChar = 150;
    var ellipsestext = "...";
    var moretext = "Show more";
    var lesstext = "Show less";
    $('.more').each(function () {
        var content = $(this).html();

        if (content.length > showChar) {

            var c = content.substr(0, showChar);
            var h = content.substr(showChar - 1, content.length - showChar);

            var html = c + '<span class="moreellipses">' + ellipsestext + '&nbsp;</span><span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;<a href="" class="morelink">' + moretext + '</a></span>';

            $(this).html(html);
        }
    });

    $(".morelink").click(function () {
        if ($(this).hasClass("less")) {
            $(this).removeClass("less");
            $(this).html(moretext);
        } else {
            $(this).addClass("less");
            $(this).html(lesstext);
        }
        $(this).parent().prev().toggle();
        $(this).prev().toggle();
        return false;
    });
});


>>>>>>> 0e15ac482c77a0c79972077b65253a1f4a21ed98
