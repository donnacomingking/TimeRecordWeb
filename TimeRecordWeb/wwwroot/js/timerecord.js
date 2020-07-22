var table = $('#timerecord').DataTable({
    columnDefs: [
    ],
    paging: false,
    order: [[1, 'asc']],
    orderCellsTop: true,
    fixedHeader: true
});

$(document).ready(function () {
    var filterTitle = ["Employee Name", "Active"];
    $('#timerecord thead tr').clone(true).appendTo('#timerecord thead');
    $('#timerecord thead tr:eq(1) th').each(function (i) {
        var title = $(this).text();
        if (filterTitle.includes(title)) {
            $(this).html('<input class="tr-filter-input" type="text" placeholder="Filter by ' + title + '" />');
        }
        else {
            $(this).html('<span></span>');
        }

        $('input', this).on('keyup change', function () {
            if (table.column(i).search() !== this.value) {
                table
                    .column(i)
                    .search(this.value)
                    .draw();
            }
        });
    });  
});