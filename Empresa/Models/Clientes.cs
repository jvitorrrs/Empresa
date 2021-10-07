using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Clientes.Models
{
    public class Participantes
    {
        //public readonly static string _conn= @"Data Source=DESKTOP-6ESPHK1\SQLEXPRESS;Initial Catalog = Clientes;
        //Integrated Security = True; Connect Timeout = 30; 
        //Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private readonly static string _conn = WebConfigurationManager.ConnectionStrings["_conn"].ConnectionString;

        public int CodPar { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Telefone { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int CodAtv { get; set; }
        public string DescAtv { get; set; }
        public int Vagas { get; set; }
        public decimal PrecoAtv { get; set; }
        public int V1 { get; }
        public string V2 { get; }
        public DateTime DateTime { get; }
        public int V3 { get; }

        public Participantes(int codpar, string nome, DateTime datanascimento, int telefone, decimal preco, string descricao, int codatv, string descatv, int vagas, decimal precoatv)
        {
            CodPar = codpar;
            Nome = nome;
            DataNascimento = datanascimento;
            Telefone = telefone;
            Preco = preco;
            Descricao = descricao;
            CodAtv = codatv;
            DescAtv = descatv;
            Vagas = vagas;
            PrecoAtv = precoatv;
        }

        public Participantes(int v)
        {
        }

        public Participantes(int v1, string v2, DateTime dateTime, int v3)
        {
            V1 = v1;
            V2 = v2;
            DateTime = dateTime;
            V3 = v3;
        }

        public Participantes()
        {
        }

        public static List<Participantes> GetCodPar()
        {
           var listaParticipantes = new List<Participantes>();
            var sql = "SELECT * FROM Participantes WHERE CodPar = (SELECT MAX(CodPar) FROM Participantes);";

            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                               {
                                    listaParticipantes.Add(new Participantes(
                                        Convert.ToInt32(dr["CodPar"]),
                                        dr["Nome"].ToString(),
                                        Convert.ToDateTime(dr["DataNascimento"]),
                                        Convert.ToInt32(dr["Telefone"])));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex){
                Console.WriteLine("Falha:" + ex.Message);
            }
            return listaParticipantes;

        }

        public static List<Participantes> GetVagas()
        {
            var listaParticipantes = new List<Participantes>();
            var sql = "SELECT * FROM Participantes WHERE Vagas = (SELECT MIN(Vagas) FROM Participantes);";

            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    listaParticipantes.Add(new Participantes(
                                        Convert.ToInt32(dr["Vagas"])));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha:" + ex.Message);
            }
            return listaParticipantes;
        }

        public void Salvar()
        {
            var sql = "INSERT INTO Participantes (codpar, nome, datanascimento, telefone) VALUES (@codpar, @nome, @datanascimento, @telefone)";

            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@codpar", CodPar);
                        cmd.Parameters.AddWithValue("@nome", Nome);
                        cmd.Parameters.AddWithValue("@datanascimento", DataNascimento);
                        cmd.Parameters.AddWithValue("@telefone", Telefone);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha:" + ex.Message);
            }

            var sql2 = "INSERT INTO Pacotes (preco, descricao) VALUES (@preco, @descricao)";

            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql2, cn))
                    {
                        cmd.Parameters.AddWithValue("@preco", Preco);
                        cmd.Parameters.AddWithValue("@descricao", Descricao);
                        
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha:" + ex.Message);
            }

            var sql3 = "INSERT INTO Atividades (codatv, descatv, vagas, precoatv) VALUES (@codatv, @descatv, @vagas, @precoatv)";

            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql3, cn))
                    {
                        cmd.Parameters.AddWithValue("@codatv", CodAtv);
                        cmd.Parameters.AddWithValue("@descatv", DescAtv);
                        cmd.Parameters.AddWithValue("@vagas", Vagas);
                        cmd.Parameters.AddWithValue("@precoatv", PrecoAtv);


                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha:" + ex.Message);
            }
        }

    public class Pacotes
    {

        private readonly static string _conn = WebConfigurationManager.ConnectionStrings["_conn"].ConnectionString;


        public decimal Preco { get; set; }
        public string Descricao { get; set; }

        public Pacotes(decimal preco, string descricao)
        {
            Preco = preco;
            Descricao = descricao;
        }

            public Pacotes()
            {
            }

            public static List<Pacotes> GetPacotes()
        {
            var listaPacotes = new List<Pacotes>();
            var sql = "SELECT * FROM tb_Clientes";

            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    listaPacotes.Add(new Pacotes(
                                        Convert.ToDecimal(dr["Preco"]),
                                        Convert.ToString(dr["Descricao"])));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha:" + ex.Message);
            }
            return listaPacotes;
        }

            public void Salvar()
            {
                var sql = "INSERT INTO Pacotes (preco, descricao) VALUES (@preco, @descricao)";

                try
                {
                    using (var cn = new SqlConnection(_conn))
                    {
                        cn.Open();
                        using (var cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.AddWithValue("@preco", Preco);
                            cmd.Parameters.AddWithValue("@descricao", Descricao);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Falha:" + ex.Message);
                }
            }
        }

    public class Atividades
    {
        private readonly static string _conn = WebConfigurationManager.ConnectionStrings["_conn"].ConnectionString;


        public int CodAtv { get; set; }
        public string DescAtv { get; set; }
        public int Vagas { get; set; }
        public decimal Preco { get; set; }

        public Atividades(int codatv, string descatv, int vagas, decimal preco)
        {
            CodAtv = codatv;
            DescAtv = descatv;
            Vagas = vagas;
            Preco = preco;
        }

        public static List<Atividades> GetAtividades()
        {
            var listaAtividades = new List<Atividades>();
            var sql = "SELECT * FROM tb_Clientes";

            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    listaAtividades.Add(new Atividades(
                                        Convert.ToInt32(dr["CodAtv"]),
                                        Convert.ToString(dr["DescAtv"]),
                                        Convert.ToInt32(dr["Vagas"]),
                                        Convert.ToDecimal(dr["Preco"])));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha:" + ex.Message);
            }
            return listaAtividades;
        }

        
        }

    }

}