using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarteiraInvestimentos.Entities;
using CarteiraInvestimentos.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarteiraInvestimentos.Repositories
{
  public class MongoDbAcaoRepository : AcoesRepositoryInterface
  {
    private const string databaseName = "admin";

    private const string collectionName = "acoes";

    private readonly IMongoCollection<Acao> acoesCollection;

    private readonly FilterDefinitionBuilder<Acao> filterBuilder = Builders<Acao>.Filter;

    public MongoDbAcaoRepository(IMongoClient mongoClient) 
    {
      IMongoDatabase database = mongoClient.GetDatabase(databaseName);
      acoesCollection = database.GetCollection<Acao>(collectionName);
    }

    public async Task<IEnumerable<Acao>> GetAcoesAsync()
    {
       return await acoesCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Acao> GetAcaoAsync(Guid id)
    {
      var filter = filterBuilder.Eq(acao => acao.Id, id);
      return await acoesCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<Acao> GetAcaoByCodigoAsync(string codigo)
    {
      var filter = filterBuilder.Eq(acao => acao.Codigo, codigo);
      return await acoesCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task CreateAcaoAsync(Acao acao)
    {
      await acoesCollection.InsertOneAsync(acao);
    }

    public async Task UpdateAcaoAsync(Acao acao)
    {
      var filter = filterBuilder.Eq(existingAcao => existingAcao.Id, acao.Id);
      await acoesCollection.ReplaceOneAsync(filter, acao);
    }

    public async Task DeleteAcaoAsync(Guid id)
    {
      var filter = filterBuilder.Eq(acao => acao.Id, id);
      await acoesCollection.DeleteOneAsync(filter);
    }
  }
}