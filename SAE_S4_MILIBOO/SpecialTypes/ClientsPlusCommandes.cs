using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.SpecialTypes
{
    public class ClientsPlusCommandes
    {
        private ClientSansMdp client;
        private List<Commande> listCommandes;

        public ClientsPlusCommandes()
        {
        }

        public ClientsPlusCommandes(ClientSansMdp client, List<Commande> listCommandes)
        {
            this.Client = client;
            this.ListCommandes = listCommandes;
        }

        public ClientSansMdp Client
        {
            get
            {
                return this.client;
            }

            set
            {
                this.client = value;
            }
        }

        public List<Commande> ListCommandes
        {
            get
            {
                return this.listCommandes;
            }

            set
            {
                this.listCommandes = value;
            }
        }
    }
}
