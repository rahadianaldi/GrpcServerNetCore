using GrpcGreeterServer.Class;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace GrpcGreeterServer.Models
{
    public class HomeContext
    {
        string strconn = "server=localhost;port=3306;user=root;password=;database=db_kemahasiswaan";

        public ListMahasiswa GetListMahasiswa()
        {
            using var connection = new MySqlConnection(strconn);
            ListMahasiswa Res = new ListMahasiswa();
            List<Mahasiswa> list = new List<Mahasiswa>();
            try
            {
                connection.Open();
                string strquery = "SELECT * FROM mahasiswa";
                var commannd = new MySqlCommand(strquery, connection);
                var reader = commannd.ExecuteReader();

                while (reader.Read())
                {
                    Mahasiswa mhs = new Mahasiswa();
                    mhs.nama = Convert.ToString(reader["Nama"]);
                    mhs.nim = Convert.ToString(reader["NIM"]);
                    mhs.asal = Convert.ToString(reader["Asal"]);
                    mhs.datebirth = Convert.ToString(reader["Tanggal Lahir"]);

                    list.Add(mhs);
                }
                Res.ErrorCode = "0";
                Res.ErrorDesc = "Succes";
                Res.mahasiswa = list;
            }
            catch (Exception e)
            {
                Res.ErrorCode = "1";
                Res.ErrorDesc = e.Message.ToString();
                throw;
            }
            return Res;
        }

        public GrpcGreeter.Mahasiswa DetailMahasiswa(string nim)
        {
            GrpcGreeter.Mahasiswa Res = new GrpcGreeter.Mahasiswa();
            using var connection = new MySqlConnection(strconn);
            try
            {
                connection.Open();
                string strquery = "SELECT * FROM mahasiswa WHERE NIM = '"+ nim +"'";
                var commannd = new MySqlCommand(strquery, connection);
                var reader = commannd.ExecuteReader();

                while (reader.Read())
                {
                    GrpcGreeter.Mahasiswa mhs = new GrpcGreeter.Mahasiswa();
                    mhs.Nama = Convert.ToString(reader["Nama"]);
                    mhs.Nim = Convert.ToString(reader["NIM"]);
                    mhs.Asal = Convert.ToString(reader["Asal"]);
                    mhs.Datebirth = Convert.ToString(reader["Tanggal Lahir"]);

                    Res = mhs;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }

        public string InsertMahasiswa(GrpcGreeter.Mahasiswa mhs)
        {
            string Res = "1~General Error";
            using var connection = new MySqlConnection(strconn);
            try
            {
                connection.Open();
                string strquery = "INSERT INTO mahasiswa VALUES" +
                    "('"+ mhs.Nama +"', '"+ mhs.Nim +"', '"+ mhs.Asal +"', '"+ mhs.Datebirth +"')";

                var commannd = new MySqlCommand(strquery, connection);
                var reader = commannd.ExecuteReader();
                Res = "0~Insert Berhasil";
            }
            catch (Exception e)
            {
                Res = "1~" + e.Message.ToString();
                throw;
            }
            return Res;
        }

        public string EditMahasiswa(GrpcGreeter.Mahasiswa mhs)
        {
            string Res = "1~General Error";
            using var connection = new MySqlConnection(strconn);
            try
            {
                connection.Open();
                string strquery = "UPDATE mahasiswa SET " +
                    "Nama = '"+ mhs.Nama +"'" +
                    ", Asal = '"+ mhs.Asal +"'" +
                    ", `Tanggal Lahir` = '"+ mhs.Datebirth +"' " +
                    "WHERE NIM = '"+ mhs.Nim +"'";
                var commannd = new MySqlCommand(strquery, connection);
                var reader = commannd.ExecuteReader();
                Res = "0~Edit Berhasil";
            }
            catch (Exception e)
            {
                Res = "1~" + e.Message.ToString();
                throw;
            }
            return Res;
        }

        public string DeleteMahasiswa(string nim)
        {
            string Res = "1~General Error";
            using var connection = new MySqlConnection(strconn);
            try
            {
                connection.Open();
                string strquery = "DELETE FROM mahasiswa WHERE NIM = '"+ nim +"'";
                var commannd = new MySqlCommand(strquery, connection);
                var reader = commannd.ExecuteReader();
                Res = "0~Delete Berhasil";
            }
            catch (Exception e)
            {
                Res = "1~" + e.Message.ToString();
                throw;
            }
            return Res;
        }
    }
}
