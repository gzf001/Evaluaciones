jQuery(document).ready(function () {

    $('a[title!=""]').tooltipster();

    $('#formModal').validate({

        errorClass: 'state-error',
        validClass: 'state-success',
        errorElement: 'em',
        rules: {
            Rbd: {
                required: true
            },
            AreaGeograficaCodigo: {
                required: true
            },
            Nombre: {
                required: true
            },
            AutorizacionFecha: {
                required: true
            },
            AutorizacionNumero: {
                required: true
            }
        },
        messages: {
            Rbd: {
                required: 'Ingrese el R.B.D.'
            },
            AreaGeograficaCodigo: {
                required: 'Ingrese el área geográfica'
            },
            Nombre: {
                required: 'Ingrese el nombre'
            },
            AutorizacionFecha: {
                required: 'Ingrese la fecha de autorización'
            },
            AutorizacionNumero: {
                required: 'Ingrese el número de autorización'
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

            var areaGeografica = $("input[name='AreaGeograficaCodigo']:checked");

            var obj = {
                empresaId: $('#empresaId').val(),
                id: $('#centroCostoId').val(),
                nombre: $('#nombre').val(),
                rbd: $('#rbd').val(),
                sigla: $('#sigla').val(),
                areaGeograficaCodigo: $(areaGeografica).val(),
                regionCodigo: $('#region').val(),
                ciudadCodigo: $('#ciudad').val(),
                comunaCodigo: $('#comuna').val(),
                email: $('#email').val(),
                direccion: $('#direccion').val(),
                telefono1: $('#telefono1').val(),
                telefono2: $('#telefono2').val(),
                fax: $('#fax').val(),
                celular: $('#celular').val(),
                autorizacionNumero: $('#autorizacionNumero').val(),
                autorizacionFecha: $('#autorizacionFecha').val()
            };

            $.ajax({
                type: "POST",
                url: "/Administracion/Admin/CentrosCosto",
                data: obj,
                success: function (data) {

                    if (data == "200") {

                        $.magnificPopup.close();

                        gridView();

                        swal("Listo!", "Su información fue guardada correctamente", "success");
                    }
                    else {

                        swal("Error!", data, "error");
                    }
                },
                error: function (data) {

                    swal("Error!", "Se ha producido un error al registrar la información", "error");
                }
            })
        }
    })

    $('#empresaId').change(function () {

        gridView();
    })

    $(document).on('click', 'a[typebutton=Add]', function () {

        $('#searchCostCenter').show();

        $.getJSON('/Administracion/Admin/AddCentroCosto/' + $('#empresaId').val(), function (data) {

            popUp();

            loadData(data, true);

        });
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $('#searchCostCenter').hide();

        $.getJSON('/Administracion/Admin/GetCentroCosto/' + $('#empresaId').val() + '/' + $(this).attr('data-value'), function (data) {

            loadData(data, false);

            popUp();
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

    $('#searchCostCenter').click(function (e) {

        e.preventDefault();

        var rbd = $('#rbd').val();

        rbd = rbd.replace(/\./g, '').replace('-', '');

        $.getJSON('/Administracion/Admin/CentroCosto/' + $('#empresaId').val() + '/' + rbd, function (data) {

            if (data == '500') {

                $('#centroCostoId').val('');
                $('#rbd').val('');
                $('#nombre').val('');
                $('#areaGeografica').val('');
                $('#sigla').val('');
                $('#autorizacionFecha').val('');
                $('#autorizacionNumero').val('');
                $('#celular').val('');
                $('#telefono1').val('');
                $('#telefono2').val('');
                $('#fax').val('');
                $('#email').val('');
                $('#direccion').val('');
                $('#region').val('');
                $('#ciudad').val('');
                $('#comuna').val('');

                disabled(true);
            }
            else {

                loadData(data, false);
            }
        })
    })
})

$('#cancel').click(function (e) {

    e.preventDefault();

    $.magnificPopup.close();
})

function loadData(data, enabled) {

    $('#centroCostoId').val(data.Id);
    $('#razonSocial').val(data.Empresa.RazonSocial);
    $('#rbd').val(data.Rbd);
    $('#nombre').val(data.Nombre);

    var areaGeografica = $("input[name='AreaGeograficaCodigo'][value=" + data.AreaGeograficaCodigo + "]");

    $(areaGeografica).attr('checked', true);

    $('#sigla').val(data.Sigla);
    $('#autorizacionFecha').val(data.FechaAutorizacionString);
    $('#autorizacionNumero').val(data.AutorizacionNumero);
    $('#celular').val(data.Celular);
    $('#telefono1').val(data.Telefono1);
    $('#telefono2').val(data.Telefono2);
    $('#fax').val(data.Fax);
    $('#email').val(data.Email);
    $('#direccion').val(data.Direccion);
    $('#region').val(data.RegionCodigo);

    $.getJSON('/Evaluaciones/Ciudades?regionCodigo=' + data.RegionCodigo, function (ciudad) {

        loadSelect($('#ciudad'), ciudad, data.CiudadCodigo);
    });

    $('#ciudad').val(data.CiudadCodigo);

    $.getJSON('/Evaluaciones/Comunas/' + data.RegionCodigo + '/' + data.CiudadCodigo, function (comuna) {

        loadSelect($('#comuna'), comuna, data.ComunaCodigo);
    });

    $('#comuna').val(data.ComunaCodigo);

    disabled(enabled);
}

function disabled(enabled) {

    $('#nombre').prop('disabled', enabled);
    $('#areaGeografica').prop('disabled', enabled);
    $('#sigla').prop('disabled', enabled);
    $('#autorizacionFecha').prop('disabled', enabled);
    $('#autorizacionNumero').prop('disabled', enabled);
    $('#celular').prop('disabled', enabled);
    $('#telefono1').prop('disabled', enabled);
    $('#telefono2').prop('disabled', enabled);
    $('#fax').prop('disabled', enabled);
    $('#email').prop('disabled', enabled);
    $('#direccion').prop('disabled', enabled);
    $('#region').prop('disabled', enabled);
    $('#ciudad').prop('disabled', enabled);
    $('#comuna').prop('disabled', enabled);
}

function loadSelect(control, data, value) {

    control.find("option[value='-1']").remove();

    $(control).empty();

    $.each(data, function (key, value) {

        var option = $(document.createElement('option'));

        option.html(value.Text);

        option.val(value.Value);

        $(control).append(option);

    });

    control.val(value);
}

function gridView() {

    $('#gridView').DataTable({
        "ajax": "/Administracion/Admin/GetCentrosCosto/" + $('#empresaId').val(),
        "columns": [
            { "data": "Nombre" },
            { "data": "AreaGeograficaNombre" },
            { "data": "Accion" }
        ],
        "destroy": true,
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
        ],
        "sDom": '<"dt-panelmenu clearfix"lfr>t<"dt-panelfooter clearfix"ip>'
    });

    $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar centro de costo" typebutton="Add"><i class="fa fa-plus"></i></a>');
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