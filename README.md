# 建立架構，讓 ASP.NET Core Minimal API 更有條理好維護

### 組織 ASP.NET Core Minimal APIs 的幾個小技巧
1. 使用擴充方法來組織端點
2. 使用 TypedResults 替代 Results 來簡化程式碼
3. 將功能與端點註冊分離，讓程式碼更乾淨，也更容易測試
4. 將端點分組以避免重複，打造更簡潔的架構

### 實作
1. 端點
```C#
    //使用擴充方法來組織端點
    app.SampleEndPoints();
```
2. 擴充方法
```C#
    public static void SampleEndPoints(this WebApplication app)
    {
        //將端點分組以避免重複，打造更簡潔的架構
        var sampleGroup = app.MapGroup("/Sample");

        sampleGroup.MapGet("/", () => "Hello World!");

        //將功能與端點註冊分離，讓程式碼更乾淨，也更容易測試
        sampleGroup.MapGet("/{str}", ReturnStr_Get);
        sampleGroup.MapPost("/", ReturnStr_Post);
        sampleGroup.MapGet("/GetSqlVersion", GetSqlVersion);
    }
```
3. 功能
```C#
    //Get Resuest
    private static Ok<string> ReturnStr_Get(IConfiguration configuration,string str)
    {
        str += "(from sample)";

        //使用 TypedResults 替代 Results 來簡化程式碼
        return TypedResults.Ok(str);
    }

    //Post Resuest
    private static Ok<PostParams> ReturnStr_Post(IConfiguration configuration, PostParams postParams)
    {
        postParams.Key1 += "(from sample)";
        postParams.Key2 += "(from sample)";

        //使用 TypedResults 替代 Results 來簡化程式碼
        return TypedResults.Ok(postParams);
    }

    private static Ok<string> GetSqlVersion(IConfiguration configuration)
    {
        var repository = new SampleRepository(configuration);
        var version= repository.GetSqlVersion();

        //使用 TypedResults 替代 Results 來簡化程式碼
        return TypedResults.Ok(version);
    }
```
4. 資料庫存取 interface
```C#
    public interface IRepository
    {
        string ConnectionString { get; }

        IDbConnection Connection { get; }
    }

    //都要使用 interface
    public class SampleRepository(IConfiguration configuration) : IRepository
    {
        //透過Configuration取得連線字串
        public string ConnectionString => configuration["DBConnection:Sample"];

        //透過ConnectionString取得連線
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }
    }
```

### 命名規則
1. EndPoints
> {XXX}Api.cs  
> public static void {XXX}EndPoints(this WebApplication app)
2. Models
> {XXX}Model.cs
3. DataAccesses
> {XXX}Repository.cs  
> public class {XXX}Repository(IConfiguration configuration) : IRepository
> {XXX}Dto.cs  

### 備註
1. 先修改 appsettings.Development.json 中的連線字串
```json
    {
        "DBConnection":{
            "Sample": "Server=  ;Database=  ;User Id=  ;Password=  ;"
        }
    }
```
2. 對環境變數建立相對應的 appsettings，例如 appsettings.Staging.json、appsettings.Production.json
3. 使用 Visual Studio Code 的話，可使用 ApiTest.http 進行測試，需先安裝 [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)

### 參考
* https://blog.darkthread.net/blog/organizing-min-api/
* https://www.tessferrandez.com/blog/2023/10/31/organizing-minimal-apis.html