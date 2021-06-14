using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Measurement.Models;
using Plotly.Blazor;
using Plotly.Blazor.Traces;
using Plotly.Blazor.Traces.ScatterLib;
using System.Collections.Generic;
using System.Linq;

namespace LetItGrow.UI.Node.Extensions
{
    public static class ChartExtensions
    {
        public static Scatter ToScatter(this ICollection<IrrigationModel> irrigations)
        {
            var x = irrigations.Select(x => x.IssuedAt.ToLocalTime()).Cast<object>().ToList();
            var y = irrigations.Select(x => x.Type.ToString()).Cast<object>().ToList();

            return new Scatter
            {
                Name = "Irrigations",
                Mode = ModeFlag.Markers,
                Marker = new()
                {
                    Size = 20,
                    Symbol = Plotly.Blazor.Traces.ScatterLib.MarkerLib.SymbolEnum.Square,
                },
                X = x,
                Y = y
            };
        }

        public static IList<ITrace> ToTraceList(this ICollection<IrrigationModel> irrigations) =>
            new List<ITrace> { irrigations.ToScatter() };

        public static Scatter[] ToScatter(this ICollection<MeasurementModel> measurements)
        {
            var x = measurements.Select(x => x.MeasuredAt).Cast<object>().ToList();
            var y1 = measurements.Select(x => x.AirTemperatureC).Cast<object>().ToList();
            var y2 = measurements.Select(x => x.AirHumidity).Cast<object>().ToList();
            var y3 = measurements.Select(x => x.SoilMoisture).Cast<object>().ToList();

            return new[]
            {
                new Scatter
                {
                    Name = "Air Temerature C",
                    Mode = ModeFlag.Lines | ModeFlag.Markers,
                    X = x,
                    Y = y1
                },
                new Scatter
                {
                    Name = "Air Humidity %",
                    Mode = ModeFlag.Lines | ModeFlag.Markers,
                    X = x,
                    Y = y2
                },
                new Scatter
                {
                    Name = "Soil Moisture %",
                    Mode = ModeFlag.Lines | ModeFlag.Markers,
                    X = x,
                    Y = y3
                }
            };
        }

        public static IList<ITrace> ToTraceList(this ICollection<MeasurementModel> measurements) =>
            new List<ITrace>(measurements.ToScatter());
    }
}