namespace InspiringIPT.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<InspiringIPT.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;//permite a actualiza��o autom�tica da BD
        }

        protected override void Seed(InspiringIPT.Models.ApplicationDbContext context)
        {
            //########################################################
            //adiciona as �reas
            var areas = new List<Areas> {
                new Areas  {AreaID = 1, NomeArea = "Engenharia e Tecnologia"},
                new Areas  {AreaID = 2, NomeArea = "Gest�o"},
                new Areas  {AreaID = 3, NomeArea = "Design"},
                new Areas  {AreaID = 4, NomeArea = "Turismo"}

            };
            areas.ForEach(arar => context.Areas.AddOrUpdate(ar => ar.NomeArea, arar));
            context.SaveChanges();

            //########################################################
            //adiciona os Tipo do Curso
            var tiposcursos = new List<TipoCurso> {
                new TipoCurso  {TipoID = 1, Tipo = "Licenciatura"},
                new TipoCurso  {TipoID = 2, Tipo = "Mestrado"},
                new TipoCurso  {TipoID = 3, Tipo = "TeSPs"},
                new TipoCurso  {TipoID = 4, Tipo = "M23"}
            };
            tiposcursos.ForEach(tt => context.TipoCurso.AddOrUpdate(t => t.Tipo, tt));
            context.SaveChanges();

            //########################################################
            //adiciona as Escolas
            var escolas = new List<Escola> {
                new Escola  {EscolaID = 1, NomeEscola = "Escola Superior de Tecnologia de Tomar", SiglaEscola="ESTT"},
                new Escola  {EscolaID = 2, NomeEscola = "Escola Superior de Gest�o de Tomar", SiglaEscola="ESGT"},
                new Escola  {EscolaID = 3, NomeEscola = "Escola Superior de Tecnologia de Abrantes", SiglaEscola="ESTA"}

            };
            escolas.ForEach(ee => context.Escola.AddOrUpdate(e => e.NomeEscola, ee));
            context.SaveChanges();

            //########################################################
            //adiciona Cursos
            var cursos = new List<Cursos>
            {

                new Cursos {CursoID=1, NomeCurso="Engenharia Inform�tica", SiglaCurso="EI", Descricao="Falta descrever", EscolaFK=1, AreaFK=1,TipoCursoFK=1},
                new Cursos {CursoID=2, NomeCurso="Gest�o Tur�stica e Cultural", SiglaCurso="GTC", Descricao="Falta descrever",EscolaFK=3, AreaFK=2,TipoCursoFK=2},
                new Cursos {CursoID=3, NomeCurso="Design e Tecnologia das Artes Gr�ficas", SiglaCurso="DTAG", Descricao="Falta descrever",EscolaFK=2, AreaFK=4,TipoCursoFK=3},
                new Cursos {CursoID=4, NomeCurso="Engenharia Eletrot. e de Computadores", SiglaCurso="EEC", Descricao="Falta descrever", EscolaFK=3, AreaFK=3,TipoCursoFK=1}

            };
            cursos.ForEach(cc => context.Cursos.AddOrUpdate(c => c.NomeCurso, cc));
            context.SaveChanges();

            

            //########################################################
            //adiciona as outras areas 
            var outrasareas = new List<OutrasAreas> {
                new OutrasAreas  {OutrasID = 1, DescriArea = "Falta descrever", PotencialAlunoFK=1, AreaFK=1},
                new OutrasAreas  {OutrasID = 2, DescriArea = "Falta descrever", PotencialAlunoFK=3, AreaFK=2},
                new OutrasAreas  {OutrasID = 3, DescriArea = "Falta descrever", PotencialAlunoFK=2, AreaFK=3},
                new OutrasAreas  {OutrasID = 4, DescriArea = "Falta descrever", PotencialAlunoFK=1, AreaFK=4}
            };

            outrasareas.ForEach(oa => context.OutrasAreas.Add(oa));
            context.SaveChanges();

            //########################################################
            //adiciona as outras areas 
            var outroscursos = new List<OutrosCursos> {
                new OutrosCursos  {OutrosID = 1, OutrasFormacoes = "Falta descrever", PotencialAlunoFK=1, TipoCursoFK=1},
                new OutrosCursos  {OutrosID = 2, OutrasFormacoes = "Falta descrever", PotencialAlunoFK=3, TipoCursoFK=2},
                new OutrosCursos  {OutrosID = 3, OutrasFormacoes = "Falta descrever", PotencialAlunoFK=2, TipoCursoFK=3},
                new OutrosCursos  {OutrosID = 4, OutrasFormacoes = "Falta descrever", PotencialAlunoFK=4, TipoCursoFK=4}
            };
            outroscursos.ForEach(oc => context.OutrosCursos.Add(oc));
            context.SaveChanges();

            ////########################################################
            ////adiciona os Potenciais Alunos
            var potencialaluno = new List<PotencialAluno>
            {
               //new PotencialAluno {AlunoID=1, NomeCompleto="Arru� Valentim Afonso",Email="arrua.afonso@gmail.com",Concelho="Tomar",DataNascimento = new DateTime(1999,02,04),Contacto="967325844",Genero="Masculino", DataInscricao = new DateTime(2017,02,04),HabAcademicas="Licenciatura", AreasFK =  1,  CursosFK = 1, TiposCursosFK= 1},
                //new PotencialAluno {AlunoID=2, NomeCompleto="Jo�o Gomes Cravid",Email="jgomesc@gmail.com",Concelho="Tomar",DataNascimento = new DateTime(2000,02,04),Contacto="910202099",Genero="Masculino", DataInscricao = new DateTime(2017,02,04),HabAcademicas="Licenciatura",  AreasFK =  4, CursosFK = 2, TiposCursosFK= 3},
               // new PotencialAluno {AlunoID=3, NomeCompleto="Paulo Duque J�nior",Email="pauloj@gmail.com",Concelho="Tomar",DataNascimento = new DateTime(2001,02,04),Contacto="967386733",Genero="Masculino", DataInscricao = new DateTime(2017,02,04),HabAcademicas="Licenciatura",  AreasFK =  2, CursosFK = 3, TiposCursosFK= 2},
                //new PotencialAluno {AlunoID=5, NomeCompleto="Ana Maria Concei��o Lima",Email="a.lima@gmail.com",Concelho="Tomar",DataNascimento = new DateTime(1988,02,04),Contacto="917834672",Genero="Feminino", DataInscricao = new DateTime(2017,02,04),HabAcademicas="TeSPs",  AreasFK =  3, CursosFK = 4, TiposCursosFK= 4}
            };
            potencialaluno.ForEach(pa => context.PotencialAluno.AddOrUpdate(p => p.NomeCompleto, pa));
            context.SaveChanges();

            var colaboradores = new List<Colaboradores>
            {
                //new Colaboradores {ColaboradorID=1, NomeProprio="Joana",Apelido="Pinto",NIF="245689402",Localidade="Maria Grande",Contacto="967325844",UserID=""},
                //new PotencialAluno {AlunoID=2, NomeCompleto="Jo�o Gomes Cravid",Email="jgomesc@gmail.com",Concelho="Tomar",DataNascimento = new DateTime(2000,02,04),Contacto="910202099",Genero="Masculino", DataInscricao = new DateTime(2017,02,04),HabAcademicas="Licenciatura",  AreasFK =  4, CursosFK = 2, TiposCursosFK= 3},
                // new PotencialAluno {AlunoID=3, NomeCompleto="Paulo Duque J�nior",Email="pauloj@gmail.com",Concelho="Tomar",DataNascimento = new DateTime(2001,02,04),Contacto="967386733",Genero="Masculino", DataInscricao = new DateTime(2017,02,04),HabAcademicas="Licenciatura",  AreasFK =  2, CursosFK = 3, TiposCursosFK= 2},
                //new PotencialAluno {AlunoID=5, NomeCompleto="Ana Maria Concei��o Lima",Email="a.lima@gmail.com",Concelho="Tomar",DataNascimento = new DateTime(1988,02,04),Contacto="917834672",Genero="Feminino", DataInscricao = new DateTime(2017,02,04),HabAcademicas="TeSPs",  AreasFK =  3, CursosFK = 4, TiposCursosFK= 4}
            };
            colaboradores.ForEach(cc => context.Colaboradores.AddOrUpdate(c => c.NomeProprio, cc));
            context.SaveChanges();







        }
    }
}
