﻿var dataTable;

$(document).ready(function () {
    cargarDatatable();
});


function cargarDatatable() {
    dataTable = $("#tblPages").DataTable({
        "ajax": {
            "url": "/admin/pageClass/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "25%" },
            { "data": "description", "width": "25%" },
            {
                "data": "status", "width": "15%",
                "render": function (currentStatus) {
                    if (currentStatus == true) {
                        return "Active"
                    } else {
                        return "Inactive"
                    }
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/admin/pageClass/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-edit"></i> Edit
                                </a>
                                &nbsp;
                                <a onclick=Delete("/admin/pageClass/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-trash-alt"></i> Delete
                                </a>
                                &nbsp;
                                <a href="/admin/pageClass/ViewPage/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-trash-alt"></i> View page
                                </a>
                            </div>
                            `;
                }, "width": "30%"
            }
        ],
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
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
        });
    });
}