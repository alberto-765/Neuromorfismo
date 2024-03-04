
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


// Hacer scroll hacia un elemento
function ScrollHaciaElemento(selectorElemento, poxY) {
    let elemento = document.querySelector(selectorElemento);
    elemento.scrollIntoView({
        behavior: "smooth",
        block: poxY,
        inline: "nearest"
    })
}


// Hacer scroll al bottom de la pantalla
function ScrollBottom() {
    scrollTo({
        left: 0,
        top: document.body.scrollHeight,
        behavior: "smooth"
    });
}

function DescargarExcel(filename, bytesBase64) {
    // Cremos link para simular descarga del excel
    let link = document.createElement('a');

    // Nombre del archivo
    link.download = filename;

    // Cargamos el tipo de archivo y los bytes
    link.href = window.URL.createObjectURL(new Blob([bytesBase64], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" }));
    document.body.appendChild(link); 

    link.click();
    document.body.removeChild(link);
}

// Convertir html en una imagen con html2canvas
async function GenerarImagenDeHtml(id) {
    const contenedor = document.getElementById(id);
    let base64 = "";

    // Generamos base64 de la imagen
    if (contenedor != null) {
        await html2canvas(contenedor).then(canvas => {
            base64 = canvas.toDataURL()
        }); 
    }

    return base64;
}


// Animacion FadeIn
function FadeIn(selectorElemento, duracion) {
    let elemento = document.querySelector(selectorElemento);
    if (elemento != null) {
        elemento.style.opacity = 0;
        elemento.style.display = "block";

        let tiempoInicial = performance.now();

        // La animación se realiza midiendo el momento de comienzo y de finalización
        function fade() {
            let tiempoTranscurrido = performance.now() - tiempoInicial;

            // Calculamos la opacidad con el tiempo transcurrido y el deseado
            let opacidad = (tiempoTranscurrido / duracion);

            // Si la opacidad llega a 1 paramos la animacion
            if (opacidad >= 1) {
                elemento.style.opacity = 1;
                return;
            }

            elemento.style.opacity = opacidad;

            // Continuamos con la animacion
            requestAnimationFrame(fade);
        }

        fade();
        return true;
    }
}

// Animacion FadeOut
function FadeOut(selectorElemento, duracion) {
    let elemento = document.querySelector(selectorElemento);
    if (elemento != null) {
        elemento.style.opacity = 1;

        let tiempoInicial = performance.now();

        // La animación se realiza midiendo el momento de comienzo y de finalización
        function fade() {
            let tiempoTranscurrido = performance.now() - tiempoInicial;

            // Calculamos la opacidad con el tiempo transcurrido y el deseado
            let opacidad = 1-(tiempoTranscurrido / duracion);

            // Si la opacidad llega a 1 paramos la animacion
            if (opacidad <= 0) {
                elemento.style.opacity = 0;
                elemento.style.display = "none";
                return;
            }

            elemento.style.opacity = opacidad;

            // Continuamos con la animacion
            requestAnimationFrame(fade);
        }

        fade();
        return true;
    }
}

