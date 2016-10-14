using System.Data;
using Marten.StockTicker.Ticker;
using Marten.StockTicker.Tracker;
using MediatR;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using StructureMap;

namespace Marten.StockTicker.Host
{
    public class Bootstrapper : StructureMapNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IContainer container)
        {
            var documentStore = DocumentStore.For(_ =>
            {
                _.Connection("host=localhost;database=marten.stockticker;username=postgres;password=admin");
                _.Events.InlineProjections.AggregateStreamsWith<TrackedStock>();
                _.Events.AddEventType(typeof(TrackingStarted));
                _.Events.AddEventType(typeof(StockRateChanged));

                _.AutoCreateSchemaObjects = AutoCreate.All;
            });

            container.Configure(_ =>
            {
                _.ForSingletonOf<IWriter>().Use<ConsoleScreenWriter>();
                _.ForSingletonOf<IDocumentStore>().Use(documentStore);
                
                _.For<IDocumentSession>().Use(ctx => ctx.GetInstance<IDocumentStore>().OpenSession(DocumentTracking.None, IsolationLevel.ReadCommitted));
                _.For<IQuerySession>().Use(ctx => ctx.GetInstance<IDocumentStore>().QuerySession());

                _.AddRegistry<MediatorRegistry>();
            });

            base.ConfigureApplicationContainer(container);
        }

        protected override void ConfigureRequestContainer(IContainer container, NancyContext context)
        {
            container.Configure(_ =>
            {
                _.For<IDocumentSession>().Use(container.GetInstance<IDocumentStore>().OpenSession());
            });

            base.ConfigureRequestContainer(container, context);
        }

        protected override void ApplicationStartup(IContainer container, IPipelines pipelines)
        {
            container.GetInstance<IMediator>().SendAsync(new StartTicker());
            
            base.ApplicationStartup(container, pipelines);
        }
    }
}