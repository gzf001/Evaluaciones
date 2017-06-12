jQuery(document).ready(function () {

    $('#aplicacion').change(function () {

        if ($(this).val() == '-1') {

            $('#menuItems').html('');
        }
        else {

            carga();
        }
    })

    var validator = $('#formModal').validate({

        errorClass: 'state-error',
        validClass: 'state-success',
        errorElement: 'em',
        rules: {
            Nombre: {
                required: true,
            },
            Titulo: {
                required: true
            }
        },
        messages: {
            Nombre: {
                required: 'Ingrese el nombre'
            },
            Titulo: {
                required: 'Ingrese el título'
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
                aplicacionId: $('#aplicacion').val(),
                menuItemId: $('#parentId').val(),
                id: $('#id').val(),
                nombre: $('#nombre').val(),
                informacion: $('#informacion').val(),
                faIcon: $('#faIcon').val(),
                url: $('#url').val(),
                visible: $("#visible").prop("checked"),
                orden: 0
            };

            $.ajax({
                type: "POST",
                url: "/Administracion/Admin/ItemsMenu",
                data: obj,
                success: function () {

                    $.magnificPopup.close();

                    swal("Listo!", "Su información fue guardada correctamente", "success");

                    carga();
                },
                error: function (data) {

                    swal("Error!", "Se ha producido un error al registrar la información", "error");
                }
            });
        }
    });

    $("#tree").fancytree({
        clickFolderMode: 2,
        activate: function (event, data) {

            $('#url').val(data.node.title)
        }
    })

    $(document).on('click', 'a[typeButton=Add]', function () {

        $('#parentId').val($(this).attr('data-value'));
        $('#id').val(generateId());

        popUp();

    })

    $(document).on('click', 'a[typeButton=Edit]', function () {

        var parentId = $(this).attr('data-parent');
        var menuItemId = $(this).attr('data-value');

        $.getJSON('/Administracion/Admin/GetItemMenu/' + $('#aplicacion').val() + '/' + menuItemId, function (data) {

            popUp();

            $('#parentId').val(parentId);
            $('#id').val(menuItemId);
            $('#nombre').val(data.Nombre);
            $('#url').val(data.Url);
            $('#informacion').val(data.Informacion);
            $('#icono').val(data.FaIcon);
            $('#visible').prop("checked", data.Visible);
        });
    })

    $(document).on('click', 'a[typeButton=Delete]', function () {

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará el ítem de menú y todos sus relacionados",
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

                        swal("Eliminado!", "La aplicación fue eliminada de forma correcta", "success");

                        carga();
                    },
                    error: function (data) {

                        swal("Error!", "El ítem de menú no puede ser eliminado", "error");
                    }
                });
            })
    })

    $('#cancelOrder').click(function () {

        $('#menuItems').nestable('destroy');

        carga();
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
        })
    })

    $('#cancel').click(function (e) {

        e.preventDefault();

        $.magnificPopup.close();
    })
})

function carga() {

    $.getJSON('/Administracion/Admin/GetItemsMenu/' + $('#aplicacion').val(), function (data) {

        $('#menuItems').html(data);

        menu();
    })
}

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
    }

    $('#menu').nestable({
        group: 1
    }).on('change', updateOutput);

    updateOutput($('#menu').data('output', $('#menuJson')));

    $('a[title!=""]').tooltipster();
}

function popUp() {

    var form = $('#modal-form');

    $('#nombreAplicacion').val($("#aplicacion option:selected").text());

    $('#nombre').val("");
    $('#url').val("");
    $('#informacion').val("");
    $('#icono').val("");
    $('#visible').prop("checked", false);

    form.find(".state-error").removeClass("state-error");
    form.find(".state-success").removeClass("state-success");
    form.find("em").remove();

    $("#tree").fancytree("getTree").activateKey("0");

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