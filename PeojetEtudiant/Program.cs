using Microsoft.EntityFrameworkCore;
using PeojetEtudiant.Data;
using PeojetEtudiant.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuration des services
builder.Services.AddControllersWithViews();

// Configuration de la base de données (updated to SQLite)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Pipeline de requêtes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Initialisation de la base de données avec données de test
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Supprime et recrée la base de données (uniquement en développement)
        if (app.Environment.IsDevelopment())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        // Vérifie si des classes existent déjà
        if (!context.Classes.Any())
        {
            // Ajout de classes de test
            var classes = new List<Classse>
            {
                new Classse { labelle = "Informatique" },
                new Classse { labelle = "Mathématiques" },
                new Classse { labelle = "Physique" }
            };
            context.Classes.AddRange(classes);
            context.SaveChanges();

            // Ajout d'étudiants de test
            var etudiants = new List<Etudiants>
            {
                new Etudiants { Nom = "Dupont", Age = 20, ClasseId = classes[0].Id_c },
                new Etudiants { Nom = "Martin", Age = 23, ClasseId = classes[0].Id_c },
                new Etudiants { Nom = "Bernard", Age = 25, ClasseId = classes[1].Id_c },
                new Etudiants { Nom = "Petit", Age = 19, ClasseId = classes[1].Id_c },
                new Etudiants { Nom = "Robert", Age = 30, ClasseId = classes[2].Id_c }
            };
            context.Etudiants.AddRange(etudiants);
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erreur lors de l'initialisation de la base de données");
    }
}

app.Run();