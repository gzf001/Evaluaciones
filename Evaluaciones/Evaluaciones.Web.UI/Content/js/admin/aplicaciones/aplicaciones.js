jQuery(document).ready(function () {

    var table = gridView();

    $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar aplicación" typebutton="Add"><i class="fa fa-plus"></i></a>');

    var validator = $('#formModal').validate({

        errorClass: 'state-error',
        validClass: 'state-success',
        errorElement: 'em',
        rules: {
            Nombre: {
                required: true
            },
            Clave: {
                required: true
            },
            Fa_Icon: {
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
            Fa_Icon: {
                required: 'Ingrese el nombre del ícono (font awesome)'
            },
            Orden: {
                required: 'Ingrese el orden',
                min: 'El mínimo valor a ingresar es 1',
                max: 'El máximo valor a ingresar es 20'
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

            var selectedPerfil = null;

            selectedPerfil = [];

            $(":checkbox:checked").each(function () {
                selectedPerfil.push($(this).attr('value'));
            });

            var obj = {
                id: $('#aplicacionId').val(),
                nombre: $('#nombre').val(),
                clave: $('#clave').val(),
                fa_Icon: $('#faIcon').val(),
                orden: $('#orden').val(),
                selectedPerfil: selectedPerfil
            };

            $.ajax({
                type: "POST",
                url: "/Administracion/Admin/Aplicaciones",
                data: obj,
                success: function () {

                    table.ajax.reload();

                    $.magnificPopup.close();

                    swal("Listo!", "Su información fue guardada correctamente", "success");
                },
                error: function (data) {

                    swal("Error!", "Se ha producido un error al registrar la información", "error");
                }
            });
        }
    })

    $(document).on('click', 'a[typebutton=Add]', function () {

        $.getJSON('/Administracion/Admin/AddAplicacion', function (data) {

            popUp();

            $('#aplicacionId').val(data.Id);
            $('#nombre').val(data.Nombre);
            $('#clave').val(data.Clave);
            $('#faIcon').val(data.Fa_Icon);
            $('#orden').val(data.Orden);

            $(":checkbox").each(function () {

                $(this).prop('checked', false);

            });
        })
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $.getJSON('/Administracion/Admin/EditAplicacion/' + $(this).attr('data-value'), function (data) {

            popUp();

            $('#aplicacionId').val(data.Id);
            $('#nombre').val(data.Nombre);
            $('#clave').val(data.Clave);
            $('#faIcon').val(data.Fa_Icon);
            $('#orden').val(data.Orden);

            $.each(data.SelectedPerfil, function (i, item) {

                $('[value=' + item + ']').prop('checked', true);

            });
        });
    })

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
    })
})

$('#cancel').click(function (e) {

    e.preventDefault();

    $.magnificPopup.close();
})

function gridView() {

    var table = $('#gridView').DataTable({
        "ajax": "/Administracion/Admin/GetAplicaciones",
        "columns": [
            { "data": "Nombre" },
            { "data": "Orden" },
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