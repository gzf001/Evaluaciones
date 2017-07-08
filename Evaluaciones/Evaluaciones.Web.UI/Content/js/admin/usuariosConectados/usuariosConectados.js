jQuery(document).ready(function () {
    countUser();
})

function countUser() {

    $.ajax({
        type: "Get",
        url: "/Administracion/Admin/CountUsuarioConectados",
        success: function (data) {

            $('#numeroUsuarios').val(data);
        },
        error: function (data) {

            swal("Error!", "Se ha producido un error al realizar el conteo", "error");
        }
    })

    setTimeout("countUser()", 1000);
}