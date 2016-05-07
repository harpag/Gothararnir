//Hnappur til þess að búa til nýtt verkefni
$(document).ready(function () {
    $("#btn-newAssignment").click(function () {
        window.location.href = 'CreateAssignment.cshtml';
    });
});

//til að velja allt í lista 
$(function () {
    $('#selectAll').click(function () {

        $('[id*=UserList]').prop("checked", this.checked);
    });
});