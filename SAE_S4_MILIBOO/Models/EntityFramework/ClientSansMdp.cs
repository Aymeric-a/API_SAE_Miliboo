namespace SAE_S4_MILIBOO.Models.EntityFramework
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
            ClientId = clientId;
            Mail = mail;
            Nom = nom;
            Prenom = prenom;
            Portable = portable;
            NewsMiliboo = newsMiliboo;
            NewsPartenaire = newsPartenaire;
            SoldeFidelite = soldeFidelite;
            DerniereConnexion = derniereConnexion;
            DateCreation = dateCreation;
            Civilite = civilite;
        }

        public int ClientId
        {
            get
            {
                return clientId;
            }

            set
            {
                clientId = value;
            }
        }

        public string Mail
        {
            get
            {
                return mail;
            }

            set
            {
                mail = value;
            }
        }

        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                nom = value;
            }
        }

        public string Prenom
        {
            get
            {
                return prenom;
            }

            set
            {
                prenom = value;
            }
        }

        public string Portable
        {
            get
            {
                return portable;
            }

            set
            {
                portable = value;
            }
        }

        public bool NewsMiliboo
        {
            get
            {
                return newsMiliboo;
            }

            set
            {
                newsMiliboo = value;
            }
        }

        public bool NewsPartenaire
        {
            get
            {
                return newsPartenaire;
            }

            set
            {
                newsPartenaire = value;
            }
        }

        public int SoldeFidelite
        {
            get
            {
                return soldeFidelite;
            }

            set
            {
                soldeFidelite = value;
            }
        }

        public DateTime? DerniereConnexion
        {
            get
            {
                return derniereConnexion;
            }

            set
            {
                derniereConnexion = value;
            }
        }

        public DateTime DateCreation
        {
            get
            {
                return dateCreation;
            }

            set
            {
                dateCreation = value;
            }
        }

        public string Civilite
        {
            get
            {
                return civilite;
            }

            set
            {
                civilite = value;
            }
        }
    }
}
