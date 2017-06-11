jQuery(document).ready(function () {

    $(document).tooltip();

    var table;

    $('#aplicacion').change(function () {

        table = gridView();
    })
})

$('#save').click(function () {

    var objs = [];

    var rolId = $("#RolId").val();
    var aplicacionId = $('#aplicacion').val();

    objs.push({
        rolId: rolId,
        aplicacionId: aplicacionId
    });

    $("#gridView tbody tr td label input").each(function () {

        if ($(this).prop("checked")) {

            objs.push({
                rolId: rolId,
                aplicacionId: aplicacionId,
                menuItemId: $(this).attr("data-parent"),
                accionCodigo: $(this).attr("data-value")
            })
        }
    });

    $.ajax({
        type: "POST",
        url: "/Administracion/Admin/GetPermissions/",
        data: { 'model': objs },
        success: function () {

            swal("Listo!", "Los permisos fueron guardados correctamente", "success");
        },
        error: function (data) {

            swal("Error!", "Se ha producido un error al registrar los permisos", "error");
        }
    })
})

function gridView() {

    var table = $('#gridView').DataTable({

        "ajax": "/Administracion/Admin/GetRolAccion/" + $('#RolId').val() + "/" + $('#aplicacion').val(),
        "columns": [
            { "data": "MenuItemNombre" },
            { "data": "AccionNombre" }
        ],
        "destroy": true,
        "paging": false,
        "ordering": false,
        "info": false,
        "sDom": ''
    });

    return table;
}