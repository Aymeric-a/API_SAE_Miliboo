using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_j_carte_enregistree_cbe")]
    public class CarteEnregistree
    {
        public CarteEnregistree()
        {
            
        }

        
        [Column("cbr_id")]
        public int CarteBancaireId { get; set; }

        
        [Column("clt_id")]
        public int ClientId { get; set; }


        //Lien vers les cartes bancaires
        [InverseProperty("CartesEnregistreesNavigation")]
        public virtual CarteBancaire CarteLieeNavigation { get; set; } = null!;

        //Lien vers les clients
        [InverseProperty("ClientsEnregistresNavigation")]
        public virtual Client ClientLieNavigation { get; set; } = null!;
    }
}
