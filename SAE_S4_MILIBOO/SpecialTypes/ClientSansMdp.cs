using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.SpecialTypes
{
    public class ClientSansMdp
    {
        private int clientId;
        private string mail;
        private string nom;
        private string prenom;
        private string portable;
        private bool newsMiliboo;
        private bool newsPartenaire;
        private int soldeFidelite;
        private DateTime? derniereConnexion;
        private DateTime dateCreation;
        private string civilite;

        public ClientSansMdp()
        {

        }

        public ClientSansMdp(int clientId, string mail, string nom, string prenom, string portable, bool newsMiliboo, bool newsPartenaire, 
            int soldeFidelite, DateTime? derniereConnexion, DateTime dateCreation, string civilite)
        {
            this.ClientId = clientId;
            this.Mail = mail;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Portable = portable;
            this.NewsMiliboo = newsMiliboo;
            this.NewsPartenaire = newsPartenaire;
            this.SoldeFidelite = soldeFidelite;
            this.DerniereConnexion = derniereConnexion;
            this.DateCreation = dateCreation;
            this.Civilite = civilite;
        }

        public int ClientId
        {
            get
            {
                return this.clientId;
            }

            set
            {
                this.clientId = value;
            }
        }

        public string Mail
        {
            get
            {
                return this.mail;
            }

            set
            {
                this.mail = value;
            }
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public string Prenom
        {
            get
            {
                return this.prenom;
            }

            set
            {
                this.prenom = value;
            }
        }

        public string Portable
        {
            get
            {
                return this.portable;
            }

            set
            {
                this.portable = value;
            }
        }

        public bool NewsMiliboo
        {
            get
            {
                return this.newsMiliboo;
            }

            set
            {
                this.newsMiliboo = value;
            }
        }

        public bool NewsPartenaire
        {
            get
            {
                return this.newsPartenaire;
            }

            set
            {
                this.newsPartenaire = value;
            }
        }

        public int SoldeFidelite
        {
            get
            {
                return this.soldeFidelite;
            }

            set
            {
                this.soldeFidelite = value;
            }
        }

        public DateTime? DerniereConnexion
        {
            get
            {
                return this.derniereConnexion;
            }

            set
            {
                this.derniereConnexion = value;
            }
        }

        public DateTime DateCreation
        {
            get
            {
                return this.dateCreation;
            }

            set
            {
                this.dateCreation = value;
            }
        }

        public string Civilite
        {
            get
            {
                return this.civilite;
            }

            set
            {
                this.civilite = value;
            }
        }
    }
}
