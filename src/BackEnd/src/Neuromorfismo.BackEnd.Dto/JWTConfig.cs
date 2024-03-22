namespace Neuromorfismo.BackEnd.Dto {
    public class JWTConfig {
        public string Key { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string ValidezTokenEnHoras { get; set; } = default!;
        public string ValidezRefreshTokenEnDias { get; set; } = default!;

    }
}
