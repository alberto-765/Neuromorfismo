
using Neuromorfismo.BackEnd.Model;
using Neuromorfismo.Shared.Dto.Usuarios;

namespace Neuromorfismo.BackEnd.ServicesDependencies.Mappers {
    public static class MedicosMapers {
        // UserRegistroDto To Model
        public static MedicosModel ToModel(this UserRegistroDto user) =>
            new() {
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                FechaCreac = user.FechaCreac,
                FechaUltMod = user.FechaUltMod,
                FechaNac = user.FechaNac ?? DateTime.MinValue,
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
               FechaNac = user.FechaNac,
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
                FechaNac = user.FechaNac,
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
                FechaNac = user.FechaNac ?? DateTime.MinValue,
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
                FechaNac = user.FechaNac,
                UserLogin = user.UserLogin,
                Rol = user.Rol,
                Sexo = user.Sexo,
            };

        // UserInfoDto to MedicosModel 
        public static MedicosModel ToModel(this UserInfoDto user) =>
            new() {
                IdMedico = user.IdMedico,
                Nombre = user.Nombre ?? string.Empty,
                Apellidos = user.Apellidos ?? string.Empty,
                FechaCreac = user.FechaCreac,
                FechaUltMod = user.FechaUltMod,
                FechaNac = user.FechaNac,
                UserLogin = user.UserLogin,
                Rol = user.Rol ?? string.Empty,
                Sexo = user.Sexo ?? string.Empty,
            };

    }
}
