using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_liste_lst")]
    public class Liste
    {
        public Liste()
        {
            
        }

        [Column("lst_id")]
        public int ListeId { get; set; }

        [Column("clt_id")]
        public int ClientId { get; set; }

        [Column("lst_libelle")]
        [StringLength(100)]
        public string Libelle { get; set; }

        [Column("lst_date_derniere_modif", TypeName ="date")]
        public DateTime DerniereModif { get; set; }

        [Column("lst_date_creation", TypeName = "date")]
        public DateTime DateCreation { get; set; }


        [InverseProperty("ListeClientNavigation")]
        public virtual Client? ClientListeNavigation { get; set; } = null!;

        [InverseProperty("ListeProduitListeNavigation")]
        public virtual ICollection<ProduitListe> ProduitListeListeNavigation { get; set; } = new List<ProduitListe>();
    }
}
