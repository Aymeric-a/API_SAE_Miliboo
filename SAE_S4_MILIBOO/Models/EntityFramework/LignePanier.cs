using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_panier_lgp")]
    public class LignePanier
    {
        public LignePanier()
        {
            
        }

 
        [Column("lgp_id")]
        public int LigneId { get; set; }

        [Column("pnr_id")]
        public int PanierId { get; set; }

        [Column("vrt_id")]
        public int VarianteId { get; set; }

        [Column("lgp_quantite")]
        public int Quantite { get; set; }

        [InverseProperty("LignesDansPanierNavigation")]
        public virtual Panier LigneDuPanierNavigation { get; set; } = null!;

        [InverseProperty("LignePanierVarianteNavigation")]
        public virtual Variante VariantesLignePanierNavigation { get; set; } = null!;
    }
}
