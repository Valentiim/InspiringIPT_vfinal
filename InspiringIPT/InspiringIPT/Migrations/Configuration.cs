namespace InspiringIPT.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<InspiringIPT.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;//permite a actualização automática da BD
        }



        protected override void Seed(InspiringIPT.Models.ApplicationDbContext context)
        {

            //######################################################## IMAGENS
            //adiciona as Imagens
            var imagens = new List<Imagens>
            {
                 //*******************************************************ALIMENTAÇÃO****************************
                new Imagens {ImagensID = 1, Nome = "alimentacao1.jpg", Categoria = "alimentacao", Data = DateTime.Now},
                new Imagens {ImagensID = 2, Nome = "alimentacao3.jpg", Categoria = "alimentacao", Data = DateTime.Now},
                new Imagens {ImagensID = 3, Nome = "alimentacao4.jpg", Categoria = "alimentacao", Data = DateTime.Now},

                //*******************************************************ALOJAMENTO****************************
                new Imagens {ImagensID = 4, Nome = "alojamento1.jpg", Categoria = "alojamento", Data = DateTime.Now},
                new Imagens {ImagensID = 5, Nome = "alojamento2.jpg", Categoria = "alojamento", Data = DateTime.Now},
                new Imagens {ImagensID = 6, Nome = "alojamento3.jpg", Categoria = "alojamento", Data = DateTime.Now},
                new Imagens {ImagensID = 7, Nome = "alojamento4.jpg", Categoria = "alojamento", Data = DateTime.Now},

                //*******************************************************ASSOCIATIVISMO****************************
                new Imagens {ImagensID = 8, Nome = "Associativismo2.jpg", Categoria = "associativismo", Data = DateTime.Now},
                new Imagens {ImagensID = 9, Nome = "Associativismo3.jpg", Categoria = "associativismo", Data = DateTime.Now},
                new Imagens {ImagensID = 10, Nome = "Associativismo4.jpg", Categoria = "associativismo", Data = DateTime.Now},
                
                //*******************************************************BANNER*****************************
                new Imagens {ImagensID = 11, Nome = "banner_1.jpg", Categoria = "banner", Data = DateTime.Now},
                new Imagens {ImagensID = 12, Nome = "banner_4.jpg", Categoria = "banner", Data = DateTime.Now},
                new Imagens {ImagensID = 13, Nome = "banner_5.jpg", Categoria = "banner", Data = DateTime.Now},
                new Imagens {ImagensID = 14, Nome = "banner_6.jpg", Categoria = "banner", Data = DateTime.Now},
                new Imagens {ImagensID = 15, Nome = "banner_7_.jpg", Categoria = "banner", Data = DateTime.Now},

                //*******************************************************CULTURA****************************
                new Imagens {ImagensID = 16, Nome = "cultura1.jpg", Categoria = "cultura", Data = DateTime.Now},
                new Imagens {ImagensID = 17, Nome = "cultura2.jpg", Categoria = "cultura", Data = DateTime.Now},
                new Imagens {ImagensID = 18, Nome = "cultura4.jpg", Categoria = "cultura", Data = DateTime.Now},
                new Imagens {ImagensID = 19, Nome = "cultura5.jpg", Categoria = "cultura", Data = DateTime.Now},

                //*******************************************************DESPORTO****************************
                new Imagens {ImagensID = 20, Nome = "desporto2.jpg", Categoria = "desporto", Data = DateTime.Now},
                new Imagens {ImagensID = 21, Nome = "desporto3.jpg", Categoria = "desporto", Data = DateTime.Now},
                new Imagens {ImagensID = 22, Nome = "desporto4.jpg", Categoria = "desporto", Data = DateTime.Now},
                new Imagens {ImagensID = 23, Nome = "desportos.jpg", Categoria = "desporto", Data = DateTime.Now},

                 //*******************************************************EQUIPA****************************
                new Imagens {ImagensID = 24, Nome = "img1.jpg", Categoria = "equipa", Data = DateTime.Now},
                new Imagens {ImagensID = 25, Nome = "img2.jpg", Categoria = "equipa", Data = DateTime.Now},
                new Imagens {ImagensID = 26, Nome = "img3.jpg", Categoria = "equipa", Data = DateTime.Now},
                new Imagens {ImagensID = 27, Nome = "img4.jpg", Categoria = "equipa", Data = DateTime.Now},
                new Imagens {ImagensID = 28, Nome = "img5.jpg", Categoria = "equipa", Data = DateTime.Now},
                new Imagens {ImagensID = 29, Nome = "img6.jpg", Categoria = "equipa", Data = DateTime.Now},
                new Imagens {ImagensID = 30, Nome = "img7.jpg", Categoria = "equipa", Data = DateTime.Now},
                new Imagens {ImagensID = 31, Nome = "img8.jpg", Categoria = "equipa", Data = DateTime.Now},
                new Imagens {ImagensID = 32, Nome = "img9.jpg", Categoria = "equipa", Data = DateTime.Now},


                //*******************************************************ESTUDAR****************************
                new Imagens {ImagensID = 33, Nome = "estudar_3.jpg", Categoria = "estudar", Data = DateTime.Now},
                new Imagens {ImagensID = 34, Nome = "estudar_4.jpg", Categoria = "estudar", Data = DateTime.Now},
                new Imagens {ImagensID = 35, Nome = "estudar1.jpg", Categoria = "estudar", Data = DateTime.Now},
                new Imagens {ImagensID = 36, Nome = "estudar2.jpg", Categoria = "estudar", Data = DateTime.Now}

            };
            imagens.ForEach(arar => context.Imagens.AddOrUpdate(ar => ar.Nome, arar));
            context.SaveChanges();


            //######################################################## ÁREAS
            //adiciona as Áreas
            var areas = new List<Areas> {
                new Areas  {AreaID = 1, NomeArea = "Engenharia e Tecnologia"},
                new Areas  {AreaID = 2, NomeArea = "Gestão"},
                new Areas  {AreaID = 3, NomeArea = "Design"},
                new Areas  {AreaID = 4, NomeArea = "Turismo"}

            };
            areas.ForEach(arar => context.Areas.AddOrUpdate(ar => ar.NomeArea, arar));
            context.SaveChanges();


            //######################################################## TIPO CURSO
            //adiciona os Tipo do Curso
            var tiposcursos = new List<TipoCurso> {
                new TipoCurso  {TipoID = 1, Tipo = "Licenciatura"},
                new TipoCurso  {TipoID = 2, Tipo = "Mestrado"},
                new TipoCurso  {TipoID = 3, Tipo = "TeSPs"},
                new TipoCurso  {TipoID = 4, Tipo = "Doutoramento"},
                new TipoCurso  {TipoID = 5, Tipo = "12º ano"},
                new TipoCurso  {TipoID = 6, Tipo = "Ainda não tenho o 12º ano"},
                new TipoCurso  {TipoID = 7, Tipo = "Grau Académico Superior Estrangeiro"}

            };
            tiposcursos.ForEach(tt => context.TipoCurso.AddOrUpdate(t => t.Tipo, tt));
            context.SaveChanges();


            //######################################################## ESCOLA
            //adiciona as Escolas
            var escolas = new List<Escola> {
                new Escola  {EscolaID = 1, NomeEscola = "Escola Superior de Tecnologia de Tomar", SiglaEscola="ESTT"},
                new Escola  {EscolaID = 2, NomeEscola = "Escola Superior de Gestão de Tomar", SiglaEscola="ESGT"},
                new Escola  {EscolaID = 3, NomeEscola = "Escola Superior de Tecnologia de Abrantes", SiglaEscola="ESTA"}

            };
            escolas.ForEach(ee => context.Escola.AddOrUpdate(e => e.NomeEscola, ee));
            context.SaveChanges();


            //############################################################### CURSOS
            //adiciona Cursos 
            var cursos = new List<Cursos>
            {

                new Cursos {CursoID=1, NomeCurso="Engenharia Informática", SiglaCurso="EI", Descricao="a descrever...", EscolaFK=1, AreaFK=1,TipoCursoFK=1},
                new Cursos {CursoID=2, NomeCurso="Gestão Turística e Cultural", SiglaCurso="GTC", Descricao="a descrever...",EscolaFK=2, AreaFK=2,TipoCursoFK=1},
                new Cursos {CursoID=3, NomeCurso="Design e Tecnologia das Artes Gráficas", SiglaCurso="DTAG", Descricao="a descrever...",EscolaFK=1, AreaFK=1,TipoCursoFK=1},
                new Cursos {CursoID=4, NomeCurso="Engenharia Eletrotécnica e de Computadores", SiglaCurso="EEC", Descricao="a descrever...", EscolaFK=1, AreaFK=1,TipoCursoFK=1}

            };
            cursos.ForEach(cc => context.Cursos.AddOrUpdate(c => c.CursoID, cc));
            context.SaveChanges();

            ////############################################################# POTENCIAL ALUNO
            ////adiciona os Potenciais Alunos 
            var potencialaluno = new List<PotencialAluno>
            {
                new PotencialAluno {AlunoID=1, NomeCompleto="Joaninha Pedro Afonso",Email="j.pedro@ipt.pt",Concelho="Tomar",DataNascimento = new DateTime(1999,02,04),
                Contacto ="967325844",Genero="M", HabAcademicas="Licenciatura", DataInscricao = new DateTime(2017,02,04), CodigoIdentificacao="de56jGFDR98765" ,ListaCursos= new List<Cursos> {cursos[0], cursos[1]},
                ListaTipoCurso = new List<TipoCurso> {tiposcursos[0], tiposcursos[1]}, ListaAreas= new List<Areas> {areas[0], areas[1]}},

                new PotencialAluno {AlunoID=2, NomeCompleto="João Gomes Cravid",Email="jgomesc@ipt.pt",Concelho="Abrantes",DataNascimento = new DateTime(2000,02,04),
                Contacto ="910202099",Genero="M", HabAcademicas="Licenciatura", DataInscricao = new DateTime(2017,02,04),CodigoIdentificacao="asdeFWT-fer564646" ,ListaCursos= new List<Cursos> {cursos[2], cursos[3]},
                ListaTipoCurso = new List<TipoCurso> {tiposcursos[2], tiposcursos[3]}, ListaAreas= new List<Areas> {areas[2], areas[3]}},

                new PotencialAluno {AlunoID=3, NomeCompleto="Paulo Duque Júnior",Email="pauloj@ipt.pt",Concelho="Torres Novas",DataNascimento = new DateTime(2001,02,04),
                Contacto ="967386733",Genero="M", HabAcademicas="Licenciatura", DataInscricao = new DateTime(2017,02,04),CodigoIdentificacao="wrygjhhgYUFTTDYKLKJl8989" ,ListaCursos= new List<Cursos> {cursos[0], cursos[2]},
                ListaTipoCurso = new List<TipoCurso> {tiposcursos[2], tiposcursos[1]}, ListaAreas= new List<Areas> {areas[3], areas[0]}},

                new PotencialAluno {AlunoID=4, NomeCompleto="Ana Maria Conceição Lima",Email="a.lima@ipt.pt",Concelho="Leiria",DataNascimento = new DateTime(1988,02,04),
                Contacto ="917834672",Genero="F", HabAcademicas="TeSPs", DataInscricao = new DateTime(2017,02,04),CodigoIdentificacao="nmaslkjfdGTFDCA76884jlkdj" , ListaCursos= new List<Cursos> {cursos[1], cursos[3]},
                 ListaTipoCurso = new List<TipoCurso> {tiposcursos[0], tiposcursos[2]}, ListaAreas= new List<Areas> {areas[3], areas[2]}}
            };
            potencialaluno.ForEach(pa => context.PotencialAluno.AddOrUpdate(p => p.AlunoID, pa));
            context.SaveChanges();
           

        }
    }
}
