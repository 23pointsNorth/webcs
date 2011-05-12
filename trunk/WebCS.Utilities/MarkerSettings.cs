using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marker;

namespace WebCS.Utilities
{
    public class MarkerSettings : SettingsBase
    {
        public List<ColorMarker> markersList = new List<ColorMarker>();

        public override void ReadSettings(UserSettingsReader reader)
        {
            //markersList = reader.Read();
            int count = reader.Read("MarkerCount", 0);
            for (int i = 0; i < count; i++)
            {
                markersList.Add(reader.Read(
                    "Marker" + i.ToString(), new ColorMarker()));
            }
        }

        public override void WriteSettings(UserSettingsWriter writer)
        {
            writer.Write("MarkerCount", markersList.Count);
            for (int i = 0; i < markersList.Count; i++)
            {
                writer.Write("Marker" + i.ToString(), markersList[i]);
            }

        }
    }
}
