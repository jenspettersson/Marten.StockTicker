using Owin;

namespace Marten.StockTicker.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}