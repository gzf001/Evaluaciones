var rol = [];

jQuery(document).ready(function () {

    $('a[title!=""]').tooltipster();

    $('#search').click(function (e) {

        e.preventDefault();

        loadGridView();
    })

    $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar nuevo usuario" typebutton="Add"><i class="fa fa-plus"></i></a>');

    $('#formModalUser').validate({

        errorClass: 'state-error',
        validClass: 'state-success',
        errorElement: 'em',
        rules: {
            "Persona.Nombres": {
                required: true
            },
            "Persona.Run": {
                required: true,
                maxlength: 14
            },
            "Persona.Email": {
                required: true
            },
            "Persona.ApellidoPaterno": {
                required: true
            },
            "Persona.SexoCodigo": {
                required: true
            }
        },
        messages: {
            "Persona.Nombres": {
                required: 'Los nombres de la persona son requeridos'
            },
            "Persona.Run": {
                required: 'El R.U.N. es requerido',
                maxlength: "La máxima cantidad de caracteres es de 14"
            },
            "Persona.Email": {
                required: 'El correo electrónico es requerido'
            },
            "Persona.ApellidoPaterno": {
                required: 'El apellido paterno es requerido'
            },
            "Persona.SexoCodigo": {
                required: 'Seleccione el sexo de la persona'
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

            var sexo = $("input[name='Persona.SexoCodigo']:checked");

            var obj = {
                id: $('#userId').val(),
                userName: $('#user').val(),
                password: 'string',
                persona: {
                    id: $('#userId').val(),
                    run: $('#run').val(),
                    nombres: $('#nombres').val(),
                    apellidoPaterno: $('#apellidoPaterno').val(),
                    apellidoMaterno: $('#apellidoMaterno').val(),
                    email: $('#email').val(),
                    sexoCodigo: $(sexo).val(),
                    fechaNacimiento: $('#fechaNacimiento').val(),
                    nacionalidadCodigo: $('#nacionalidad').val(),
                    estadoCivilCodigo: $('#estadoCivil').val(),
                    nivelEducacionalCodigo: $('#nivelEducacional').val(),
                    regionCodigo: $('#region').val(),
                    ciudadCodigo: $('#ciudad').val(),
                    comunaCodigo: $('#comuna').val(),
                    villaPoblacion: $('#villaPoblacion').val(),
                    direccion: $('#direccion').val(),
                    telefono: $('#telefono').val(),
                    celular: $('#celular').val()
                }
            };

            $.ajax({
                type: "POST",
                url: "/Administracion/Admin/Usuarios",
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

    $('#formModalUserChangePass').validate({

        errorClass: 'state-error',
        validClass: 'state-success',
        errorElement: 'em',
        rules: {
            "ChangePass.Password1": {
                required: true,
                minlength: 8,
                maxlength: 8
            },
            "ChangePass.Password2": {
                required: true,
                minlength: 8,
                maxlength: 8,
                equalTo: "#password1"
            }
        },
        messages: {
            "ChangePass.Password1": {
                required: 'La contraseña número 1 es requerida',
                minlength: 'La contraseña número 1 debe tener como mínimo 8 caraceteres',
                maxlength: 'La contraseña número 1 debe tener como máximo 8 caraceteres'
            },
            "ChangePass.Password2": {
                required: 'La contraseña número 2 es requerida',
                minlength: 'La contraseña número 2 debe tener como mínimo 8 caraceteres',
                maxlength: 'La contraseña número 2 debe tener como máximo 8 caraceteres',
                equalTo: 'La contraseña número 2 debe ser similar a la contraseña número 1'
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
                id: $('#userId').val(),
                password: 'string',
                changePass: {
                    password1: $('#password1').val(),
                    password2: $('#password2').val()
                }
            };

            $.ajax({
                type: "POST",
                url: "/Administracion/Admin/ChangePass",
                data: obj,
                success: function (data) {

                    if (data == "200") {

                        $.magnificPopup.close();

                        swal("Listo!", "La contraseña fue asignada correctamente", "success");
                    }
                    else {

                        swal("Error!", data, "error");
                    }
                },
                error: function (data) {

                    swal("Error!", "Se ha producido un error al asignar la contraseña", "error");
                }
            })
        }
    })

    $('a[typebutton=Add]').click(function () {

        $('#searchPerson').show();

        $.getJSON('/Administracion/Admin/GetUsuario', function (data) {

            popUp($('#formModalUser'), 'modal-form-principal');

            loadData(data, true);

        });
    })

    $('#searchPerson').click(function () {

        var run = $('#run').val();

        run = run.replace(/\./g, '').replace('-', '');

        $.getJSON('/Administracion/Admin/Usuario/' + run, function (data) {

            if (data == '500') {

                $('#userId').val('');
                $('#nombres').val('');
                $('#apellidoPaterno').val('');
                $('#apellidoMaterno').val('');
                $('#email').val('');

                var masculino = $("input[name='Persona.SexoCodigo'][value=1]");
                var femenino = $("input[name='Persona.SexoCodigo'][value=2]");

                $(masculino).attr('checked', false);
                $(femenino).attr('checked', false);

                $('#fechaNacimiento').val('');
                $('#nacionalidad').val('');
                $('#estadoCivil').val('');
                $('#nivelEducacional').val('');
                $('#telefono').val('');
                $('#celular').val('');

                $('#region').val('');
                $('#ciudad').val('');
                $('#comuna').val('');

                $('#direccion').val('');
                $('#villaPoblacion').val('');

                disabled(true);
            }
            else {

                loadData(data, false);
            }
        })
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $('#searchPerson').hide();

        $.getJSON('/Administracion/Admin/GetUsuario/' + $(this).attr('data-value'), function (data) {

            popUp($('#formModalUser'), 'modal-form-principal');

            $.getJSON('/Evaluaciones/Ciudades?regionCodigo=' + data.Persona.RegionCodigo, function (ciudad) {

                loadSelect($('#ciudad'), ciudad, data.Persona.CiudadCodigo);
            });

            $.getJSON('/Evaluaciones/Comunas/' + data.Persona.RegionCodigo + '/' + data.Persona.CiudadCodigo, function (comuna) {

                loadSelect($('#comuna'), comuna, data.Persona.ComunaCodigo);
            });

            loadData(data, false);
        });
    })

    $(document).on('click', 'a[typebutton=Delete]', function () {

        var usuarioId = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará el usuario",
            type: "warning",
            showCancelButton: true,
            cancelButtonText: "Cancelar",
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Si, eliminalo",
            closeOnConfirm: false
        },
            function () {

                $.getJSON('/Administracion/Admin/DeleteUsuario/' + usuarioId, function (data) {

                    if (data == "200") {

                        loadGridView();

                        swal("Eliminado!", "El usuario fue eliminado de forma correcta", "success");
                    }
                    else {

                        swal("Error!", data, "error");
                    }
                })
            })
    })

    $(document).on('click', 'a[id=changePass]', function () {

        $.getJSON('/Administracion/Admin/GetUsuario/' + $(this).attr('data-value'), function (data) {

            popUp($('#formModalUserChangePass'), 'modal-form-changePass');

            $('#userId').val(data.Id);
            $('#runChangePass').val(data.Persona.Run);
            $('#nombresChangePass').val(data.Persona.Nombre);
            $('#password1').val('');
            $('#password2').val('');
        });
    })

    $(document).on('click', 'a[id=assignRole]', function () {

        $.getJSON('/Administracion/Admin/GetUsuario/' + $(this).attr('data-value'), function (data) {

            popUp($('#formModalRol'), 'modal-form-rol');

            $('#userId').val(data.Id);
            $('#runRol').val(data.Persona.Run);
            $('#nombreRol').val(data.Persona.Nombre);

            $('#ambitoRol').val('-1');

            $('#empresaRol').val('-1');

            $('#centroCostoRol').val('-1');

            $('#empresaRolDiv').hide();

            $('#centroCostoRolDiv').hide();

            rol.length = 0;

            loadGridViewRol();
        });
    })

    $(document).on('click', 'a[id=disableAccount]', function () {

        $.getJSON('/Administracion/Admin/DeshabilitarUsuario/' + $(this).attr('data-value'), function (data) {

            loadGridView();
        });
    })

    $(document).on('click', 'a[id=enableAccount]', function () {

        $.getJSON('/Administracion/Admin/HabilitarUsuario/' + $(this).attr('data-value'), function (data) {

            loadGridView();
        });
    })

    $('#ambitoRol').change(function () {

        var ambitoCodigo = $('#ambitoRol').val();

        switch (ambitoCodigo) {

            case '1': {

                $('#empresaRolDiv').hide();
                $('#centroCostoRolDiv').hide();

                break;
            }
            case '2': {

                $('#empresaRolDiv').show();
                $('#centroCostoRolDiv').hide();

                break;
            }
            case '3': {

                $('#empresaRolDiv').show();
                $('#centroCostoRolDiv').show();

                break;
            }
            default: {

                $('#empresaRolDiv').hide();
                $('#centroCostoRolDiv').hide();

                break;
            }
        }

        loadGridViewRol();
    })

    $('#empresaRol').change(function () {

        var ambitoCodigo = $('#ambitoRol').val();
        var empresaId = $('#empresaRol').val();

        $.getJSON('/Evaluaciones/CentrosCosto?empresaId=' + $('#empresaRol').val(), function (data) {

            var items = "";

            $.each(data, function (i, centroCosto) {
                items += "<option value='" + centroCosto.Value + "'>" + centroCosto.Text + "</option>";
            });

            $("#centroCostoRol").html(items);

            if (ambitoCodigo == 2 && empresaId != '-1') {

                loadGridViewRol();
            }
        });
    })

    $('#centroCostoRol').change(function () {

        var ambitoCodigo = $('#ambitoRol').val();
        var centroCostoId = $('#centroCostoRol').val();

        if (ambitoCodigo == 3 && centroCostoId != '-1') {

            loadGridViewRol();
        }
    })

    jQuery.validator.addMethod("notEqual", function (value, element, param) {

        var notEqual = true;

        if (value == '-1') {

            notEqual = false;
        }

        return notEqual;
    })

    $('#formModalRol').validate({

        errorClass: 'state-error',
        validClass: 'state-success',
        errorElement: 'em',
        rules: {
            'Role.AmbitoCodigo': {
                notEqual: '-1'
            },
            'Business.RazonSocial': {
                notEqual: '-1'
            },
            'CostCenter.Nombre': {
                notEqual: '-1'
            }
        },
        messages: {
            'Role.AmbitoCodigo': {
                notEqual: 'Debe seleccionar el ámbito'
            },
            'Business.RazonSocial': {
                notEqual: 'Debe seleccionar la empresa'
            },
            'CostCenter.Nombre': {
                notEqual: 'Debe seleccionar el centro de costo'
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

            var objs = [];

            if (rol.length > 0) {

                var userId = $('#userId').val();
                var ambitoCodigo = $('#ambitoRol').val();
                var empresaId = $("#empresaRol").is(":visible") ? $('#empresaRol').val() == '-1' ? null : $('#empresaRol').val() : null;
                var centroCostoId = $("#centroCostoRol").is(":visible") ? $('#centroCostoRol').val() == '-1' ? null : $('#centroCostoRol').val() : null;

                $(rol).each(function (i) {

                    objs.push({
                        ambitoCodigo: ambitoCodigo,
                        rolId: rol[i],
                        personaId: userId,
                        empresaId: empresaId == '-1' ? null : empresaId,
                        centroCostoId: centroCostoId == '-1' ? null : centroCostoId
                    })
                });

                $.ajax({
                    type: "POST",
                    url: "/Administracion/Admin/UsuarioRol/",
                    data: { 'model': objs },
                    success: function (data) {

                        switch (data) {

                            case "501": {

                                swal("Error!", "Los roles no pudieron ser asignados", "error");

                                break;
                            }
                            case "500": {

                                swal("Error!", "Error interno", "error");

                                break;
                            }
                            default: {

                                $.magnificPopup.close();

                                swal("Listo!", "Los roles fueron asignados correctamente", "success");

                                break;
                            }
                        }
                    },
                    error: function () {

                        swal("Error!", "Se ha producido un error al asignar los roles", "error");
                    }
                })
            }
            else {

                swal('Error!', 'Debe seleccionar al menos un rol para asignar', 'warning');
            }
        }
    })

    $(document).on('change', '#gridViewRol tbody tr td input[type=checkbox]', function (e) {

        e.preventDefault();

        var rolId = $(this).attr('data-value');

        var index = jQuery.inArray(rolId, rol);

        if ($(this).is(':checked')) {

            if (index == -1) {

                rol.push(rolId);
            }
        } else {

            rol.splice(index, 1);
        }
    })

    $('button[typebutton=Cancel]').click(function (e) {

        e.preventDefault();

        $.magnificPopup.close();
    })
})

function loadData(data, enabled) {

    $('#userId').val(data.Id);
    $('#nombres').val(data.Persona.Nombres);
    $('#apellidoPaterno').val(data.Persona.ApellidoPaterno);
    $('#apellidoMaterno').val(data.Persona.ApellidoMaterno);
    $('#run').val(data.Persona.Run);
    $('#email').val(data.Persona.Email);

    var sexo = $("input[name='Persona.SexoCodigo'][value=" + data.Persona.SexoCodigo + "]");

    $(sexo).attr('checked', true);

    $('#fechaNacimiento').val(data.FechaNacimientoString);
    $('#nacionalidad').val(data.Persona.NacionalidadCodigo);
    $('#estadoCivil').val(data.Persona.EstadoCivilCodigo);
    $('#nivelEducacional').val(data.Persona.NivelEducacionalCodigo);
    $('#telefono').val(data.Persona.Telefono);
    $('#celular').val(data.Persona.Celular);

    $('#region').val(data.Persona.RegionCodigo);
    $('#ciudad').val(data.Persona.CiudadCodigo);
    $('#comuna').val(data.Persona.ComunaCodigo);

    $('#direccion').val(data.Persona.Direccion);
    $('#villaPoblacion').val(data.Persona.VillaPoblacion);

    disabled(enabled);
}

function disabled(enabled) {

    $('#nombres').prop('disabled', enabled);
    $('#apellidoPaterno').prop('disabled', enabled);
    $('#apellidoMaterno').prop('disabled', enabled);
    $('#email').prop('disabled', enabled);

    $(sexo).prop('disabled', enabled);

    $('#fechaNacimiento').prop('disabled', enabled);
    $('#nacionalidad').prop('disabled', enabled);
    $('#estadoCivil').prop('disabled', enabled);
    $('#nivelEducacional').prop('disabled', enabled);
    $('#telefono').prop('disabled', enabled);
    $('#celular').prop('disabled', enabled);

    $('#region').prop('disabled', enabled);
    $('#ciudad').prop('disabled', enabled);
    $('#comuna').prop('disabled', enabled);

    $('#direccion').prop('disabled', enabled);
    $('#villaPoblacion').prop('disabled', enabled);
}

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

function loadGridView() {

    var nombre = $('#searchName').val();
    var findType = $("input[name=FindType]:checked");
    var run = $('#searchRun').val();

    var url;

    if (nombre != '') {

        url = "/Administracion/Admin/GetAllUsuarios/" + findType.val() + "/" + nombre;

    }
    else if (run != '') {

        run = run.replace(/\./g, '').replace('-', '');

        url = "/Administracion/Admin/GetUsuarios/" + run;
    }
    else {

        swal("Listo!", "Debe seleccionar un filtro de busqueda", "error");

        return;
    }

    $('#gridView').DataTable({
        "ajax": url,
        "destroy": true,
        "columns": [
            { "data": "Nombre" },
            { "data": "Run" },
            { "data": "Estado" },
            { "data": "UltimoLogin" },
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

function loadGridViewRol() {

    var ambitoCodigo = $('#ambitoRol').val();
    var empresaId = $("#empresaRol").is(":visible") ? $('#empresaRol').val() == '-1' ? null : $('#empresaRol').val() : null;
    var centroCostoId = $("#centroCostoRol").is(":visible") ? $('#centroCostoRol').val() == '-1' ? null : $('#centroCostoRol').val() : null;

    if (ambitoCodigo == '-1') {

        $('#gridViewRol').empty();
    }
    else if (ambitoCodigo == 2 && empresaId == '-1') {

        $('#gridViewRol').empty();
    }
    else if (ambitoCodigo == 3 && centroCostoId == '-1') {

        $('#gridViewRol').empty();
    }

    $('#gridViewRol').DataTable({
        "ajax": "/Administracion/Admin/UsuarioRol/" + $("#userId").val() + "/" + $("#ambitoRol").val() + "/" + empresaId + "/" + centroCostoId,
        "columns": [
            { "data": "Accion" },
            { "data": "Nombre" }
        ],
        "destroy": true,
        "order": [[1, "asc"]],
        "columnDefs": [
            {
                "targets": [0],
                "searchable": false,
                "sortable": false
            }
        ],
        "iDisplayLength": 4,
        "aLengthMenu": [
            [4, 8, 12, 16, -1],
            [4, 8, 12, 16, "All"]
        ],
        "fnCreatedRow": function (nRow, aData, iDataIndex) {

            if (rol != null) {
                var chekBox = nRow.childNodes[0].childNodes[0].childNodes[0];

                if ($(chekBox).is(':checked')) {

                    var rolId = $(chekBox).attr('data-value');

                    rol.push(rolId);
                }
            }
        },
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