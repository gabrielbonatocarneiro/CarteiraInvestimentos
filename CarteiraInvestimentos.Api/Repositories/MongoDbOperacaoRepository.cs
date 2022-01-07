using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarteiraInvestimentos.Api.Entities;
using CarteiraInvestimentos.Api.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarteiraInvestimentos.Api.Repositories
{
  public class MongoDbOperacaoRepository : OperacoesRepositoryInterface
  {
    private const string databaseName = "carteirainvestimentos";

    private const string collectionName = "operacoes";

    private readonly IMongoCollection<Operacao> operacoesCollection;

    private readonly FilterDefinitionBuilder<Operacao> filterBuilder = Builders<Operacao>.Filter;

    public MongoDbOperacaoRepository(IMongoClient mongoClient) 
    {
      IMongoDatabase database = mongoClient.GetDatabase(databaseName);
      operacoesCollection = database.GetCollection<Operacao>(collectionName);
    }

    public async Task<IEnumerable<Operacao>> GetOperacaoesAsync(string? codigoAcao = null)
    {
      if (codigoAcao is null) {
        return await operacoesCollection.Find(new BsonDocument()).ToListAsync();
      }

      var filter = filterBuilder.Eq(operacao => operacao.CodigoAcao, codigoAcao);
      return await operacoesCollection.Find(filter).ToListAsync();
    }

    public async Task<Operacao> GetOperacaoAsync(Guid id)
    {
      var filter = filterBuilder.Eq(operacao => operacao.Id, id);
      return await operacoesCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task CreateOperacaoAsync(Operacao operacao)
    {
      await operacoesCollection.InsertOneAsync(operacao);
    }
  }
}