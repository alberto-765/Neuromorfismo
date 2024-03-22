using Microsoft.AspNetCore.Identity;
using Neuromorfismo.Shared.Dto.UserAccount;

namespace Neuromorfismo.BackEnd.ServicesDependencies.Mappers;
public static class PerfilMapers {

    // Obtener response mensage de error o success al cambiar contraseña
    public static List<CodigosErrorChangePass> GetResponseMensage(this IdentityResult respuesta) {
        try {
            List<CodigosErrorChangePass> responseMensage = new();

            // Mapeamos los errores y obtenemos lista de codigos de error
            foreach (IdentityError error in respuesta.Errors) {
                // Obtenemos el mensaje personalizado dependiendo del tipo de error
                switch (error.Code) {
                    case "PasswordMismatch":
                    responseMensage.Add(CodigosErrorChangePass.ContraIncorrecta);
                    break;

                    // Errores de formato
                    case "PasswordRequiresUpper":
                    responseMensage.Add(CodigosErrorChangePass.FaltaMayuscula);
                    break;
                    case "PasswordRequiresDigit":
                    responseMensage.Add(CodigosErrorChangePass.FaltaNumero);
                    break;
                    case "PasswordRequiresNonAlphanumeric":
                    responseMensage.Add(CodigosErrorChangePass.FaltaCaractEspecial);
                    break;
                    case "PasswordTooShort":
                    responseMensage.Add(CodigosErrorChangePass.ContraCorta);
                    break;

                    default:
                    responseMensage.Add(CodigosErrorChangePass.ErrorDefault) ;
                    break;
                }
            }

            return responseMensage;


        } catch (Exception) {
            return new();
        }
    }


}

