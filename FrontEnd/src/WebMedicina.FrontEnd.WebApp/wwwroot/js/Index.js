// Bloquear el scroll de un elemento
function bloquearScroll(claseElemento, ejes) {
    let elemento = document.querySelector(claseElemento);

    if (elemento) {
        // Bloqueamos eje x, eje y, o ambos
        switch (ejes.toLowerCase()) {
            case "x":
                elemento.style.overflowX = "hidden";
                break;
            case "y":
                elemento.style.overflowY = "hidden";
                break;
            case "xy":
                elemento.style.overflow = "hidden";
                break;
        }
    }
}


function desbloquearScroll(claseElemento, ejes) {
    let elemento = document.querySelector(claseElemento);

    if (elemento) {
        // Bloqueamos eje x, eje y, o ambos
        switch (ejes.toLowerCase()) {
            case "x":
                elemento.style.overflowX = "";
                break;
            case "y":
                elemento.style.overflowY = "";
                break;
            case "xy":
                elemento.style.overflow = "";
                break;
        }
    }
}

// Simular click en un boton
function EnviarForm(identificador) {
    try {
        let form = document.querySelector(identificador)

        // Validamos que es un formulario
        if (form.tagName == "FORM") {
            form.submit();
        }
    } catch (e) {
        throw e;
    }
}
