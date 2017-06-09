$(document).ready(function () {

    $(document).tooltip();

    var table = gridView();

    $("div.toolbar").html('<a class="btn btn-success btn-xs btn-flat md-trigger" data-modal="form-primary" href="#" title="Agregar una nueva aplicación" typebutton="Add"><i class="fa fa-plus"></i></a>');

    var validator = $('#modalForm').validate({

        errorClass: 'errorText',
        validClass: 'validText',
        errorElement: 'em',
        rules: {
            Nombre: {
                required: true
            },
            Clave: {
                required: true
            },
            Orden: {
                required: true,
                number: true,
                min: 1,
                max: 20
            }
        },
        messages: {
            Nombre: {
                required: 'Ingrese el nombre'
            },
            Clave: {
                required: 'Ingrese la clave'
            },
            Orden: {
                required: 'Ingrese el orden',
                min: 'El mínimo valor a ingresar es 1',
                max: 'El máximo valor a ingresar es 30'
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

            var selectedPerfil = null;

            selectedPerfil = [];

            $(":checkbox:checked").each(function () {
                selectedPerfil.push($(this).attr('value'));
            });

            var obj = {
                id: $('#aplicacionId').val(),
                nombre: $('#nombre').val(),
                clave: $('#clave').val(),
                orden: $('#orden').val(),
                selectedPerfil: selectedPerfil
            };

            $.ajax({
                type: 'POST',
                url: '/Administracion/Admin/Aplicaciones',
                data: obj,
                success: function () {

                    $('#modalForm').hide();

                    swal("Listo!", "Su información fue guardada correctamente", "success");

                    table.ajax.reload();
                },
                error: function (data) {

                    swal("Error!", "Se ha producido un error al registrar la información", "error");
                }
            });
        }
    });

    $(document).on('click', 'a[typebutton=Add]', function () {

        $.getJSON('/Administracion/Admin/AddAplicacion', function (data) {

            popUp();

            $('#aplicacionId').val(data.Id);
            $('#nombre').val(data.Nombre);
            $('#clave').val(data.Clave);
            $('#orden').val(data.Orden);
        });
    });

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $.getJSON('/Administracion/Admin/EditAplicacion/' + $(this).attr('data-value'), function (data) {

            popUp();

            $('#aplicacionId').val(data.Id);
            $('#nombre').val(data.Nombre);
            $('#clave').val(data.Clave);
            $('#orden').val(data.Orden);

            $.each(data.SelectedPerfil, function (i, item) {

                $('[value=' + item + ']').prop('checked', true);

            });
        });
    });

    $(document).on('click', 'a[typebutton=Delete]', function () {

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará la aplicación",
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
                    url: '/Administracion/Admin/DeleteAplicacion/' + id,
                    success: function () {

                        table.ajax.reload();

                        swal("Eliminado!", "La aplicación fue eliminada de forma correcta", "success");
                    },
                    error: function (data) {

                        swal("Error!", "La aplicación no puede ser eliminada", "error");
                    }
                });
            });
    });
});

$('#gridView').on('draw.dt', function () {
    $('.md-trigger').modalEffects();
});

function gridView() {

    var table = $('#gridView').DataTable({
        "ajax": "/Administracion/Admin/GetAplicaciones",
        "columns": [
            { "data": "Nombre" },
            { "data": "Orden" },
            { "data": "Accion" }
        ],
        "order": [[1, "asc"]],
        "columnDefs": [
            {
                "targets": [2],
                "searchable": false,
                "sortable": false
            }
        ],
        "dom": '<"toolbar">',
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

    $('#nombre').val("");
    $('#clave').val("");
    $('#orden').val("");

    $(":checkbox").each(function () {

        $(this).prop('checked', false);

    });

    form.find(".errorText").removeClass("errorText");
    form.find(".validText").removeClass("validText");
    form.find("em").remove();

    $('#modalForm').show();
}