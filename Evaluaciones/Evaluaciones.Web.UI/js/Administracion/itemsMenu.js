jQuery(document).ready(function () {

    var validator = $('#modalForm').validate({

        errorClass: 'errorText',
        validClass: 'validText',
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
                aplicacionId: $('#aplicacion').val(),
                menuItemId: $('#menuItemId').val(),
                id: $('#id').val(),
                nombre: $('#nombre').val(),
                informacion: $('#informacion').val(),
                icono: $('#icono').val(),
                url: $('#url').val(),
                visible: $("#visible").prop("checked"),
                muestraMenus: true,
                orden: 0
            };

            $.ajax({
                type: "POST",
                url: "/Administracion/Admin/ItemsMenu",
                data: obj,
                success: function () {

                    $('#modalForm').hide();

                    swal("Listo!", "Su información fue guardada correctamente", "success");

                    carga();

                    menu();
                },
                error: function (data) {

                    swal("Error!", "Se ha producido un error al registrar la información", "error");
                }
            });
        }
    })

    $('#aplicacion').change(function () {

        if ($(this).val() == '-1') {

            $('#menuItems').html('');
        }
        else {

            carga();
        }
    })

    $(document).on('click', 'a[typeButton=Add]', function () {

        $('#menuItemId').val($(this).attr('data-value'));
        $('#id').val(generateId());

        popUp();

    })

    $(document).on('click', 'a[typeButton=Edit]', function () {

        $('#menuItemId').val($(this).attr('data-parent'));
        $('#id').val($(this).attr('data-value'));

        popUp();

        $.getJSON('/Administracion/Admin/GetItemMenu/' + $('#aplicacion').val() + '/' + $(this).attr('data-value'), function (data) {

            $('#nombre').val(data.Nombre);
            $('#informacion').val(data.informacion);
            $('#icono').val(data.Icono);
            $('#url').val(data.Url);
            $("#visible").prop("checked", data.visible);
        });
    })

    $(document).on('click', 'a[typeButton=Delete]', function () {

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará el ítem y todos sus relacionados",
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
                    url: '/Administracion/Admin/DeleteItemsMenu/' + $('#aplicacion').val() + '/' + id,
                    success: function () {

                        swal("Eliminado!", "El ítem fue eliminado correctamente", "success");

                        carga();

                        menu();
                    },
                    error: function (data) {

                        swal("Error!", "El ítem no puede ser eliminado", "error");
                    }
                });
            });
    })

    $('#cancelOrder').click(function () {

        $('#menu').nestable('destroy');

        carga();

        menu();
    })

    $('#saveOrder').click(function () {

        var obj = {
            data: JSON.stringify(eval("(" + $('#menuJson').val() + ")"))
        };

        $.ajax({
            type: "POST",
            url: "/Administracion/Admin/GetOrder",
            data: obj,
            success: function () {

                swal("Listo!", "El orden fue guardado correctamente", "success");
            },
            error: function (data) {

                swal("Error!", "Se ha producido un error al registrar el orden", "error");
            }
        });
    })

    $('label.tree-toggler').click(function () {
        var icon = $(this).children(".fa");
        if (icon.hasClass("fa-folder-open-o")) {
            icon.removeClass("fa-folder-open-o").addClass("fa-folder-o");
        } else {
            icon.removeClass("fa-folder-o").addClass("fa-folder-open-o");
        }

        $(this).parent().children('ul.tree').toggle(300, function () {
            $(this).parent().toggleClass("open");
            $(".tree .nscroller").nanoScroller({ preventPageScrolling: true });
        });
    })

    $('#path a[data-value]').on('click', function () {

        $('#url').val($(this).attr('data-value'));

    })
})

function menu() {

    var updateOutput = function (e) {

        if (e.length) {
            var a = e;
        }
        else {
            var b = $(e.target);
        }

        var list = e.length ? e : $(e.target);

        output = list.data('output');

        if (output != null) {

            if (window.JSON) {

                output.val(window.JSON.stringify(list.nestable('serialize')));

            } else {

                output.val('JSON browser support required for this demo.');
            }
        }

        $('.md-trigger').modalEffects();
    };

    $('#menu').nestable({
        group: 1
    }).on('change', updateOutput);

    updateOutput($('#menu').data('output', $('#menuJson')));
}

function carga() {

    $.getJSON('/Administracion/Admin/GetItemsMenu/' + $('#aplicacion').val(), function (data) {

        $('#menuItems').html(data);

        menu();
    });
}

function popUp() {

    var form = $('#modalForm');

    $('#nombreAplicacion').val($('#aplicacion option:selected').text());

    $('#nombre').val("");
    $('#informacion').val("");
    $('#icono').val("");
    $('#url').val("");
    $("#visible").prop("checked", false);

    form.find(".errorText").removeClass("errorText");
    form.find(".validText").removeClass("validText");
    form.find("em").remove();

    form.show();
}