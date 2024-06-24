using Microsoft.AspNetCore.Http.HttpResults;
using static Sample_MinimalAPI.Models.TemplateModel;

namespace Sample_MinimalAPI.Models
{
    public static class TemplateApi
    {
        public static void TemplateEndPoints(this WebApplication app)
        {
            //TODO 將端點分組以避免重複，打造更簡潔的架構
            var templateGroup = app.MapGroup("/Template");

            //TODO 將功能與端點註冊分離，讓程式碼更乾淨，也更容易測試
            templateGroup.MapGet("/", ReturnStr_Get);
            templateGroup.MapGet("/{str}", ReturnStr_Get2);
            templateGroup.MapPost("/", ReturnStr_Post);

        }

        private static Ok<string> ReturnStr_Get(IConfiguration configuration)
        {
            return TypedResults.Ok("");
        }

        private static Ok<string> ReturnStr_Get2(IConfiguration configuration, string str)
        {
            //TODO 使用 TypedResults 替代 Results 來簡化程式碼
            return TypedResults.Ok(str);
        }

        private static Ok<PostParams> ReturnStr_Post(IConfiguration configuration, PostParams postParams)
        {
            //TODO 使用 TypedResults 替代 Results 來簡化程式碼
            return TypedResults.Ok(postParams);
        }
    }
}