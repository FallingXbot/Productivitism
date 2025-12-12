using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Productivitism.Model
{
    internal class Saves
    {
        public static (TimeSpan, TimeSpan, int) Init()
        {
            string rawdata = File.ReadAllText(@"K:\Repo For VS\Projects\Productivitism\Productivitism\TimerData.json");
            var data = JsonSerializer.Deserialize<TData>(rawdata);



            return (TimeSpan.Parse(data.TimeForABreak), TimeSpan.Parse(data.AllTimeSpended), data.Tcoins);

        }
        
        public static void SaveData(TimeSpan Break, TimeSpan AllTime, int coins)
        {
            TData data = new TData();
            data.TimeForABreak = Break.ToString();
            data.AllTimeSpended = AllTime.ToString();
            data.Tcoins = coins;

            string rawdata = File.ReadAllText(@"K:\Repo For VS\Projects\Productivitism\Productivitism\TimerData.json");
            string newdata = JsonSerializer.Serialize(data);
            File.WriteAllText(@"K:\Repo For VS\Projects\Productivitism\Productivitism\TimerData.json", newdata);
        }
       

    }
}
