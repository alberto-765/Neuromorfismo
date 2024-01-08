namespace WebMedicina.Shared.Dto.Usuarios {

    public record UserToken {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
