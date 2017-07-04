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
            AutomaticMigrationsEnabled = true;//permite a actualização automática da BD
        }

        protected override void Seed(InspiringIPT.Models.ApplicationDbContext context)
        {
            //########################################################
            //adiciona as Áreas
            var areas = new List<Areas> {
                new Areas  {AreaID = 1, NomeArea = "Engenharia e Tecnologia"},
                new Areas  {AreaID = 2, NomeArea = "Gestão"},
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
                new Escola  {EscolaID = 2, NomeEscola = "Escola Superior de Gestão de Tomar", SiglaEscola="ESGT"},
                new Escola  {EscolaID = 3, NomeEscola = "Escola Superior de Tecnologia de Abrantes", SiglaEscola="ESTA"}

            };
            escolas.ForEach(ee => context.Escola.AddOrUpdate(e => e.NomeEscola, ee));
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

            //###############################################################3
            //adiciona Cursos A (M)
            var cursos = new List<Cursos>
            {

                new Cursos {CursoID=1, NomeCurso="Engenharia Informática", SiglaCurso="EI", Descricao="Falta descrever", EscolaFK=1, AreaFK=1,TipoCursoFK=1},
                new Cursos {CursoID=2, NomeCurso="Gestão Turística e Cultural", SiglaCurso="GTC", Descricao="Falta descrever",EscolaFK=3, AreaFK=2,TipoCursoFK=2},
                new Cursos {CursoID=3, NomeCurso="Design e Tecnologia das Artes Gráficas", SiglaCurso="DTAG", Descricao="Falta descrever",EscolaFK=2, AreaFK=4,TipoCursoFK=3},
                new Cursos {CursoID=4, NomeCurso="Engenharia Eletrot. e de Computadores", SiglaCurso="EEC", Descricao="Falta descrever", EscolaFK=3, AreaFK=3,TipoCursoFK=1}

            };
            cursos.ForEach(cc => context.Cursos.AddOrUpdate(c => c.CursoID, cc));
            context.SaveChanges();

            ////########################################################
            ////adiciona os Potenciais Alunos B (N)
            var potencialaluno = new List<PotencialAluno>
            {
                new PotencialAluno {AlunoID=1, NomeCompleto="Arruá Valentim Afonso",Email="arrua.afonso@ipt.pt",Concelho="Tomar",DataNascimento = new DateTime(1999,02,04),Contacto="967325844",Genero="M", HabAcademicas="Licenciatura", DataInscricao = new DateTime(2017,02,04), ListaCursos= new List<Cursos> {cursos[0], cursos[1]}, ListaTipoCurso= new List<TipoCurso> {tiposcursos[0], tiposcursos[1]}, ListaAreas= new List<Areas> {areas[0], areas[1]}},
                new PotencialAluno {AlunoID=2, NomeCompleto="João Gomes Cravid",Email="jgomesc@ipt.pt",Concelho="Tomar",DataNascimento = new DateTime(2000,02,04),Contacto="910202099",Genero="M", HabAcademicas="Licenciatura", DataInscricao = new DateTime(2017,02,04),ListaCursos= new List<Cursos> {cursos[2], cursos[3]}, ListaTipoCurso= new List<TipoCurso> {tiposcursos[2], tiposcursos[3]}, ListaAreas= new List<Areas> {areas[2], areas[3]}},
                new PotencialAluno {AlunoID=3, NomeCompleto="Paulo Duque Júnior",Email="pauloj@ipt.pt",Concelho="Tomar",DataNascimento = new DateTime(2001,02,04),Contacto="967386733",Genero="M", HabAcademicas="Licenciatura", DataInscricao = new DateTime(2017,02,04),ListaCursos= new List<Cursos> {cursos[4], cursos[5]}, ListaTipoCurso= new List<TipoCurso> {tiposcursos[4], tiposcursos[5]}, ListaAreas= new List<Areas> {areas[4], areas[5]}},
                new PotencialAluno {AlunoID=4, NomeCompleto="Ana Maria Conceição Lima",Email="a.lima@ipt.pt",Concelho="Tomar",DataNascimento = new DateTime(1988,02,04),Contacto="917834672",Genero="F", HabAcademicas="TeSPs", DataInscricao = new DateTime(2017,02,04), ListaCursos= new List<Cursos> {cursos[6], cursos[7]}, ListaTipoCurso= new List<TipoCurso> {tiposcursos[6], tiposcursos[7]}, ListaAreas= new List<Areas> {areas[6], areas[7]}}

            };
            potencialaluno.ForEach(pa => context.PotencialAluno.AddOrUpdate(p => p.AlunoID, pa));
            context.SaveChanges();
           

        }
    }
}
