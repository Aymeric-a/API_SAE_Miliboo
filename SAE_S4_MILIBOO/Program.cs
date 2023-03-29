    using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SAE_S4_MILIBOO.Models.DataManager;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<MilibooDBContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("BDDistante")));

            //builder.Services.AddControllers().AddNewtonsoftJson(options =>
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //    );

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDataRepositoryCommande<Commande>, CommandeManager>();
            builder.Services.AddScoped<IDataRepositoryAdresse<Adresse>, AdresseManager>();
            builder.Services.AddScoped<IDataRepositoryProduits<Produit>, ProduitManager>();
            builder.Services.AddScoped<IDataRepositoryVariante<Variante>, VarianteManager>();
            builder.Services.AddScoped<IDataRepositoryCouleur<Couleur>, CouleurManager>();
            builder.Services.AddScoped<IDataRepositoryAvis<Avis>, AvisManager>();
            builder.Services.AddScoped<IDataRepositoryCarteBancaire<CarteBancaire>, CarteBancaireManager>();
            builder.Services.AddScoped<IDataRepositoryClient<Client>, ClientManager>();
            builder.Services.AddScoped<IDataRepositoryCollection<Collection>, CollectionManager>();
            builder.Services.AddScoped<IDataRepositoryLignePanier<LignePanier>, LignePanierManager>();
            builder.Services.AddScoped<IDataRepositoryListeSouhait<Liste>, ListeSouhaitManager>();
            builder.Services.AddScoped<IDataRepositoryPhoto<Photo>, PhotoManager>();
            builder.Services.AddScoped<IDataRepositoryCategorie<Categorie>, CategorieManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); 
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}