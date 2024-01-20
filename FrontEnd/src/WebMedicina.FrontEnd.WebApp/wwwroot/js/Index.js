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

// Activar scroll de un contenedor
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

// Simular envio de un formulario via js
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

// Hacer scroll hacia un elemento
function ScrollHaciaElemento(idElemento, poxY) {
    try {
        let elemento = document.getElementById(idElemento);
        elemento.scrollIntoView({
            behavior: "smooth",
            block: poxY,
            inline: "nearest"
        })
    } catch (e) {
        throw e;
    }
}


// Hacer scroll al bottom de la pantalla
function ScrollBottom() {
    scrollTo({
        left: 0,
        top: document.body.scrollHeight,
        behavior: "smooth"
    });
}