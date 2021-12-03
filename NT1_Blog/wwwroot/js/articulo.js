var dataTable;
$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    dataTable = $("#tblArticulos").DataTable({
        //llamado api
        "ajax": {
            "url": "/admin/articulos/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        // columnas
        "columns": [
            { "data": "id", "width": "5%"},
            { "data": "nombre", "width": "25%"},
            { "data": "categoria.nombre", "width": "15%"},
            { "data": "fechaCreacion", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `  <div class="text-center">
                               <a href='/Admin/Articulos/Edit/${data}' class='btn btn-succes text-white' style='cursor:pointer; width:100px'> Editar
                                </a>
                                &nbsp;
                              <div class="text-center">
                                <a onclick=Delete("/Admin/Articulos/Delete/${data}") 'class='btn btn-danger text-white' style='cursor:pointer; width:100px'> Eliminar
                                </a>
                                &nbsp;

                                `;
                },"width":"30%"
            }

        ],

        "language": {
            "emptyTable": "No hay registros"
        },
        "width": "100%"

    });
}

// sweetAlert para el delete 
function Delete(url) {
    swal({
        title: "Esta seguro de borrar?",
        text: "este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: "true",
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar",
        closeOnconfirm: true

    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            succes: function (data) {
                if (data.succes) {
                    toastr.succes(data.message);
                    dataTable.reload();
                }
                else {
                    toaster.error(data.message);
                }
            }
        });

    });
}