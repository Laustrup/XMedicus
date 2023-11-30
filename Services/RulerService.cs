using XMedicus.Models;
using Newtonsoft.Json.Linq;

namespace XMedicus.Services
{
    public class RulerService
    {
        private static readonly string _url = "https://gist.githubusercontent.com/adbir/e8b768cc854f0499034cd40fcf34a720/raw";

        private readonly string _idKey = "id",
            _nameKey = "nm",
            _yearKey = "yrs",
            _houseKey = "hse";

        public async Task<List<Ruler>> GetRulersAsync()
        {
            List<Ruler> rulers = new List<Ruler>();
            JArray json = JArray.Parse(await new HttpClient().GetStringAsync(_url));
            Console.WriteLine($"\tFetched json \n\"{json}\"\n\n at {_url}");

            for (int i = 0; i < json.Count; i++)
            {
                try
                {
                    rulers.Add(ConvertFromJson(json,i));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nCouldn't convert ruler with index {i}\n{e.Message}");
                }
            }

            return await Task.FromResult(rulers);
        }

        private Ruler ConvertFromJson(JArray json, int index)
        {
            var ruler = json[index];
            string[] splittedYears = ((string) ruler[_yearKey]).Split("-");

            return new Ruler(
                (int) ruler[_idKey],
                (string) ruler[_nameKey],
                splittedYears[0] == "" || splittedYears[1] == "present"
                    ? new Rule(int.Parse(splittedYears[0] == "" ? splittedYears[1] : splittedYears[0]), splittedYears[1] == "present")
                    : new Rule(int.Parse(splittedYears[0]),int.Parse(splittedYears[1])),
                (string) ruler[_houseKey] != "" ? (string) ruler[_houseKey] : "Uden kendt slægt"
            );
        }
    }
}