'use strict';
//  Author: AdminDesigns.com
// 
//  This file is reserved for changes made by the use. 
//  Always seperate your work from the theme. It makes
//  modifications, and future theme updates much easier 
// 

(function($) {

    //DATE (dd/mm/yy)
    $(".hasDatePicker").datepicker({
        prevText: '<i class="fa fa-chevron-left"></i>',
        nextText: '<i class="fa fa-chevron-right"></i>',
        dateFormat: 'dd/mm/yy',
        showButtonPanel: false,
        beforeShow: function (input, inst) {
            var newclass = 'admin-form';
            var themeClass = $(this).parents('.admin-form').attr('class');
            var smartpikr = inst.dpDiv.parent();
            if (!smartpikr.hasClass(themeClass)) {
                inst.dpDiv.wrap('<div class="' + themeClass + '"></div>');
            }
        }
    });

    $('.hasDatePicker').mask('99/99/9999');

    enableValidationRut();

    $("#region").change(function () {

        $("#ciudad").html("<option value='-1'>[Seleccione]</option>");
        $("#comuna").html("<option value='-1'>[Seleccione]</option>");

        $.getJSON('/Evaluaciones/Ciudades?regionCodigo=' + $('#region').val(), function (data) {
            var items = "";

            $.each(data, function (i, ciudad) {
                items += "<option value='" + ciudad.Value + "'>" + ciudad.Text + "</option>";
            });

            $("#ciudad").html(items);
        });
    });

    $("#ciudad").change(function () {

        $("#comuna").html("<option value='-1'>[Seleccione]</option>");

        $.getJSON('/Evaluaciones/Comunas/' + $('#region').val() + '/' + $('#ciudad').val(), function (data) {

            var items = "";
            $.each(data, function (i, comuna) {
                items += "<option value='" + comuna.Value + "'>" + comuna.Text + "</option>";
            });

            $("#comuna").html(items);
        });
    });

})(jQuery);