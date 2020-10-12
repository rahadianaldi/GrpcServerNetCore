using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using GrpcGreeterServer.Class;
using GrpcGreeterServer.Models;


namespace GrpcGreeter
{
    public class GreeterService : Greeter.GreeterBase
    {
        HomeContext home = new HomeContext();

        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
        public override Task<ListMhs> GetListMahasiswa(empty request, ServerCallContext context)
        {
            ListMahasiswa Res = new ListMahasiswa();
            List<Mahasiswa> mahasiswa = new List<Mahasiswa>();
            Res = home.GetListMahasiswa();

            foreach (var data in Res.mahasiswa)
            {
                Mahasiswa mhs = new Mahasiswa();
                mhs.Nama = data.nama;
                mhs.Asal = data.asal;
                mhs.Nim = data.nim;
                mhs.Datebirth = data.datebirth;

                mahasiswa.Add(mhs);
            }
            var rslt = new ListMhs
            {
                Mahasiswa = { mahasiswa }
            };
            return Task.FromResult(rslt);
        }

        public override Task<Mahasiswa> DetailMahasiswa(ID request, ServerCallContext context)
        {
            Mahasiswa Res = new Mahasiswa();
            Res = home.DetailMahasiswa(request.Id);

            return Task.FromResult(Res);
        }

        public override Task<Result> InsertMahasiswa(Mahasiswa request, ServerCallContext context)
        {
            string data = home.InsertMahasiswa(request);
            Result Res = new Result();
            Res.Txt = data;

            return Task.FromResult(Res);
        }

        public override Task<Result> EditMahasiswa(Mahasiswa request, ServerCallContext context)
        {
            string data = home.EditMahasiswa(request);
            Result Res = new Result();
            Res.Txt = data;

            return Task.FromResult(Res);
        }

        public override Task<Result> DeleteMahasiswa(ID request, ServerCallContext context)
        {
            string data = home.DeleteMahasiswa(request.Id);
            Result Res = new Result();
            Res.Txt = data;

            return Task.FromResult(Res);
        }
    }
}
