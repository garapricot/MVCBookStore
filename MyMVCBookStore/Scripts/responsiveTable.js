$(document).ready(function () {
    $('#book_grid').dataTable({
        "bPaginate": false,
        "bLengthChange": false,
        "ordering": false,
        "bInfo": false,
        "bAutoWidth": false,
        "searching": false,
        columnDefs: [
            { responsivePriority: 1, targets: 8 }
    ]
    });
});