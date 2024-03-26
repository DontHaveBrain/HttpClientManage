using IHttpClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientPor
{
    [Export(typeof(IHttpClientPor))]
    public class HttpClientPor: IHttpClientPor
    {
        ServiceCollection serviceCollection = new ServiceCollection();
        IHttpClientFactory httpClientFactory = null;
        public  HttpClientPor(List<NameWithUri> CreareMessage)
        {
            foreach (var createMsg in CreareMessage)
            {
                if (createMsg.Uri==null)
                {
                    serviceCollection.AddHttpClient(createMsg.Name);
                    continue;
                }
                serviceCollection.AddHttpClient(createMsg.Name, (x) => { x.BaseAddress = createMsg.Uri;  });
            }
            ServiceProvider service = serviceCollection.BuildServiceProvider();
            httpClientFactory = service.GetRequiredService<IHttpClientFactory>();
            //创建
        }
        public IHttpClientFactory GetHttpClientFactory()
        {
            return this.httpClientFactory;
        }
    }
}
