using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_panier_pnr")]
    public class Panier
    {
        public Panier()
        {

        }

       
        [Column("pnr_id")]
        public int PanierId { get; set; }

        [Column("clt_id")]
        public int ClientId { get; set; }

        [Column("pnr_derniere_modif", TypeName ="date")]
        public DateTime DerniereModif { get; set; }


        [InverseProperty("LigneDuPanierNavigation")]
        public virtual ICollection<LignePanier> LignesDansPanierNavigation { get; set; } = null!;

        [InverseProperty("PanierClientNavigation")]
        public virtual Client ClientPanierNavigation { get; set; } = null!;
    }
}
