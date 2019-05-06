using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class Weather
    {
        public int Id;
        public int ComboId;

        public Weather(int _id, int _comboId) {
            this.Id = _id;
            this.ComboId = _comboId;
        }
    }

    public class WeatherMaster : BaseMaster
    {
        private static List<Weather> weatherMaster = new List<Weather>();

        public bool IsWeather(int _id) {
            return weatherMaster.Exists(n => n.ComboId == _id);
        }

        public override void ResetData() {
            weatherMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                weatherMaster.Add(new Weather(int.Parse(master[0]), int.Parse(master[1])));
            }
        }
    }
}
