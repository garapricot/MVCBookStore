$(document).ready(function () {
    $('#book_grid').dataTable({
        "bPaginate": false,
        "bLengthChange": true,
        "ordering": false,
        "bInfo": false,
        "bAutoWidth": false,
        "searching": false,
        columnDefs: [
            { responsivePriority: 1, targets: 7 }
    ]
    });
});