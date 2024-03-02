
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

// Copiar en el portapeles
async function CopyToClipboard(text) {
    // Creamos elemento invisible
    let link = document.createElement("a")
    link.setAttribute("class", "d-none");

    // Al hacer click se copiará al portapeles el texto
    link.addEventListener("click",  async () => {
        await navigator.clipboard.writeText(text);
    });

    // Añadimos el elemento, simulamos click y lo borramos
    document.body.append(link);
    link.click();
    link.remove();
}