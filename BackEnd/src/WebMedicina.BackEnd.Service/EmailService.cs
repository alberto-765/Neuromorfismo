using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using WebMedicina.BackEnd.Dto;
using WebMedicina.BackEnd.ServicesDependencies;

namespace WebMedicina.BackEnd.Service; 
public class EmailService : IEmailService {
    private readonly EmailConfig _config;    

    public EmailService(IOptionsSnapshot<EmailConfig> config) { 
        _config = config.Value;
    }

    /// <summary>
    /// Enviamos mensaje de correo
    /// </summary> 
    public void Send(string asunto, string mensaje, MemoryStream imagen) {

        // Generamos mensaje del correo
        MailMessage mail = new() {
            From = new(_config.Usuario),
            Subject = asunto,
            Body = mensaje,
            IsBodyHtml = true
        };

        // Mapeamos los destinatarios
        foreach (string correo in _config.Destinatarios) {
            mail.To.Add(new(correo));
        }

        // Adjuntamos imagen de progreso del paciente
        mail.Attachments.Add(new Attachment(imagen, "image/png"));

        // Conectamos con el servidor de email
        SmtpClient smtp = new(_config.Host, _config.Puerto) {
            Credentials = new NetworkCredential(_config.Usuario, _config.Contrasena)
        };

        smtp.SendAsync(mail, null);
    }
}

