var id = -1;

function removeInvitee(e) {
    id = $(e).attr("data-id");
    $("#myModal").modal('show');
}

function confirmRemove(e) {
    $('#myModal').modal("hide");
    $.ajax({
        type: "POST",
        url: "/Support/RemoveInvitee",
        datatype: 'json',
        data: { id: id }
    }).done(function (data) {
        if (data.success == "true") {
            location.reload();
        } else {
            $("#modalFailed").modal("show");
        }
    });
}

$(document).ready(function () {
    var text;
    $("#searchLastName").keyup(function () {
        $(".inviteeRow").show();
        text = $("#searchLastName").val().toLowerCase();
        if (text.length > 0) {
            $('.inviteeLastName').each(function (i, obj) {
                console.log($(this).html());
                if ($(this).html().toLowerCase().indexOf(text) == -1) {
                    $(this).closest(".inviteeRow").hide();
                }
            });
        }
    });

    $("#searchFirstName").keyup(function () {
        $(".inviteeRow").show();
        text = $("#searchFirstName").val().toLowerCase();
        if (text.length > 0) {
            $('.inviteeFirstName').each(function (i, obj) {
                console.log($(this).html());
                if ($(this).html().toLowerCase().indexOf(text) == -1) {
                    $(this).closest(".inviteeRow").hide();
                }
            });
        }
    });

});