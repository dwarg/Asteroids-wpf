using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace asteroids.Models{

    public class ResultsManager{
        private StreamReader stream { get; set; }

        public List<Result> GetResults(){
            using (var stream = new StreamReader("results.json")){
                string list = stream.ReadToEnd();
                stream.Close();
                return JsonConvert.DeserializeObject<List<Result>>(list);
            }
        }

        public void SaveResults(List<Result> results){
            var newResults = results.OrderByDescending(x => x.Points).Take(5);
            string rawResult = JsonConvert.SerializeObject(newResults);
            File.WriteAllText("results.json", rawResult);
        }
    }
}
