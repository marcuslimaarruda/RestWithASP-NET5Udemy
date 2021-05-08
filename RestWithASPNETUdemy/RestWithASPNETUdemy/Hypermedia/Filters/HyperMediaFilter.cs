using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Hypermedia.Filters
{
    public class HyperMediaFilter : ResultFilterAttribute
    {
        private readonly HypeMediaFiltersOptions _hypeMediaFiltersOptions;

        public HyperMediaFilter(HypeMediaFiltersOptions hypeMediaFiltersOptions) 
        {
            _hypeMediaFiltersOptions = hypeMediaFiltersOptions;
        }

        public override void OnResultExecuting(ResultExecutingContext context) 
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        
        }

        private void TryEnrichResult(ResultExecutingContext context)
        {
           if (context.Result is OkObjectResult objectResult) 
           {
                var enricher = _hypeMediaFiltersOptions.ContentResponseEnricherList.FirstOrDefault(x => x.CanEnrich(context));

                if (enricher != null) Task.FromResult(enricher.Enrich(context));

           }
        }
    }
}
