﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
    if(role != "Admin")
        dataTable.column(2).visible(false);
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/NationalParks/GetAllNationalParks",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "50%" },
            { "data": "state", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/nationalParks/Upsert/${data}" class='btn btn-success text-white'
                                    style='cursor:pointer;'> <i class='far fa-edit'></i></a>
                                    &nbsp;
                                <a onclick=Delete("/nationalParks/Delete/${data}") class='btn btn-danger text-white'
                                    style='cursor:pointer;'> <i class='far fa-trash-alt'></i></a>
                                    &nbsp;
                            </div>`;
                },
                "width": "30%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangetMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}