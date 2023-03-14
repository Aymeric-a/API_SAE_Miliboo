﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommandesController : ControllerBase
    {
        private readonly IDataRepositoryCommande<Commande> dataRepository;

        public CommandesController(IDataRepositoryCommande<Commande> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Commandes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commande>>> GetCommandes()
        {
            return await dataRepository.GetAll();
        }

        // GET: api/Commandes/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Commande>> GetCommande(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Commandes/5
        [HttpGet("{id}")]
        [ActionName("GetCommandesByIdClient")]
        public async Task<ActionResult<IEnumerable<Commande>>> GetCommandesByIdClient(int clientId)
        {
            var produit = await dataRepository.GetAllCommandeByClientId(clientId);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // PUT: api/Commandes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommande(int id, Commande commande)
        {
            if (id != commande.CommandeId)
            {
                return BadRequest();
            }

            var userToUpdate = await dataRepository.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, commande);
                return NoContent();
            }
        }

        // POST: api/Commandes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Commande>> PostCommande(Commande commande)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(commande);


            return CreatedAtAction("GetById", new { id = commande.CommandeId }, commande);
        }

        // DELETE: api/Commandes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommande(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);
            if (produit == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(produit.Value);

            return NoContent();
        }

        //private bool CommandeExists(int id)
        //{
        //    return _context.Commandes.Any(e => e.CommandeId == id);
        //}
    }
}