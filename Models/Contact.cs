namespace MinimalCrud.Models;

// Classe entidade que será mapeada para tabela 'contacts'.
public partial class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
