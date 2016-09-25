$(document).ready(function () {
    $('#book_grid').dataTable({
        "bPaginate": false,
        "bLengthChange": false,
        filter: false,
        "bInfo": false,
        "bAutoWidth": false,
        "bSearch": false,
        columnDefs: [
            { responsivePriority: 1, targets: 8 }
    ]
    });
});