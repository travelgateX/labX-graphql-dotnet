using GraphQL;
using GraphQL.Http;
using GraphQL.StarWars;
using GraphQL.StarWars.Data;
using GraphQL.StarWars.Enum;
using GraphQL.StarWars.Resolve;
using GraphQL.StarWars.Types;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Example
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Environment.SetEnvironmentVariable("DOTNET_CLI_TELEMETRY_OPTOUT", "1");
            try
            {
                //---General--------------------------------------------------
                ServicePointManager.ReusePort = true;                       // Default: false
                //---Common for all service points--------------------------------------------------
                ServicePointManager.DefaultConnectionLimit = int.MaxValue;  // Default: 2
                ServicePointManager.Expect100Continue = false;              // Default: true
                ServicePointManager.UseNagleAlgorithm = false;              // Default: true
                ServicePointManager.SetTcpKeepAlive(true, 10000, 10000);


           var auxEndpoint = "http://localhost:9002/graphql";
                var auxServiceMng = ServicePointManager.FindServicePoint(new Uri(auxEndpoint));
                auxServiceMng.ConnectionLeaseTimeout = 60 * 1000; // 5 minutes -> 1 minute
            }
            catch (Exception)
            {
            }

        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            // Reference: https://offering.solutions/blog/articles/2017/02/07/difference-between-addmvc-addmvcore/
            services.AddMvcCore().AddJsonFormatters();

            //Returns GZIP
            //--- Compression Gzip ---------------------------------------------
            //services.Configure<GzipCompressionProviderOptions>(options =>
            //options.Level = System.IO.Compression.CompressionLevel.Optimal);
            //services.AddResponseCompression(options =>
            //{
            //options.EnableForHttps = true;
            //options.Providers.Add<GzipCompressionProvider>();
            //});
            //------------------------------------------------------------------

            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(type => s.GetRequiredService(type)));

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddSingleton<SearchResultType>();
            services.AddSingleton<StarWarsData>();
            services.AddSingleton<ExternalData>();     
            services.AddSingleton<StarWarsQuery>();
            services.AddSingleton<StarWarsMutation>();
            services.AddSingleton<HumanType>();
            services.AddSingleton<ReviewInputType>();
            services.AddSingleton<DroidType>();
            services.AddSingleton<StarshipType>();
            services.AddSingleton<CharacterInterface>();
            services.AddSingleton<EpisodeEnum>();
            services.AddSingleton<HeighEnum>();
            services.AddSingleton<IdGraphType>();
            services.AddSingleton<ReviewType>();
            services.AddSingleton<SpecieType>();
            services.AddSingleton<HomeworldType>();

            services.AddSingleton<HeighEnum>();

            services.AddSingleton<ISchema, StarWarsSchema>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(LogLevel.Warning);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphiQl("graphiQL");

            //--- Compression Gzip ---
            //app.UseResponseCompression();

            app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings
            {
                BuildUserContext = ctx => new GraphQLUserContext
                {
                    User = ctx.User
                }
            });
            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            app.UseMvc();

        }
    }
}
