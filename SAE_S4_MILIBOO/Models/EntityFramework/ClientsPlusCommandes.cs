using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
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
            Client = client;
            ListCommandes = listCommandes;
        }

        public ClientSansMdp Client
        {
            get
            {
                return client;
            }

            set
            {
                client = value;
            }
        }

        public List<Commande> ListCommandes
        {
            get
            {
                return listCommandes;
            }

            set
            {
                listCommandes = value;
            }
        }
    }
}
