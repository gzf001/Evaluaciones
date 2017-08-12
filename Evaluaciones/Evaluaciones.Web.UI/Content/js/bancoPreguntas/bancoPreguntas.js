jQuery(document).ready(function () {

    var objetivoAprendizajeId;

    var aprendizajeId;

    var contenidoId;

    var referenciaId;

    $('#preguntaAlternativa').hide();
    $('#preguntaSeleccionMultiple').hide();
    $('#preguntaVerdaderoFalso').hide();

    $('.summernote').summernote({
        height: 255,
        focus: false,
        oninit: function () { },
        onChange: function (contents, $editable) { },
    });

    $('.summernote-edit').summernote({
        airMode: true,
        focus: false
    });

    $("#accordion").accordion({
        collapsible: true,
    });

    $("#tipoEducacion").change(function (e) {

        e.preventDefault();

        $("#grado").html("<option value='-1'>[Seleccione]</option>");
        $("#sector").html("<option value='-1'>[Seleccione]</option>");

        var tipoEducacion = $('#tipoEducacion').val();

        $.getJSON('/Educacion/Grados?tipoEducacion=' + tipoEducacion, function (data) {

            var items = "";

            $.each(data, function (i, grado) {
                items += "<option value='" + grado.Value + "'>" + grado.Text + "</option>";
            });

            $("#grado").html(items);
        });
    });

    $("#grado").change(function (e) {

        e.preventDefault();

        var tipoEducacion = $('#tipoEducacion').val();
        var grado = $('#grado').val();

        $.getJSON('/Educacion/BaseCurricular', function (data) {

            if ((parseInt(tipoEducacion) + parseInt(grado)) <= (data.TipoEducacionCodigo + data.Codigo)) {

                $('#baseCurricular').removeClass("ui-state-disabled"); //Bases curriculares
                $('#recursoCurricular').addClass("ui-state-disabled"); //Recursos curriculares
            }
            else {

                $('#baseCurricular').addClass("ui-state-disabled"); //Bases curriculares
                $('#recursoCurricular').removeClass("ui-state-disabled"); //Recursos curriculares
            }

            $("#sector").html("<option value='-1'>[Seleccione]</option>");

            $.getJSON('/Educacion/Sectores/' + tipoEducacion + '/' + grado, function (data) {

                var items = "";

                $.each(data, function (i, sector) {
                    items += "<option value='" + sector.Value + "'>" + sector.Text + "</option>";
                });

                $("#sector").html(items);
            });
        });
    });

    $("#sector").change(function (e) {

        e.preventDefault();

        var tipoEducacion = $('#tipoEducacion').val();
        var grado = $('#grado').val();
        var sector = $('#sector').val();

        loadReferencia();

        if ($('#baseCurricular').hasClass('ui-state-disabled')) {

           //Recursos curriculares

            $("#unidadRecursoCurricular").html("<option value='-1'>[Seleccione]</option>");
            $("#ejeRecursoCurricular").html("<option value='-1'>[Seleccione]</option>");

            loadAprendizaje();
            loadContenido();

            $.getJSON('/RecursoCurricular/Unidades/' + tipoEducacion + '/' + grado + '/' + sector, function (data) {

                var items = "";

                $.each(data, function (i, unidad) {
                    items += "<option value='" + unidad.Value + "'>" + unidad.Text + "</option>";
                });

                $("#unidadRecursoCurricular").html(items);
            });
        }
        else {

            //Bases curriculares

            $("#unidadBaseCurrilar").html("<option value='-1'>[Seleccione]</option>");
            $("#ejeBaseCurrilar").html("<option value='-1'>[Seleccione]</option>");

            $.getJSON('/BaseCurricular/Unidades/' + tipoEducacion + '/' + grado + '/' + sector, function (data) {

                var items = "";

                $.each(data, function (i, unidad) {
                    items += "<option value='" + unidad.Value + "'>" + unidad.Text + "</option>";
                });

                $("#unidadBaseCurrilar").html(items);
            });

            loadObjetivosAprendizaje();
        }
    });

    $("#tipoReactivo").change(function (e) {

        e.preventDefault();

        var tipoReactivo = parseInt($('#tipoReactivo').val());

        switch (tipoReactivo) {

            case 1:
                {
                    $('#preguntaAlternativa').show();
                    $('#preguntaSeleccionMultiple').hide();
                    $('#preguntaVerdaderoFalso').hide();

                    break;
                }
            case 2:
                { 
                    $('#preguntaAlternativa').hide();
                    $('#preguntaSeleccionMultiple').show();
                    $('#preguntaVerdaderoFalso').hide();

                    break;
                }
            case 3:
                {
                    $('#preguntaAlternativa').hide();
                    $('#preguntaSeleccionMultiple').hide();
                    $('#preguntaVerdaderoFalso').show();

                    break;
                }
            default:
                {
                    $('#preguntaAlternativa').hide();
                    $('#preguntaSeleccionMultiple').hide();
                    $('#preguntaVerdaderoFalso').hide();

                    break;
                }
        }
    });

    /************************************************************************************/
    $("#unidadBaseCurrilar").change(function (e) {

        e.preventDefault();

        $("#ejeBaseCurrilar").html("<option value='-1'>[Seleccione]</option>");

        var tipoEducacion = $('#tipoEducacion').val();
        var grado = $('#grado').val();
        var sector = $('#sector').val();
        var unidad = $("#unidadBaseCurrilar").val();

        $("#ejeBaseCurrilar").html("<option value='-1'>[Seleccione]</option>");

        $.getJSON('/BaseCurricular/Ejes/' + tipoEducacion + '/' + grado + '/' + sector + '/' + unidad, function (data) {

            var items = "";

            $.each(data, function (i, eje) {
                items += "<option value='" + eje.Value + "'>" + eje.Text + "</option>";
            });

            $("#ejeBaseCurrilar").html(items);
        });

        loadObjetivosAprendizaje();
    });

    $("#ejeBaseCurrilar").change(function (e) {

        e.preventDefault();

        loadObjetivosAprendizaje();
    });

    function loadObjetivosAprendizaje() {

        var tipoEducacion = $('#tipoEducacion').val();
        var grado = $('#grado').val();
        var sector = $('#sector').val();
        var unidad = $("#unidadBaseCurrilar").val();
        var eje = $("#ejeBaseCurrilar").val();

        var table = $('#objetivoGridView').DataTable({
            'ajax': '/BaseCurricular/GetObjetivosAprendizaje/' + tipoEducacion + '/' + grado + '/' + sector + '/' + unidad + '/' + eje,
            'columns': [
                { 'data': 'Descripcion' }
            ],
            'destroy': true,
            'bPaginate': false,
            'fnCreatedRow': function (nRow, aData, iDataIndex) {

                $(nRow).on('click', function () {

                    if ($(nRow).hasClass('selected')) {

                        $(nRow).removeClass('selected');

                        $(nRow).addClass('odd');

                        objetivoAprendizajeId = '';
                    }
                    else {
                        table.$('tr.selected').removeClass('selected');

                        $(nRow).addClass('selected');

                        $(nRow).removeClass('odd');

                        objetivoAprendizajeId = aData.Id;
                    }
                });
            },
            'sDom': '<"dt-panelfooter clearfix"ip>',
        });
    }
    /************************************************************************************/


    /************************************************************************************/
    $("#unidadRecursoCurricular").change(function (e) {

        e.preventDefault();

        loadContenido();

        loadAprendizaje();
    });

    $("#ejeRecursoCurricular").change(function (e) {

        loadContenido();
    });
    /************************************************************************************/

    $(document).on('click', 'a[typebutton=OtherAction]', function () {

        $.getJSON('/BancoPregunta/Pregunta/GetReferencia/' + $(this).attr('data-value'), function (data) {

            $('#modalTipoEducacion').val(data.TipoEducacion.Nombre);
            $('#modalGradoCodigo').val(data.GradoCodigo);
            $('#modalSector').val($("#sectorReferencia option:selected").text());
            $('#modalCodigo').val(data.Codigo);
            $('#referenciaTexto').val(data.Texto);

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
        });
    })

    function loadAprendizaje() {

        var tipoEducacion = $('#tipoEducacion').val();
        var grado = $('#grado').val();
        var sector = $('#sector').val();
        var unidad = $("#unidadRecursoCurricular").val();

        var table = $('#aprendizajeGridView').DataTable({
            'ajax': '/RecursoCurricular/GetAprendizajes/' + tipoEducacion + '/' + grado + '/' + sector + '/' + unidad,
            'columns': [
                { 'data': 'Descripcion' }
            ],
            'destroy': true,
            'bPaginate': false,
            'fnCreatedRow': function (nRow, aData, iDataIndex) {

                $(nRow).on('click', function () {

                    if ($(nRow).hasClass('selected')) {

                        $(nRow).removeClass('selected');

                        $(nRow).addClass('odd');

                        $("#ejeRecursoCurricular").html("<option value='-1'>[Seleccione]</option>");

                        aprendizajeId = '';
                    }
                    else {
                        table.$('tr.selected').removeClass('selected');

                        $(nRow).addClass('selected');

                        $(nRow).removeClass('odd');

                        aprendizajeId = aData.Id;

                        $.getJSON('/RecursoCurricular/Ejes/' + tipoEducacion + '/' + grado + '/' + sector + '/' + aprendizajeId, function (data) {

                            var items = "";

                            $.each(data, function (i, unidad) {
                                items += "<option value='" + unidad.Value + "'>" + unidad.Text + "</option>";
                            });

                            $("#ejeRecursoCurricular").html(items);
                        });
                    }
                });
            },
            'sDom': '<"dt-panelfooter clearfix"ip>',
        });
    }

    function loadContenido() {

        var tipoEducacion = $('#tipoEducacion').val();
        var grado = $('#grado').val();
        var sector = $('#sector').val();
        var eje = $("#ejeRecursoCurricular").val();

        var table = $('#contenidoGridView').DataTable({
            'ajax': '/RecursoCurricular/GetContenidos/' + tipoEducacion + '/' + grado + '/' + sector + '/' + aprendizajeId + '/' + eje,
            'columns': [
                { 'data': 'Descripcion' }
            ],
            'destroy': true,
            'bPaginate': false,
            'fnCreatedRow': function (nRow, aData, iDataIndex) {

                $(nRow).on('click', function () {

                    if ($(nRow).hasClass('selected')) {

                        $(nRow).removeClass('selected');

                        $(nRow).addClass('odd');

                        contenidoId = '';
                    }
                    else {
                        table.$('tr.selected').removeClass('selected');

                        $(nRow).addClass('selected');

                        $(nRow).removeClass('odd');

                        contenidoId = aData.Id;
                    }
                });
            },
            'sDom': '<"dt-panelfooter clearfix"ip>',
        });
    }

    function loadReferencia() {

        var tipoEducacion = $('#tipoEducacion').val();
        var grado = $('#grado').val();
        var sector = $('#sector').val();

        var table = $('#referenciaGridView').DataTable({
            "ajax": "/BancoPregunta/Pregunta/Referencias/" + tipoEducacion + "/" + grado + "/" + sector,
            "columns": [
                { "data": "Codigo" },
                { "data": "Texto" },
                { "data": "Accion" }
            ],
            'destroy': true,
            "order": [[0, "asc"]],
            "columnDefs": [
                {
                    "targets": [1],
                    "searchable": false,
                    "sortable": false
                }
            ],
            "iDisplayLength": 10,
            "aLengthMenu": [
                [10, 20, 30, 40, 50, -1],
                [10, 20, 30, 40, 50, "All"]
            ],
            'fnCreatedRow': function (nRow, aData, iDataIndex) {

                $(nRow).on('click', function () {

                    if ($(nRow).hasClass('selected')) {

                        $(nRow).removeClass('selected');

                        $(nRow).addClass('odd');

                        referenciaId = '';
                    }
                    else {
                        table.$('tr.selected').removeClass('selected');

                        $(nRow).addClass('selected');

                        $(nRow).removeClass('odd');

                        referenciaId = aData.Id;
                    }
                });
            },
            "sDom": '<"dt-panelmenu clearfix"lfr>t<"dt-panelfooter clearfix"ip>'
        });
    }

    /************************************************************************************/

    $('#formPregunta').validate({

        errorClass: 'state-error',
        validClass: 'state-success',
        errorElement: 'em',
        rules: {
            "TipoEducacionCodigo": {
                notEqual: '-1'
            },
            "GradoCodigo": {
                notEqual: '-1',
            },
            "SectorId": {
                notEqual: '-1'
            },
            "DificultadCodigo": {
                notEqual: '-1'
            },
            "TipoReactivoCodigo": {
                notEqual: '-1'
            },
            "TipoEvaluacionCodigo": {
                notEqual: '-1'
            },
            "HabilidadCodigo": {
                notEqual: '-1'
            },
            "BaseCurricular.TipoEducacionCodigo": {
                notEqual: '-1'
            },
            "BaseCurricular.GradoCodigo": {
                notEqual: '-1'
            },
            "BaseCurricular.UnidadId": {
                notEqual: '-1'
            },
            "BaseCurricular.EjeId": {
                notEqual: '-1'
            },
            "RecursoCurricular.TipoEducacionCodigo": {
                notEqual: '-1'
            },
            "RecursoCurricular.GradoCodigo": {
                notEqual: '-1'
            },
            "RecursoCurricular.UnidadId": {
                notEqual: '-1'
            },
            "RecursoCurricular.EjeId": {
                notEqual: '-1'
            },
            "AlternativaA": {
                required: true
            },
            "AlternativaB": {
                required: true
            },
            "AlternativaC": {
                required: true
            },
            "AlternativaCorrecta": {
                notEqual: '-1'
            }
        },
        messages: {
            "TipoEducacionCodigo": {
                notEqual: 'Debe seleccionar el tipo de educación'
            },
            "GradoCodigo": {
                notEqual: 'Debe seleccionar un grado'
            },
            "SectorId": {
                notEqual: 'Debe seleccionar un sector'
            },
            "DificultadCodigo": {
                notEqual: 'Debe seleccionar una dificultad'
            },
            "TipoReactivoCodigo": {
                notEqual: 'Debe seleccionar el tipo de reactivo'
            },
            "TipoEvaluacionCodigo": {
                notEqual: 'Debe seleccionar el tipo de evaluación'
            },
            "HabilidadCodigo": {
                notEqual: 'Debe seleccionar una habilidad'
            },
            "BaseCurricular.TipoEducacionCodigo": {
                notEqual: 'Debe seleccionar el tipo de educación de las bases curriculares'
            },
            "BaseCurricular.GradoCodigo": {
                notEqual: 'Debe seleccionar el grado de las bases curriculares'
            },
            "BaseCurricular.UnidadId": {
                notEqual: 'Debe seleccionar la unidad de las bases curriculares'
            },
            "BaseCurricular.EjeId": {
                notEqual: 'Debe seleccionar el eje de las bases curriculares'
            },
            "RecursoCurricular.TipoEducacionCodigo": {
                notEqual: 'Debe seleccionar el tipo de educación de los recursos curriculares'
            },
            "RecursoCurricular.GradoCodigo": {
                notEqual: 'Debe seleccionar el grado de los recursos curriculares'
            },
            "RecursoCurricular.UnidadId": {
                notEqual: 'Debe seleccionar la unidad de los recursos curriculares'
            },
            "RecursoCurricular.EjeId": {
                notEqual: 'Debe seleccionar el eje de los recursos curriculares'
            },
            "AlternativaA": {
                required: 'Debe texto para la alternativa A'
            },
            "AlternativaB": {
                required: 'Debe texto para la alternativa B'
            },
            "AlternativaC": {
                required: 'Debe texto para la alternativa C'
            },
            "AlternativaCorrecta": {
                notEqual: 'Debe seleccionar la alternativa correcta'
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

            $.getJSON('/Utils/GenerateId', function (data) {

                var fecha = new Date();

                var obj = {
                    tipoEducacionCodigo: $('#tipoEducacion').val(),
                    gradoCodigo: $('#grado').val(),
                    sectorId: $('#sector').val(),
                    id: data,
                    referenciaId: referenciaId,
                    codigo: 0,
                    dificultadCodigo: $('#dificultad').val(),
                    tipoReactivoCodigo: $('#tipoReactivo').val(),
                    tipoEvaluacionCodigo: $('#tipoEvaluacion').val(),
                    habilidadCodigo: $('#habilidad').val(),
                    texto: $('#editor').val(),
                    privado: $("#privada").prop("checked"),
                    baseCurricular: {
                        tipoEducacionCodigo: $('#tipoEducacion').val(),
                        gradoCodigo: $('#grado').val(),
                        sectorId: $('#sector').val(),
                        preguntaId: data,
                        anioNumero: fecha.getFullYear(),
                        unidadId: $('#unidadBaseCurrilar').val(),
                        ejeId: $('#ejeBaseCurrilar').val(),
                        objetivoAprendizajeId: objetivoAprendizajeId
                    },
                    preguntaAlternativa: {
                        tipoEducacionCodigo: $('#tipoEducacion').val(),
                        gradoCodigo: $('#grado').val(),
                        sectorId: $('#sector').val(),
                        preguntaId: data,
                        alternativaCorrecta: $('#alternativaCorrecta').val(),
                        alternativaA: $('#alternativaA').val(),
                        alternativaB: $('#alternativaB').val(),
                        alternativaC: $('#alternativaC').val(),
                        alternativaD: $('#alternativaD').val(),
                        alternativaE: $('#alternativaE').val()
                    }
                };

                $.ajax({
                    type: "POST",
                    url: "/BancoPregunta/Pregunta/CrearPregunta",
                    data: obj,
                    success: function (data) {

                        if (data == "200") {

                            $.magnificPopup.close();

                            swal("Listo!", "Su información fue guardada correctamente", "success");
                        }
                        else {

                            swal("Error!", data, "error");
                        }
                    },
                    error: function (data) {

                        swal("Error!", "Se ha producido un error al registrar la información", "error");
                    }
                });
            });
        }
    })

    jQuery.validator.addMethod("notEqual", function (value, element, param) {

        var notEqual = true;

        if (value == '-1') {

            notEqual = false;
        }

        return notEqual;
    })
})