using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Nest;
using WorkFlowTaskSystem.Application.EmailJobs.Dto;
using WorkFlowTaskSystem.Application.ES.Dto;
using Abp.ElasticSearch;

namespace WorkFlowTaskSystem.Application.ES
{
   public class EsService:ApplicationService
    {
        private readonly IAbpElasticClient _abpElasticClient;

        public EsService(IAbpElasticClient abpElasticClient)
        {
            _abpElasticClient = abpElasticClient;
        }

        public async Task<IndexResponse> IndexDocumentAsync()
        {
            // indexing
            var line = new TestModel()
            {
                Id = 10,
                Name = $"测试线路 ",
                Dic = $"大鱼海棠 ",
                State =11,
                AddTime = DateTime.Now,
                PassingRate = 10,
                Dvalue = 20
            };
            var result = await _abpElasticClient.IndexDocumentAsync(line);
            return result;

        }
       
        public List<TestModel> GetElasticSearch(string name)
        {
            try
            {
                // Searching
                var searchResponse = _abpElasticClient.Search<TestModel>(s => s
                    .From(0)
                    .Size(10)
                    .Query(q =>  q
                                    .Match(m => m.Field(f => f.Name).Query(name))
                    )
                );

                return searchResponse.Documents.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
        public List<FinOrderBookExpire> GetElasticSearchTest()
        {
            try
            {
                // Searching
                var searchResponse = _abpElasticClient.Search<FinOrderBookExpire>(s => s
                    .From(1)
                    .Size(100000)

                );

                return searchResponse.Documents.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public ISearchResponse<FinOrderBookExpire> GetElasticSearchGroup()
        {
            try
            {
                // Searching
                var searchResponse = _abpElasticClient.Search<FinOrderBookExpire>(s => s
                    .Size(0)
                    .Aggregations(u=>
                        u.Terms("ParkId",
                        t=>t.Field(f=>f.ParkId)
                            .Aggregations(ag=>ag.Terms("AgencyId", t2=>t2.Field(fd=>fd.AgencyId).
                                Aggregations(agg=> agg.Sum("SettlementAmount", m => m.Field(f => f.SettlementAmount)))
                                ))
                            
                            ))

                );

                return searchResponse;
                }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public async Task<IndexResponse> IndexFinOrderBookExpireAsync()
        {
            // indexing
            var line = new FinOrderBookExpire()
            {
                ToHeaderId="1234567"
            };
            var result = await _abpElasticClient.IndexDocumentAsync(line);
            return result;

        }
    }
}
