//$("#newBtn").click(function (e) {
//    $(".modal-header").load("/Books/Create");
//});
$(function () {

    $.ajaxSetup({ cache: false });

    $("#newBtn").on("click", function (e) {

        // hide dropdown if any
        

       
        $('.modal-header').load("/Books/Create", function () {
           

            $('#myModal').modal({
                /*backdrop: 'static',*/
                keyboard: true
            }, 'show');

            bindForm(this);
        });

        return false;
    });

});

function bindForm(dialog) {
   
    $("myModal").dialog.submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    //Refresh
                    location.reload();
                } else {
                    $('#table table-striped grid-table').html(result);
                    bindForm(dialog);
                }
            }
        });
        return false;
    });
}