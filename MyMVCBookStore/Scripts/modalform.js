$(function () {

    $.ajaxSetup({ cache: false });

    $("a[data-modal]").on("click", function (e) {
        $("#myModalContent").load(this.href, function () {
            

            $("#myModal").modal({
                keyboard: true
            }, "show");

            bindForm(this);
        });

        return false;
    });
});

function bindForm(dialog) {
    
    $("#crtForm", dialog).submit(function () {
        var myform = document.getElementById("crtForm");
        var fdata = new FormData(myform);
       $.ajax({
            url: this.action,
            data: fdata,
            cache: false,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (result) {
                if (result.success) {
                    $("#myModal").modal("hide");
                    location.reload();
                } else {
                    $("#myModalContent").html(result);
                    bindForm();
                }
            }
        });
        return false;
    });
}