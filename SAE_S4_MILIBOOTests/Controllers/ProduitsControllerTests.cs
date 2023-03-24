using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE_S4_MILIBOO.Controllers;
using SAE_S4_MILIBOO.Models.DataManager;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SAE_S4_MILIBOO.Controllers.Tests
{
    [TestClass()]
    public class ProduitsControllerTests
    {
        private readonly MilibooDBContext _context;
        private readonly ProduitsController _controller;
        private IDataRepositoryProduits<Produit> _dataRepository;

        public ProduitsControllerTests()
        {

            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql("Server=localhost;port=5432;Database=filmrating; uid=bubu; password=password;"); // Chaine de connexion à mettre dans les ( )
            _context = new MilibooDBContext(builder.Options);
            _dataRepository = new ProduitManager(_context);
            _controller = new ProduitsController(_dataRepository);
        }

        [TestMethod()]
        public async void GetProduitsTest()
        {
            ActionResult<IEnumerable<Produit>> users = await _controller.GetProduits();
            CollectionAssert.AreEqual(_context.Produits.ToList(), users.Value.ToList(), "La liste renvoyée n'est pas la bonne.");
        }

        [TestMethod]
        public void GetProduitById_ReturnsExistingProduct_Moq()
        {
            // Arrange
            Produit user = new Produit
            {
                IdProduit = 1,
                CategorieId = 15,
                CollectionId = null,
                Libelle = "Canapé 3 places éco-responsable en tissu recyclé naturel FOREST",
                Description = null,
                InscructionsEntretien = null,
                HauteurPieds = 186,
                Revetement = null,
                Matiere = null,
                MatierePieds = null,
                TypeMousseAssise = null,
                TypeMousseDossier = null,
                DensiteAssise = null,
                PoidsColis = null,
                DimTotale = new TDimensions(186, 105, 65),
                DimAssise = new TDimensions(null, (decimal)65.4, null),
                DimDossier = new TDimensions(null, null, null),
                DimColis = new TDimensions(null, null, null),
                DimDeplie = new TDimensions(null, null, null),
                DimAccoudoir = new TDimensions(null, null, null),
                MadeInFrance = false
            };

            var mockRepository = new Mock<IDataRepositoryProduits<Produit>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);
            var produitController = new ProduitsController(mockRepository.Object);

            // Act
            var actionResult = produitController.GetProduit(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Result);
            Assert.AreEqual(user, actionResult.Value as Produit);
        }
    } 
}