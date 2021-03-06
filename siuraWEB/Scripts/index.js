// ********************************************************
// ARCHIVO JAVASCRIPT INDEX.JS

// --------------------------------------------------------
// VARIABLES GLOBALES
var calendarioMaestroGLOBAL;
var calendarioActividades;
var vistasControllers = [
    { Vista: "", Pagina: "" },
    { Vista: "CDeportiva", Pagina: "MenuDeportivo" },
    { Vista: "CMedica", Pagina: "MenuMedico" },
    { Vista: "CPsicologica", Pagina: "MenuPsicologo" },
    { Vista: "", Pagina: "" },
    { Vista: "CConsejeria", Pagina: "MenuConsejeria" },
    { Vista: "Documentacion", Pagina: "Ingreso" },
    { Vista: "Documentacion", Pagina: "" },
    { Vista: "Documentacion", Pagina: "Administracion" },
];

// --------------------------------------------------------
// FUNCIONES TIPO DOCUMENT (BUTTONS, INPUTS, TEXTAREAS ETC)
// DOCUMENT - CONTROLA LA SELECCION DEL MENU DE OPCIONES
$(document).on('click', 'a[name="menuopc"]', function () {
    var indx = parseInt($(this).attr("indx"));
    if (indx > 5 && indx < 9) {
        $('.menuopcion').removeClass("active");
        $('#documentacionOpc').parent().addClass("active");
    }
    $.ajax({
        type: "POST",
        contentType: "application/x-www-form-urlencoded",
        url: "/" + vistasControllers[indx].Vista + "/" + vistasControllers[indx].Pagina,
        beforeSend: function () {
            LoadingOn("Validando Usuario");
        },
        success: function (data) {
            if (data.indexOf("<!-- LOGIN FORMULARIO -->") >= 0) {
                location.reload();
            } else {
                $('#divMaestro').html(data);
                LoadingOff();
            } 
        },
        error: function (error) {
            ErrorLog(error.responseText, "Abri Menu de Opción");
        }
    });
});

// DOCUMENT QUE CONTROLA EL BOTON QUE RETORNA A LA PANTALLA PRINCIPAL
$(document).on('click', '#pantallaPrincipal', function () {
    mostrarCalendario();
});

// DOCUMENT QUE CONTROLA EL MUESTREO DE EXTENSIONES
$(document).on('click', '#extensiones', function () {
    llamarExtensiones();
});

// --------------- [ BOTONES Y CONTROLES DEL CALENDARIO ] ---------------
// DOCUMENT - BOTON QUE CONTROLA LOS ELEMENTOS DEL SELECTOR DE VISTA EN CALENDARIO (MENU DERECHA)
$(document).on('click', 'li[name="carruselopc"]', function () {
    $('li[name="carruselopc"]').removeClass("active");
    $(this).addClass("active");
});

// --------------------------------------------------------
// FUNCIONES GENERALES

// FUNCION INICIAL DEL HTML DE INDEX
function indexParams() {
    document.getElementsByTagName('body')[0].style.backgroundColor = '#17a2b8';
}

// FUNCION INICIAL DEL HTML PRINCIPAL
function principalParams() {
    $.ajax({
        type: "POST",
        contentType: "application/x-www-form-urlencoded",
        url: "/Home/BarraMenu",
        beforeSend: function () {
            LoadingOn("Asignando parametros...");
        },
        success: function (data) {
            $('#barraMenuPrincipal').html(data);
            $.ajax({
                type: "POST",
                contentType: "application/x-www-form-urlencoded",
                url: "/Home/UsuarioParametros",
                dataType: "JSON",
                success: function (data) {
                    $(data).each(function (key, value) {
                        if (value.Visible) {
                            $('#' + value.Nombre).attr("id", value.IdHTML);
                        } else {
                            $('#' + value.Nombre).parent().remove();
                        }
                    });
                    LoadingOff();
                    $.getScript("../Scripts/servidornotifs.js", function () { });
                    mostrarCalendario();
                },
                error: function (error) {
                    ErrorLog(error.responseText, "principalParams");
                }
            });
        },
        error: function (error) {
            ErrorLog(error.responseText, "principalParams");
        }
    });
}

