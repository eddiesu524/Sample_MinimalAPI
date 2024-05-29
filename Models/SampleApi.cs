using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Sample_MinimalAPI.DataAccesses;
using static Sample_MinimalAPI.Models.SampleModel;

namespace Sample_MinimalAPI.Models
{
    public static class SampleApiExtensions
    {
        public static void SampleEndPoints(this WebApplication app)
        {
            //TODO 將端點分組以避免重複，打造更簡潔的架構
            var sampleGroup = app.MapGroup("/Sample");

            sampleGroup.MapGet("/", () => "Hello Sample!");

            //TODO 將功能與端點註冊分離，讓程式碼更乾淨，也更容易測試
            sampleGroup.MapGet("/{str}", ReturnStr_Get);
            sampleGroup.MapPost("/", ReturnStr_Post);
            sampleGroup.MapGet("/GetSqlVersion", GetSqlVersion);
        }

        private static Ok<string> ReturnStr_Get(IConfiguration configuration,string str)
        {
            str += "(from sample)";

            //TODO 使用 TypedResults 替代 Results 來簡化程式碼
            return TypedResults.Ok(str);
        }

        private static Ok<PostParams> ReturnStr_Post(IConfiguration configuration, PostParams postParams)
        {
            postParams.Key1 += "(from sample)";
            postParams.Key2 += "(from sample)";

            //TODO 使用 TypedResults 替代 Results 來簡化程式碼
            return TypedResults.Ok(postParams);
        }

        private static Ok<string> GetSqlVersion(IConfiguration configuration)
        {
            var repository = new SampleRepository(configuration);
            var version= repository.GetSqlVersion();

            //TODO 使用 TypedResults 替代 Results 來簡化程式碼
            return TypedResults.Ok(version);
        }
    }
}