using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APi_MongoDB.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APi_MongoDB.Model;

namespace APi_MongoDB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InfectadoController : ControllerBase
    {
        Data.MongoDB _mongoDB;

        IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
            
        }

        [HttpPost]

        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado(dto.DataNascimento, dto.Sexo, dto.Longitude, dto.Latitude);
            _infectadosCollection.InsertOne(infectado);

            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]

        public ActionResult Obterinfectados()
        {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            return Ok(infectados);
        }

    }
}
