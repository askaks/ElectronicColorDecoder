using ElectronicColorCodes.Models;
using Newtonsoft.Json;
using OhmCalculator;
using System;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;

namespace ElectronicColorCodes.Controllers
{ 
    public class ColorController : Controller
    {
        // GET: Color
        public ActionResult Resistor()
        {
            ViewBag.Message = "Calculate the Ohms of your resistor";
            ColorBands vs = new ColorBands();
            return View(vs);
        }


        [HttpPost]
        public ActionResult Calculate(string s)
        {
            Color myColor = Color.PaleVioletRed;

            // Create the ColorConverter.
            System.ComponentModel.TypeConverter converter =
            System.ComponentModel.TypeDescriptor.GetConverter(myColor);
            var colorBands = JsonConvert.DeserializeObject<ColorBands>(s);

            Color a = ColorTranslator.FromHtml(colorBands.BandAColor.Replace("#", "#FF"));
            Color b = ColorTranslator.FromHtml(colorBands.BandBColor.Replace("#", "#FF"));
            Color c = ColorTranslator.FromHtml(colorBands.BandCColor.Replace("#", "#FF"));
            Color d = ColorTranslator.FromHtml(colorBands.BandDColor.Replace("#", "#FF"));

            var colormsg = a.Name + b.Name + c.Name + d.Name;
            OhmValueCalculator ohmValueCalculator = new OhmValueCalculator();
            try
            {
                double[] tolerance = ohmValueCalculator.FindTolerance(d.Name);
                string meaningfulTolerance = tolerance?.ElementAt(0) == 0 ? "" : tolerance[0].ToString() + "";
                meaningfulTolerance = tolerance?.ElementAt(1) == 0 ? meaningfulTolerance : "±" + meaningfulTolerance;

                double ohmValue = ohmValueCalculator.CalculateOhmValue(a.Name, b.Name, c.Name, d.Name);
                return Json(new
                {
                    msg = "Successfully added ",
                    ohm = ohmValue,
                    colors = colormsg,
                    tolerance = meaningfulTolerance
                });
            }

            catch(ArgumentException argException)
            {
                return Json(new
                {
                    msg = "Resistor with bands selected does not exist.",
                    ohm = -1,
                    colors = colormsg
                });
            }

            catch(Exception exc)
            {
                return Json(new
                {
                    msg = "Error",
                    ohm = -1,
                    colors = colormsg
                });
            }
        }
    }
}