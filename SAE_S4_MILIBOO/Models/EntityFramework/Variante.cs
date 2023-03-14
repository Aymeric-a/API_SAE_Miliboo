using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_variante_vrt")]
    public class Variante
    {
        public Variante()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("vrt_id")]
        public int IdVariante { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("IdCouleur")]
        [Column("clr_id")]
        public int IdCouleur { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("IdProduit")]
        [Column("prd_id")]
        public int IdProduit { get; set; }

        [Column("vrt_prix", TypeName = "numeric")]
        [Required]
        public double Prix { get; set; }

        [Column("vrt_promo", TypeName = "numeric(4,2)")]
        public double Promo { get; set; }

        [Column("vrt_description")]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [Column("vrt_stock")]
        [Required]
        public int Stock { get; set; }

        [Column("vrt_date_creation", TypeName ="date")]
        public DateTime? DateCreation { get; set; }


        [InverseProperty("VariantesCouleurNavigation")]
        public virtual Couleur CouleurVarianteNavigation { get; set; } = null!;

        [InverseProperty("VariantesProduitNavigation")]
        public virtual Produit ProduitVarianteNavigation { get; set; } = null!;

        [InverseProperty("VariantesLignePanierNavigation")]
        public virtual ICollection<LignePanier> LignePanierVarianteNavigation { get; set; } = null!;

        [InverseProperty("VarianteAvisNavigation")]
        public virtual ICollection<Avis> AvisVarianteNavigation { get; set; } = null!;

        [InverseProperty("VariantePhotoNavigation")]
        public virtual ICollection<Photo> PhotosVarianteNavigation { get; set; } = null!;

        [InverseProperty("VarianteLigneCommandeNavigation")]
        public virtual ICollection<LigneCommande> LignesCommandeVarianteNavigation { get; set; } = null!;
    }
}