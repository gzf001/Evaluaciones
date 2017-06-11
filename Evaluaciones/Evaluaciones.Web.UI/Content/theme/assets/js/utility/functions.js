function enableValidationRut() {
    if ($("input").hasClass("rut")) {
        $(".rut").Rut({
                on_error: function () {
                    $(this).focus();
                    swal("", "Rut incorrecto!", "error");
                },
                on_success: function () {
                    console.log('rut ok!');
                }
            });
    }
}