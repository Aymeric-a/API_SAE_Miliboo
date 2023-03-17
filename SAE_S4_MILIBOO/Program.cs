using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.DataManager;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var ctx = new MilibooDBContext())
            {
                Couleur couleurBeige = ctx.Couleurs.FirstOrDefault(c => c.Libelle == "beige");
                

                ctx.Entry(couleurBeige).Collection(c => c.VariantesCouleurNavigation).Load();
                foreach(var film in couleurBeige.VariantesCouleurNavigation)
                {
                    Console.WriteLine(film.Prix);
                }
                
            }

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<MilibooDBContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("BDDistante")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDataRepositoryCommande<Commande>, CommandeManager>();
            builder.Services.AddScoped<IDataRepositoryAdresse<Adresse>, AdresseManager>();
            builder.Services.AddScoped<IDataRepositoryProduits<Produit>, ProduitManager>();
            builder.Services.AddScoped<IDataRepositoryVariante<Variante>, VarianteManager>();
            builder.Services.AddScoped<IDataRepositoryCouleur<Couleur>, CouleurManager>();

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