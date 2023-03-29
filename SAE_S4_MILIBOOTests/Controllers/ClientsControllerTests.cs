using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE_S4_MILIBOO.Controllers;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_S4_MILIBOO.Controllers.Tests
{
    [TestClass()]
    public class ClientsControllerTests
    {
        [TestMethod()]
        public void GetClientTest_ReturnsExistingClient_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(clt);
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClient(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(clt, actionResult.Value as Client);
        }

        [TestMethod]
        public void GetClientTest_ReturnsNotFound_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(clt);
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClient(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAllClientsTest_ReturnsAllClients_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            Client clt2 = clt;
            clt2.ClientId = 2;
            clt2.Mail = "tristan.ginet2@gmail.com";

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(new List<Client> { clt, clt2 });
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetAllClients().Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(new List<Client> { clt, clt2 }, actionResult.Value as Client);
        }

        [TestMethod]
        public void GetClientByEmailTest_ReturnsExistingClient_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetClientByEmail("tristan.ginet@gmail.com").Result).Returns(clt);
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClientByEmail("tristan.ginet@gmail.com").Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(clt, actionResult.Value as Client);
        }

        [TestMethod]
        public void GetClientByEmailTest_ReturnsNotFound_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetClientByEmail("tristan.ginet2@gmail.com").Result).Returns(new NotFoundResult());
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClientByEmail("tristan.ginet2@gmail.com").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetClientByPortable_ReturnsExistingClient_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetClientByPortable("0606060606").Result).Returns(clt);
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClientByPortable("0606060606").Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(clt, actionResult.Value as Client);
        }

        [TestMethod]
        public void GetClientByPortable_ReturnsNotFound_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetClientByPortable("0606060606").Result).Returns(clt);
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClientByPortable("0707070707").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        public void GetAllClientsByNomPrenom_ReturnsExistingClient_Moq()
        {
            Assert.Fail();
        }

        public void GetAllClientsByNomPrenom_ReturnsNotFound_Moq()
        {
            Assert.Fail();
        }

        public void GetAllClientsNewsletterMTest_ReturnsExistingClient_Moq()
        {
            Assert.Fail();
        }
        public void GetAllClientsNewsletterPTest_ReturnsExistingClient_Moq()
        {
            Assert.Fail();
        }

        public void PostClientTest()
        {
            Assert.Fail();
        }

        public void PutClientTest()
        {
            Assert.Fail();
        }

        public void DeleteClientTest()
        {
            Assert.Fail();
        }
    }
}