using XMedicus.Models;
using Newtonsoft.Json.Linq;

namespace XMedicus.Services
{
    ///<summary>
    ///Takes care of logic and algorithms according to <c>Ruler<c>
    ///</summary>
    public class RulerService
    {
        ///<summary>
        ///The URL which the <c>Rulers</c> will be fetched
        ///<summary>
        private static readonly string _url = "https://gist.githubusercontent.com/adbir/e8b768cc854f0499034cd40fcf34a720/raw";

        ///<summary>
        /// An attribute of the fetched <c>Rulers'</c> keys
        ///</summary>
        private readonly string _idKey = "id",
            _nameKey = "nm",
            _yearKey = "yrs",
            _houseKey = "hse";

        ///<summary>
        ///Gets the <c>Rulers<c> from the URL of gist.githubusercontent.com through HttpClient.
        ///Also converts them from JSON to <c>Ruler</c>
        ///</summary>
        ///<returns>The fetched and converted <c>Rulers</c></returns>
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

        ///<summary>
        ///The logic that are used to convert a fetched JSON file into a <c>Rulers</c> object
        ///</summary>
        ///<param name="json">The fetched JSON element as an array</param>
        ///<param name="index">The current index of the JSON array that will be converted</param>
        ///<returns>The converted JSON object of the index</returns>
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