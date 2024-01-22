namespace WebMedicina.BackEnd.Model;

public partial class Aspnetuserlogin
{
    public string LoginProvider { get; set; } = null!;

    public string ProviderKey { get; set; } = null!;

    public string? ProviderDisplayName { get; set; }

    public string UserId { get; set; } = null!;

    public virtual UserModel User { get; set; } = null!;
}
