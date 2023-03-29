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
        public void GetClientTest_ReturnsExistingProduct_Moq()
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

        //[TestMethod]
        //public void GetClientTest_ReturnsNotFound_Moq()
        //{
        //    var mockRepository = new Mock<IDataRepositoryClient<Client>>();
        //    mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns();
        //    var clientController = new ClientsController(mockRepository.Object);

        //    // Act
        //    var actionResult = clientController.GetClient(1).Result;

        //    // Assert
        //    Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        //}

        [TestMethod]
        public void GetProduitById_ReturnsNull_Moq()
        {
            var mockRepository = new Mock<IDataRepositoryProduits<Produit>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(new List<Produit> { });

            var produitController = new ProduitsController(mockRepository.Object);

            // Act
            var actionResult = produitController.GetProduit(1).Result;

            // Assert
            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        public void GetAllClientsTest()
        {
            Assert.Fail();
        }

        public void GetClientByEmailTest()
        {
            Assert.Fail();
        }

        public void GetClientByPortable()
        {
            Assert.Fail();
        }

        public void GetAllClientsByNomPrenom()
        {
            Assert.Fail();
        }

        public void GetAllClientsNewsletterMTest()
        {
            Assert.Fail();
        }
        public void GetAllClientsNewsletterPTest()
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