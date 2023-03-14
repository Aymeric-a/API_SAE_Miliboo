using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_j_pdt_lst")]
    public class ProduitListe
    {
        public ProduitListe()
        {
            
        }

        [Column("pdt_id")]
        public int ProduitId { get; set; }

        [Column("lst_id")]
        public int ListeId { get; set; }

        [InverseProperty("ListeProduitNavigation")]
        public virtual Produit ProduitDansListeNavigation { get; set; } = null!;

        [InverseProperty("ProduitListeNavigation")]
        public virtual Liste ListeDeProduitNavigation { get; set; } = null!;
    }
}
