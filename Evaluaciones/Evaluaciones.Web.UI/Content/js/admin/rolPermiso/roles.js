$(document).ready(function () {

    $(document).tooltip();

    var table;

    var validator = $('#formModal').validate({

        errorClass: 'errorText',
        validClass: 'validText',
        errorElement: 'em',
        rules: {
            Nombre: {
                required: true
            }
        },
        messages: {
            Nombre: {
                required: 'Ingrese el nombre'
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).closest('.form-control').addClass(errorClass).removeClass(validClass);
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest('.form-control').removeClass(errorClass).addClass(validClass);
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
    });

    $('#ambito').change(function () {

        table = gridView();

        $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar rol" typebutton="Add"><i class="fa fa-plus"></i></a>');
    })

    $(document).on('click', 'a[typebutton=Add]', function () {

        if ($('#ambito').val() == '-1') {

            $('#modalForm').hide();

            swal("Error!", "Primero seleccione el ámbito", "error");

        }
        else {

            popUp();

            $('#rolId').val(generateId());
        }
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $.getJSON('/Administracion/Admin/GetRol/' + $(this).attr('data-value'), function (data) {

            popUp();

            $('#rolId').val(data.Id);
            $('#nombre').val(data.Nombre);
            $('#clave').val(data.Clave);

        });
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

    $('#cancel').click(function (e) {

        e.preventDefault();

        $.magnificPopup.close();
    })
});

function gridView() {

    var table = $('#gridView').DataTable({

        "ajax": "/Administracion/Admin/GetRoles/" + $('#ambito').val(),
        "destroy": true,
        "columns": [
            { "data": "Nombre" },
            { "data": "Clave" },
            { "data": "Accion" }
        ],
        "order": [[0, "asc"]],
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
        ]
    });

    return table;
}

function popUp() {

    var form = $('#modalForm');

    $('#ambitoCodigo').val($('#ambito option:selected').text());

    $('#nombre').val("");
    $('#clave').val("");

    form.find(".errorText").removeClass("errorText");
    form.find(".validText").removeClass("validText");
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