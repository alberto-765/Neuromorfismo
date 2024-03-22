using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Neuromorfismo.FrontEnd.Dto;

namespace Neuromorfismo.FrontEnd.WebApp.Shared.Reutilizables {
    public partial class Error {
        // Configuracion de la app
        [Inject] private IConfiguration configuration { get; set; } = null!;
        [Inject] private IOptionsSnapshot<ImagenesServerDto> _imgOptions { get; set; } = null!;

        // Texto de error para pintar
        [Parameter] public string mensajeError { get; set; } = "<span>Página no encontrada</span>";
        [Parameter] public string? NombreImagen { get; set; } // Nombre imagen

        private MarkupString errorParaPintar { get; set; }
        private string UrlImagen { get; set; } = string.Empty;

        protected override void OnInitialized() {
            try {
                errorParaPintar = new MarkupString(mensajeError);

                // Obtenemos url completa de la imagen
                if (string.IsNullOrWhiteSpace(NombreImagen)) {
                    throw new ArgumentNullException();
                }

                ImagenesServerDto? imagenesServerDto = _imgOptions.Value;
                string? baseUrl = configuration.GetSection("ApiSettings")["ImgUrl"];
                NombreImagen = imagenesServerDto.GetType().GetProperty(NombreImagen)?.GetValue(imagenesServerDto)?.ToString();

                // Validamos que la base de la imagen no sea null
                if (string.IsNullOrWhiteSpace(baseUrl)) {
                    throw new ArgumentNullException();
                }

                UrlImagen = new Uri(new Uri(baseUrl), NombreImagen).ToString();

                // Validamos la url completa de la imagen no sea null
                if (string.IsNullOrWhiteSpace(UrlImagen)) {
                    throw new ArgumentNullException();
                }
            } catch (Exception) {
                throw;
            }
        }
    }
}
