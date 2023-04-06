using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_j_prd_lst")]
    public class ProduitListe
    {
        public ProduitListe()
        {
            
        }

        [Column("prd_id")]
        public int ProduitId { get; set; }

        [Column("lst_id")]
        public int ListeId { get; set; }

        [InverseProperty("ProduitListeProduitNavigation")]
        public virtual Produit? ProduitProduitListeNavigation { get; set; } = null!;

        [InverseProperty("ProduitListeListeNavigation")]
        public virtual Liste? ListeProduitListeNavigation { get; set; } = null!;
    }
}
