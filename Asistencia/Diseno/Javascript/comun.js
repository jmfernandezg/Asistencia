

function validarRFC(f) {
    return /^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z\d]{3})?$/.test($("#" + f).val());
}


function permitirSoloNumeros(f) {
    if (!/^\d*$/.test(f.value)) {
        f.value = f.value.replace(/[^0-9\.]+/g, "");
    }

}



function mensajeExito(m, t) {
    toastr.options.positionClass = 'toast-top-full-width';
    toastr.options.extendedTimeOut = 0; //1000;
    toastr.options.timeOut = 3000;
    toastr.options.fadeOut = 250;
    toastr.options.fadeIn = 250; toastr.success(m, t);
}

function mensajeNotificar(m, t) {
    toastr.options.positionClass = 'toast-top-full-width';
    toastr.options.extendedTimeOut = 0; //1000;
    toastr.options.timeOut = 3000;
    toastr.options.fadeOut = 250;
    toastr.options.fadeIn = 250; toastr.info(m, t);
}
function mensajeAdvertir(m, t) {
    toastr.options.progressBar = true;
    toastr.options.positionClass = 'toast-top-full-width';
    toastr.options.extendedTimeOut = 0; //1000;
    toastr.options.timeOut = 3000;
    toastr.options.fadeOut = 250;
    toastr.options.fadeIn = 250; toastr.warning(m, t);
}
function mensajeError(m, t) {
    toastr.options.progressBar = true;
    toastr.options.positionClass = 'toast-top-full-width';
    toastr.options.extendedTimeOut = 0; //1000;
    toastr.options.timeOut = 3000;
    toastr.options.fadeOut = 250;
    toastr.options.fadeIn = 250; toastr.error(m, t);
}


function validar(obj, tx) {
    if ($("#" + obj).val() == null || $("#" + obj).val().length == 0) {
        mensajeAdvertir("Es requerido capturar el campo: " + tx, "Campo Requerido");
        return false;
    }
    return true;
}

function mostrarDialogo() {
    $("#dialogo").dialog("open");
    $("#spinner").spin()

}




function validarFecha(obj, tx) {
    if (!isValidDate($("#" + obj).val())) {
        mensajeAdvertir("Es requerido capturar la fecha válida en el campo: " + tx + " Formato AAAA/MM/DD. Ejemplo: 2013/12/31", "Fecha Inválida");
        return false;
    }
    return true;
}




function isValidDate(dateString) {
    // First check for the pattern
    if (!/^\d{4}\/\d{2}\/\d{2}$/.test(dateString))
        return false;

    // Parse the date parts to integers
    var parts = dateString.split("/");
    var day = parseInt(parts[2], 10);
    var month = parseInt(parts[1], 10);
    var year = parseInt(parts[0], 10);

    // Check the ranges of month and year
    if (year < 1913 || year > 2050 || month == 0 || month > 12)
        return false;

    var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    // Adjust for leap years
    if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
        monthLength[1] = 29;

    // Check the range of the day
    return day > 0 && day <= monthLength[month - 1];
};