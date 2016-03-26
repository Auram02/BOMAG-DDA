using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dealer_Locator.BR
{
    public class StyleGenerator
    {

        private double _hue = 0;
        private double _saturation = 240;
        private double _luminosity = 120;
        private int _hueStep = 15;
        private int _saturationStep = 5;
        private int _luminosityStep = 30;

        private double _hueLowerRange = 20;
        private double _hueUpperRange = 240;

        private double _saturationLowerRange = 60;
        private double _saturationUpperRange = 250;

        private double _luminosityLowerRange = 40;
        private double _luminosityUpperRange = 200;


        public struct StyleDefinition
        {
            public string StrokeColor;
            public double StrokeOpacity;
            public double StrokeWeight;
            public string FillColor;
            public double FillOpacity;
        }

        public StyleDefinition GetStyle()
        {
            Dealer_Locator.BR.HSLColor colorClass = new HSLColor();

            if (_saturation < _saturationLowerRange)
                _saturation = _saturationUpperRange;

            if (_luminosity > _luminosityUpperRange)
            {
                _luminosity = _luminosityLowerRange;
            }

            HSLColor color = new HSLColor(_hue, _saturation, _luminosity);

            if (_hue >= _hueUpperRange)
            {
                _hue = _hueLowerRange;

                _saturation -= _saturationStep;
                _luminosity += _luminosityStep;

            }
            else
            {
                _hue += _hueStep;
            }

            StyleDefinition def = new StyleDefinition();
            def.FillColor = HexConverter(color);
            def.FillOpacity = 0.8;
            def.StrokeColor = "#FFFFFF";
            def.StrokeOpacity = 0.35;
            def.StrokeWeight = 1;

            return def;
        }

        public StyleDefinition GetStyleOverlap()
        {
            Dealer_Locator.BR.HSLColor colorClass = new HSLColor();

            if (_saturation < _saturationLowerRange)
                _saturation = _saturationUpperRange;

            if (_luminosity > _luminosityUpperRange)
            {
                _luminosity = _luminosityLowerRange;
            }

            HSLColor color = new HSLColor(_hue, _saturation, _luminosity);

            if (_hue >= _hueUpperRange)
            {
                _hue = _hueLowerRange;

                _saturation -= _saturationStep;
                _luminosity += _luminosityStep;

            }
            else
            {
                _hue += _hueStep;
            }

            StyleDefinition def = new StyleDefinition();
            def.FillColor = HexConverter(color);
            def.FillOpacity = 0.8;
            def.StrokeColor = "#F40000";
            def.StrokeOpacity = 0.35;
            def.StrokeWeight = 1.20;

            return def;
        }

        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

    }
}