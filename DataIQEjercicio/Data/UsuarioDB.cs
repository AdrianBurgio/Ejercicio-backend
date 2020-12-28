using DataIQEjercicio.Configuracion;
using DataIQEjercicio.Modelo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 5 - conecta el mongo con .net core
namespace DataIQEjercicio.Data
{
    public class UsuarioDB
    {
        private readonly IMongoCollection<Usuario> _usuariosCollection;

        public UsuarioDB(IUsuariosDatabaseStore settings)
        {
            var mdbClient = new MongoClient(settings.ConnectionString);
            var database = mdbClient.GetDatabase(settings.DatabaseName);

            _usuariosCollection = database.GetCollection<Usuario>(settings.UsuariosCollectionName);
        }

        public async Task<List<Usuario>> GetAsync()
        {
            return await _usuariosCollection.Find(usu => true).ToListAsync();
        }

        //public List<Usuario> Get()
        //{
        //    return _usuariosCollection.Find(usu => true).ToList();
        //}

        public async Task<Usuario> GetByIdAsync(string id)
        {
            // para que se pueda compar el id con == hay que hacer el arreglo en la clase usuario [BsonRepresentation(BsonType.ObjectId)]
            return await _usuariosCollection.Find<Usuario>(usuario => usuario.Id == id).FirstOrDefaultAsync();
        }

        public Usuario Create(Usuario usu)
        {
            _usuariosCollection.InsertOne(usu);
            return usu;
        }

        public void Update(string id, Usuario usu)
        {
            _usuariosCollection.ReplaceOne(usuario => usuario.Id == id, usu);
        }

        public void Delete(Usuario usu)
        {
            _usuariosCollection.DeleteOne(usuario => usuario.Id == usu.Id);
        }

        public void DeleteById(string id)
        {
            _usuariosCollection.DeleteOne(usuario => usuario.Id == id);
        }






    }
}
