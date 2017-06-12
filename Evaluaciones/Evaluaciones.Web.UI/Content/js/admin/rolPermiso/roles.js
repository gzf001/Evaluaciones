jQuery(document).ready(function () {

    var table;

    var validator = $('#formModal').validate({

        errorClass: 'state-error',
        validClass: 'state-success',
        errorElement: 'em',
        rules: {
            Nombre: {
                required: true,
            }
        },
        messages: {
            Nombre: {
                required: 'Ingrese el nombre'
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).closest('.field').addClass(errorClass).removeClass(validClass);
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest('.field').removeClass(errorClass).addClass(validClass);
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element.parent());
        },
        submitHandler: function (form) {

            var obj = {
                id: $('#rolId').val(),
                ambitoCodigo: $('#ambito').val(),
                nombre: $('#nombre').val(),
                clave: $('#clave').val(),
            };

            $.ajax({
                type: 'POST',
                url: '/Administracion/Admin/Roles',
                data: obj,
                success: function () {

                    $.magnificPopup.close();

                    swal("Listo!", "Su información fue guardada correctamente", "success");

                    table.ajax.reload();
                },
                error: function (data) {

                    swal("Error!", "Se ha producido un error al registrar la información", "error");
                }
            });
        }
    })

    $('#ambito').change(function () {

        table = gridView();

        $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar rol" typebutton="Add"><i class="fa fa-plus"></i></a>');
    })

    $(document).on('click', 'a[typebutton=Add]', function () {

        $.getJSON('/Administracion/Admin/AddRol/' + $('#ambito').val(), function (data) {

            popUp();

            $('#rolId').val(data.Id);
            $('#nombre').val(data.Nombre);
            $('#clave').val(data.Clave);

        }).fail(function () {

            swal("Error!", "Primero seleccione el ámbito", "error");

        })
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $.getJSON('/Administracion/Admin/EditRol/' + $(this).attr('data-value'), function (data) {

            popUp();

            $('#rolId').val(data.Id);
            $('#nombre').val(data.Nombre);
            $('#clave').val(data.Clave);

        })
    })

    $(document).on('click', 'a[typebutton=Delete]', function () {

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará el rol y todos sus permisos",
            type: "warning",
            showCancelButton: true,
            cancelButtonText: "Cancelar",
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Si, eliminalo",
            closeOnConfirm: false
        },
            function () {

                $.ajax({
                    type: 'GET',
                    url: '/Administracion/Admin/DeleteRol/' + id,
                    success: function () {

                        table.ajax.reload();

                        swal("Eliminado!", "El rol fue eliminado de forma correcta", "success");
                    },
                    error: function (data) {

                        swal("Error!", "El rol no puede ser eliminado", "error");
                    }
                });
            });
    })
})

$('#cancel').click(function (e) {

    e.preventDefault();

    $.magnificPopup.close();
})

function gridView() {

    var table = $('#gridView').DataTable({
        "ajax": "/Administracion/Admin/GetRoles/" + $('#ambito').val(),
        "columns": [
            { "data": "Nombre" },
            { "data": "Clave" },
            { "data": "Accion" }
        ],
        "destroy": true,
        "order": [[1, "asc"]],
        "columnDefs": [
            {
                "targets": [2],
                "searchable": false,
                "sortable": false
            }
        ],
        "iDisplayLength": 15,
        "aLengthMenu": [
            [15, 20, 25, 30, -1],
            [15, 20, 25, 30, "All"]
        ],
        "sDom": '<"dt-panelmenu clearfix"lfr>t<"dt-panelfooter clearfix"ip>'
    });

    return table;
}

function popUp() {

    var form = $('#modal-form');

    $('#ambitoCodigo').val($('#ambito option:selected').text());

    form.find(".state-error").removeClass("state-error");
    form.find(".state-success").removeClass("state-success");
    form.find("em").remove();

    $.magnificPopup.open({
        removalDelay: 500,
        items: {
            src: "#modal-form"
        },
        callbacks: {
            beforeOpen: function (e) {

                this.st.mainClass = "mfp-flipInX";
            }
        },
        midClick: true
    });
}