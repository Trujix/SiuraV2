// ********************************************************
// ARCHIVO JAVASCRIPT EXTENSIONES.JS

// --------------------------------------------------------
// VARIABLES GLOBALES

// --------------------------------------------------------
// FUNCIONES TIPO DOCUMENT (BUTTONS, INPUTS, TEXTAREAS ETC)
// DOCUMENT - CONTROLA LA SELECCION DEL MENU DE OPCIONES

// ::::::::::::::::::::::::::::: [ WIZARD SIURA ] :::::::::::::::::::::::::::::
// DOCUMENT - BOTON QUE EJECUTA EL WIZARD
$(document).on('click', '.ejecutarWizard', function () {
    $('#extensionesModal').modal('hide');
    setTimeout(function () {
        llamarTestWizard();
    }, 2000);
});
// ::::::::::::::::::::::::::::: [ WIZARD SIURA ] :::::::::::::::::::::::::::::

// --------------------------------------------------------
// FUNCIONES GENERALES

// ::::::::::::::::::::::::::::: [ WIZARD SIURA ] :::::::::::::::::::::::::::::
// FUNCION QUE MANDA LLAMAR EL WIZARD PARA CREAR TESTS - SIURA
function llamarTestWizard() {
    var modal = '<div class="modal" id="modalWizardPresentacion" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false"><div class="modal-dialog modal-dialog-centered" role="document"><div class="modal-content"><div class="modal-body"><div class="row"><div class="col-sm-12" align="center"><img id="modalWizardPresentacionLogo" src="../Media/siurawizard.png" style="width: 400px;" /></div></div><div class="row"><div class="col-sm-12" align="center"><span id="modalWizardPresentacionMsg" class="badge badge-pill badge-info">Por Favor espere...</span></div></div></div></div></div></div>';
    $.ajax({
        type: "POST",
        contentType: "application/x-www-form-urlencoded",
        url: "/WSiura/CrearWizardAcceso",
        dataType: "JSON",
        beforeSend: function () {
            $('body').append(modal);
            $('#modalWizardPresentacionLogo').hide();
            $('#modalWizardPresentacionMsg').hide();
            $('#modalWizardPresentacion').modal('show');
            $('.modal-backdrop').removeClass("modal-backdrop");
            $('#modalWizardPresentacionLogo').fadeIn(3500);

            $('#modalWizardPresentacion').on('hidden.bs.modal', function (e) {
                $('#modalWizardPresentacion').remove();
            });
        },
        success: function (data) {
            console.log(data);
            setTimeout(function () {
                $('#modalWizardPresentacionMsg').show();
                setTimeout(function () {
                    $('#modalWizardPresentacion').modal('hide');
                    if (data.Exito) {
                        window.open(data.Url + data.Token, '_blank');
                    } else {
                        ErrorLog(data.Error, "Cargar Wizard SIURA");
                    }
                }, 5000);
            }, 3600);
        },
        error: function (error) {
            ErrorLog(error.responseText, "Cargar Wizard SIURA");
        }
    });
}
// ::::::::::::::::::::::::::::: [ WIZARD SIURA ] :::::::::::::::::::::::::::::