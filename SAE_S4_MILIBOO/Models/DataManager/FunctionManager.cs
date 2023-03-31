using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using System.Collections.Generic;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class FunctionManager : IDataRepositoryFunction<ClientsPlusCommandes>
    {
        private readonly MilibooDBContext? milibooDBContext;

        readonly CommandeManager? commandeManager;
        readonly ClientManager? clientManager;
        public FunctionManager() { }

        public FunctionManager(MilibooDBContext context)
        {
            milibooDBContext = context;
            commandeManager = new CommandeManager(context);
            clientManager = new ClientManager(context);
        }

        public async Task<ActionResult<ClientsPlusCommandes>> GetInfosClientAndCommandes(string email)
        {
            var clientVar = await clientManager.GetClientByEmail(email);
            Client client = clientVar.Value;

            var allCommandsByClientVar = await commandeManager.GetAllCommandeByClientId(client.ClientId);
            List<Commande> allCommandsByClient = (List<Commande>)allCommandsByClientVar.Value;

            ClientsPlusCommandes finalResult = new ClientsPlusCommandes(removeMdp(client), allCommandsByClient);

            return finalResult;
        }

        public async Task<ActionResult<Client>> ReplacePassword(string newPassword, int idClient)
        {
            Client c = (from x in milibooDBContext.Clients
                          where x.ClientId == idClient
                          select x).First();
            c.Password = newPassword;
            milibooDBContext.SaveChanges();

            return c;
        }

        public ClientSansMdp removeMdp(Client client)
        {
            ClientSansMdp clientSansMdp = new ClientSansMdp();

            clientSansMdp.ClientId = client.ClientId;
            clientSansMdp.Mail = client.Mail;
            clientSansMdp.Nom = client.Nom;
            clientSansMdp.Prenom = client.Prenom;
            clientSansMdp.Portable = client.Portable;
            clientSansMdp.NewsMiliboo = client.NewsMiliboo;
            clientSansMdp.NewsPartenaire = client.NewsPartenaire;
            clientSansMdp.SoldeFidelite = client.SoldeFidelite;
            clientSansMdp.DerniereConnexion = client.DerniereConnexion;
            clientSansMdp.DateCreation = client.DateCreation;
            clientSansMdp.Civilite = client.Civilite;

            return clientSansMdp;
        }
    }
}
