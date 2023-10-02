function ocultarError() {
    try {
       let alertError = document.querySelector("#errorBody");
        if (alertError != null && alertError.style.display != "none") {
            alertError.style.display = "none"
        }
    } catch (error) {
        console.error(error);
    }

}
function errorMostrado() {
    try {
        let alertError = document.querySelector("#errorBody");
        if (alertError != null && alertError.style.display != "none") {
            return alertError.style.display != "none"
        } else {
            return false
        }
    } catch (error) {
        console.error(error);
    }
}