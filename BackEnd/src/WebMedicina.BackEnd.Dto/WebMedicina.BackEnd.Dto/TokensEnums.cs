using System.ComponentModel;

namespace WebMedicina.BackEnd.Dto {


    // Estados CrearUsuarioYMedico
    public enum EstadoCrearUsuario {
        [Description("Modelo usuario registro no válido")]
        ModeloKO,

        [Description("Ya existe un usuario con el mismo username.")]
        IdentityUserKO,

        [Description("Error al insertar el nuevo medico en la tabla de medicos")]
        MedicoKO,

        [Description("Nuevo usuario y medico creado correctamente")]
        UserYMedicoOK,
    }
}
