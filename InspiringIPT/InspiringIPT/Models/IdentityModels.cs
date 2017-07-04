using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace InspiringIPT.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        
    
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("name=DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        //**************************
        // adicionar as instruções para criar as 'tabelas'
        // o tipo DbSet para as tabelas. E <PotencialAluno> quer dizer uma coleção
        public virtual DbSet<PotencialAluno> PotencialAluno { get; set; }
        public virtual DbSet<Cursos> Cursos { get; set; }
        public virtual DbSet<Areas> Areas { get; set; }
        public virtual DbSet<Escola> Escola { get; set; }
        public virtual DbSet<TipoCurso> TipoCurso { get; set; }
        public virtual DbSet<OutrasAreas> OutrasAreas { get; set; }
        public virtual DbSet<OutrosCursos> OutrosCursos { get; set; }
        public virtual DbSet<Colaboradores> Colaboradores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();  // impede a EF de 'pluralizar' os nomes das tabelas
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();  // força a que a chave forasteira não tenha a propriedade 'on delete cascade'
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();  // força a que a chave forasteira não tenha a propriedade 'on delete cascade'

            base.OnModelCreating(modelBuilder);

        }
}