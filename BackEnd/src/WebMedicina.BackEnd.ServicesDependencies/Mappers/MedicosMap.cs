
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.ServicesDependencies.Mappers {
    public static class MedicosMap {
        // UserRegistroDto To Model
        public static MedicosModel ToModel(this UserRegistroDto user) =>
            new() {
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                FechaCreac = user.FechaCreac,
                FechaUltMod = user.FechaUltMod,
                FechaNac = user.FechaUltMod,
                UserLogin = user.UserLogin,
                Rol = user.Rol,
                Sexo = user.Sexo,
            };

        // MedicosModel to UserRegistroDto
        public static UserRegistroDto ToModel(this MedicosModel user) =>
           new() {
               Nombre = user.Nombre,
               Apellidos = user.Apellidos,
               FechaCreac = user.FechaCreac,
               FechaUltMod = user.FechaUltMod,
               FechaNac = user.FechaUltMod,
               UserLogin = user.UserLogin,
               Rol = user.Rol,
               Sexo = user.Sexo,
           };

        // MedicosModel to UserUploadDto
        public static UserUploadDto ToDto (this MedicosModel user) =>
            new() {
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                FechaCreac = user.FechaCreac,
                FechaUltMod = user.FechaUltMod,
                FechaNac = user.FechaUltMod,
                UserLogin = user.UserLogin,
                Rol = user.Rol,
                Sexo = user.Sexo,
            };

        // UserUploadDto to MedicosModel
        public static MedicosModel ToModel(this UserUploadDto user) =>
            new() {
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                FechaCreac = user.FechaCreac,
                FechaUltMod = user.FechaUltMod,
                FechaNac = user.FechaUltMod,
                UserLogin = user.UserLogin,
                Rol = user.Rol,
                Sexo = user.Sexo,
            };


        // MedicosModel to UserInfoDto
        public static UserInfoDto ToUserInfoDto(this MedicosModel user) =>
            new() {
                IdMedico = user.IdMedico,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                FechaCreac = user.FechaCreac,
                FechaUltMod = user.FechaUltMod,
                FechaNac = user.FechaUltMod,
                UserLogin = user.UserLogin,
                Rol = user.Rol,
                Sexo = user.Sexo,
            };

    }
}
