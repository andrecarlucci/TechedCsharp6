using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hadouken.Services {
    public class LightService {
        public async Task Blink() {
            using (HttpClient client = new HttpClient()) {
                await client.GetAsync("http://192.168.10.253/cgi-bin/relay.cgi?on");
                await Task.Delay(2000);
                await client.GetAsync("http://192.168.10.253/cgi-bin/relay.cgi?off");
            }
        }
    }
}