// ----------------------- [ CALENDARIO ] -----------------------
// FUNCION QUE LLENA EL CALENDARIO PRINCIPAL
function mostrarCalendario() {
    $.ajax({
        type: "POST",
        contentType: "application/x-www-form-urlencoded",
        url: "/Home/Calendario",
        beforeSend: function () {
            LoadingOn("Cargando Calendario");
        },
        success: function (data) {
            $('#divMaestro').html(data);
            $.ajax({
                type: "POST",
                contentType: "application/x-www-form-urlencoded",
                url: "/Home/CalendarioParams",
                dataType: "JSON",
                beforeSend: function () {
                    LoadingOn("Aplicando Calendario");
                    calendarioMaestroGLOBAL = $('#calendarioActividades').fullCalendar({
                        header: { left: 'title', right: 'prev,next' },
                        eventClick: function (calEvent, jsEvent, view) {
                            /*console.log(calEvent);
                            console.log(jsEvent);
                            console.log(view);*/
                            MsgAlerta("Actividad de:", calEvent.description, 2500, "info");
                        },
                    });

                    $('.fc-prev-button').attr("id", "antMesCalendario");
                    $('#antMesCalendario').addClass("btn btn-sm btn-info");
                    $('#antMesCalendario').removeClass("fc-prev-button").removeClass("fc-button").removeClass("fc-state-default").removeClass("fc-corner-left");

                    $('.fc-next-button').attr("id", "sigMesCalendario");
                    $('#sigMesCalendario').addClass("btn btn-sm btn-info");
                    $('#sigMesCalendario').removeClass("fc-next-button").removeClass("fc-button").removeClass("fc-state-default").removeClass("fc-corner-right");
                    $('#sigMesCalendario').css("margin-left", "5px");
                    $('#calendarioActividades').css("padding", "10px");
                    $('.fc-left').addClass("badge badge-info");
                },
                success: function (data) {
                    if (data.ActividadesGrupales !== undefined) {
                        calendarioMaestroGLOBAL.fullCalendar('addEventSource', data.ActividadesGrupales);
                        $('#calendarioHorarioActs').html(data.HorarioActividades);
                        $('#calendarioIndividualesActs').html(data.HorarioActIndividuales);
                        $('#calendarioAgendaCitas').html(data.HorarioCitas);
                        LoadingOff();
                    } else {
                        ErrorLog(data.responseText, "Cargando Parametros Calendarios");
                    }
                },
                error: function (error) {
                    ErrorLog(error.responseText, "Cargando Parametros Calendarios");
                }
            });
        },
        error: function (error) {
            ErrorLog(error, "Cargar Calendario");
        }
    });
}
/*
 calendario.fullCalendar('addEventSource', [
                {
                    title: 'COORD MEDICA',
                    description: 'Lorem ipsum 1...',
                    start: '2020-02-01',
                    end: '2020-02-06',
                    color: '#52BE80',
                    textColor: '#FFFFFF',
                },
                {
                    title: 'COORD PSICOLOGICA',
                    description: 'Lorem ipsum 1...',
                    start: '2020-02-01',
                    end: '2020-02-06',
                    color: '#DC7633',
                    textColor: '#FFFFFF',
                },
                {
                    title: 'COORD. DEPORTIVA',
                    description: 'Lorem ipsum 1...',
                    start: '2020-02-01',
                    end: '2020-02-06',
                    color: '#EC7063',
                    textColor: '#FFFFFF',
                },
                {
                    title: 'COORD. CONSEJERIA',
                    description: 'Lorem ipsum 1...',
                    start: '2020-02-01',
                    end: '2020-02-06',
                    color: '#F4D03F',
                    textColor: '#FFFFFF',
                },
                {
                    title: 'AL ANON',
                    description: 'Lorem ipsum 1...',
                    start: '2020-02-01',
                    end: '2020-02-06',
                    color: '#5DADE2',
                    textColor: '#FFFFFF',
                },
                {
                    title: 'COORD. ESPIRITUAL',
                    description: 'Lorem ipsum 1...',
                    start: '2020-02-01',
                    end: '2020-02-06',
                    color: '#A569BD',
                    textColor: '#FFFFFF',
                },
            ]);
 */

// ----------------------- [ EXTENSIONES ] -----------------------
// FUNCION QUE MANDA LLAMAR LAS EXTENSIONES
function llamarExtensiones() {
    $.ajax({
        type: "POST",
        contentType: "application/x-www-form-urlencoded",
        url: "/Extensiones/Index",
        beforeSend: function () {
            LoadingOn("Validando Extensiones");
        },
        success: function (data) {
            $('body').append(data);
            $('#extensionesModal').modal('show');
            $('#myModal').on('hidden.bs.modal', function (e) {
                $('#extensionesModal').remove();
            });
            LoadingOff();
        },
        error: function (error) {
            ErrorLog(error.responseText, "Abri Menu de Extensiones");
        }
    });
}