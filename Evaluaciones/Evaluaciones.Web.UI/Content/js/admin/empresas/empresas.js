var rol = [];

jQuery(document).ready(function () {

    $('a[title!=""]').tooltipster();

    $('#search').click(function (e) {

        e.preventDefault();

        loadGridView();
    })

    $('#formModalEmpresa').validate({

        errorClass: 'state-error',
        validClass: 'state-success',
        errorElement: 'em',
        rules: {
            "Rut": {
                required: true
            },
            "RazonSocial": {
                required: true
            }
        },
        messages: {
            "Rut": {
                required: 'El R.U.T. es requerido'
            },
            "RazonSocial": {
                required: 'La razón social es requerida'
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
                id: $('#empresaId').val(),
                rut: $('#rut').val(),
                razonSocial: $('#razonSocial').val(),
                regionCodigo: $('#region').val(),
                ciudadCodigo: $('#ciudad').val(),
                comunaCodigo: $('#comuna').val(),
                direccion: $('#direccion').val(),
                email: $('#email').val(),
                paginaWeb: $('#paginaWeb').val(),
                telefono1: $('#telefono1').val(),
                telefono2: $('#telefono2').val(),
                fax: $('#fax').val(),
                celular: $('#celular').val()
            };

            $.ajax({
                type: "POST",
                url: "/Administracion/Admin/Empresas",
                data: obj,
                success: function (data) {

                    if (data == "200") {

                        $.magnificPopup.close();

                        loadGridView();

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

    $('a[typebutton=Add]').click(function () {

        $('#searchEnterprise').show();

        $('#rut').val('');

        $.getJSON('/Administracion/Admin/GetAddEmpresa', function (data) {

            popUp($('#formModalUser'), 'modal-form-principal');

            loadData(data, true);

        });
    })

    $('#searchEnterprise').click(function () {

        var rut = $('#rut').val();

        rut = rut.replace(/\./g, '').replace('-', '');

        $.getJSON('/Administracion/Admin/Empresa/' + rut, function (data) {

            if (data == '500') {

                $('#empresaId').val('');
                $('#razonSocial').val('');
                $('#region').val('');
                $('#ciudad').val('');
                $('#comuna').val('');
                $('#direccion').val('');
                $('#email').val('');
                $('#paginaWeb').val('');
                $('#telefono1').val('');
                $('#telefono2').val('');
                $('#fax').val('');
                $('#celular').val('');

                disabled(true);
            }
            else {

                loadData(data, false);
            }
        })
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $('#searchEnterprise').hide();

        $.getJSON('/Administracion/Admin/GetEmpresa/' + $(this).attr('data-value'), function (data) {

            popUp($('#formModalEmpresa'), 'modal-form-principal');

            $.getJSON('/Evaluaciones/Ciudades?regionCodigo=' + data.RegionCodigo, function (ciudad) {

                loadSelect($('#ciudad'), ciudad, data.CiudadCodigo);
            });

            $.getJSON('/Evaluaciones/Comunas/' + data.RegionCodigo + '/' + data.CiudadCodigo, function (comuna) {

                loadSelect($('#comuna'), comuna, data.ComunaCodigo);
            });

            $('#rut').val(data.Rut);

            loadData(data, false);
        });
    })

    $(document).on('click', 'a[id=disableEnterprise]', function () {

        $.getJSON('/Administracion/Admin/DeshabilitarEmpresa/' + $(this).attr('data-value'), function (data) {

            loadGridView();
        });
    })

    $(document).on('click', 'a[id=enableEnterprise]', function () {

        $.getJSON('/Administracion/Admin/HabilitarEmpresa/' + $(this).attr('data-value'), function (data) {

            loadGridView();
        });
    })

    $('button[typebutton=Cancel]').click(function (e) {

        e.preventDefault();

        $.magnificPopup.close();
    })
})

function loadSelect(control, data, value) {

    control.find("option[value='-1']").remove();

    $.each(data, function (key, value) {

        var option = $(document.createElement('option'));

        option.html(value.Text);

        option.val(value.Value);

        $(control).append(option);

    });

    control.val(value);
}

function loadData(data, enabled) {

    $('#empresaId').val(data.Id);
    $('#razonSocial').val(data.RazonSocial);
    $('#region').val(data.RegionCodigo);
    $('#ciudad').val(data.CiudadCodigo);
    $('#comuna').val(data.ComunaCodigo);
    $('#direccion').val(data.Direccion);
    $('#email').val(data.Email);
    $('#paginaWeb').val(data.PaginaWeb);
    $('#telefono1').val(data.Telefono1);
    $('#telefono2').val(data.Telefono2);
    $('#fax').val(data.Fax);
    $('#celular').val(data.Celular);

    disabled(enabled);
}

function disabled(enabled) {

    $('#razonSocial').prop('disabled', enabled);
    $('#region').prop('disabled', enabled);
    $('#ciudad').prop('disabled', enabled);
    $('#comuna').prop('disabled', enabled);
    $('#direccion').prop('disabled', enabled);
    $('#email').prop('disabled', enabled);
    $('#paginaWeb').prop('disabled', enabled);
    $('#telefono1').prop('disabled', enabled);
    $('#telefono2').prop('disabled', enabled);
    $('#fax').prop('disabled', enabled);
    $('#celular').prop('disabled', enabled);
}

function loadGridView() {

    var nombre = $('#searchName').val();
    var findType = $("input[name=FindType]:checked");
    var rut = $('#searchRut').val();

    var url;

    if (nombre != '') {

        url = "/Administracion/Admin/GetAllEmpresas/" + findType.val() + "/" + nombre;

    }
    else if (run != '') {

        rut = run.replace(/\./g, '').replace('-', '');

        url = "/Administracion/Admin/GetEmpresas/" + rut;
    }
    else {

        swal("Listo!", "Debe seleccionar un filtro de busqueda", "error");

        return;
    }

    $('#gridView').DataTable({
        "ajax": url,
        "destroy": true,
        "columns": [
            { "data": "RazonSocial" },
            { "data": "Rut" },
            { "data": "Estado" },
            { "data": "Accion" }
        ],
        "order": [[0, "asc"]],
        "columnDefs": [
            {
                "targets": [0],
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
}

function popUp(form, modalForm) {

    form.find('.state-error').removeClass('state-error');
    form.find('.state-success').removeClass('state-success');
    form.find("em").remove();

    $.magnificPopup.open({
        removalDelay: 500,
        items: {
            src: '#' + modalForm
        },
        callbacks: {
            beforeOpen: function (e) {

                this.st.mainClass = 'mfp-flipInX';
            }
        },
        midClick: true
    });
}