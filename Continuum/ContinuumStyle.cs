using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot.WindowsForms;
using OxyPlot;
using DnsClient.Protocol;

namespace ContinuumNS
{
    public static class ContinuumStyle
    {        
        
        // Fonts
        public static readonly Font NormalFont = new Font("Segoe UI", 9F, FontStyle.Regular);

        public static readonly Font SectionFont = new Font("Segoe UI", 10F, FontStyle.Bold);

        public static readonly Font PageTitleFont = new Font("Segoe UI", 14F, FontStyle.Bold);

        public static readonly Font TableFont = new Font("Segoe UI", 9F, FontStyle.Regular);

        public static readonly Font TableHeaderFont = new Font("Segoe UI", 9F, FontStyle.Bold);

        // Colors
        public static readonly Color PrimaryColor = Color.FromArgb(46, 125, 50);

        public static int GetNumDecimals(string paramType, bool isPerc = false)
        {
            int numSigDigs = 0;

            if (paramType == "WS" || paramType == "ws" || paramType == "WindSpeed")
                numSigDigs = 2;
            else if (paramType == "WD" || paramType == "wd" || paramType == "WindDirection")
                numSigDigs = 1;
            else if (paramType == "Temp" || paramType == "temp" || paramType == "Temperature")
                numSigDigs = 1;
            else if (paramType == "RH")
                numSigDigs = 1;
            else if (paramType == "Pressure" || paramType == "pressure" || paramType == "Press")
                numSigDigs = 1;
            else if (paramType == "SR" || paramType == "Roughness")
                numSigDigs = 3;
            else if (paramType == "Exposure" || paramType == "Expo")
                numSigDigs = 1;
            else if (paramType == "Alpha" || paramType == "Shear")
                numSigDigs = 3;
            else if (paramType == "Elevation" || paramType == "Elev")
                numSigDigs = 1;
            else if (paramType == "Energy")
                numSigDigs = 1;
            else if (paramType == "GWh")
                numSigDigs = 2;
            else if (paramType == "CF")
                numSigDigs = 2;
            else if (paramType == "Wake" || paramType == "WakeLoss")
                numSigDigs = 2;
            else if (paramType == "Weibull" || paramType == "weibull")
                numSigDigs = 2;
            else if (paramType == "DispH" || paramType == "DH")
                numSigDigs = 2;
            else if (paramType == "PFs" || paramType == "PerfFactors")
                numSigDigs = 2;
            else if (paramType == "ExpoCoeff" || paramType == "Coeff")
                numSigDigs = 4;
            else if (paramType == "dWS SRDH" || paramType == "dWS_SRDH")
                numSigDigs = 4;
            else if (paramType == "dWS")
                numSigDigs = 3;
            else if (paramType == "LatLong" || paramType == "Lat" || paramType == "Long")
                numSigDigs = 3;
            else if (paramType == "TI" || paramType == "Turbulence")
                numSigDigs = 2;
            else if (paramType == "Error")
                numSigDigs = 2;

            if (isPerc)
                numSigDigs = numSigDigs + 2; // Percentages get converted to decimal values, so we need to add 2 to the number of significant digits

            return numSigDigs;
        }

        public static void ApplyStyles(Form form)
        {
            StyleButtons(form);
            ApplyFonts(form);
        }

        public static void StylePrimaryButton(Button button)
        {           
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = PrimaryColor;
            button.FlatAppearance.MouseDownBackColor = PrimaryColor;
        }

        public static void StyleButton(Button button)
        {
            button.Font = NormalFont;

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;

            button.BackColor = SystemColors.Control;
            button.ForeColor = Color.Black;

            button.TextAlign = ContentAlignment.MiddleCenter;

            button.Padding = new Padding(6, 2, 6, 2);

            button.AutoSize = false;

       /*     if (button.Text.Length < 25)
                button.Height = 28;
            else
                button.Height = 50;
       */
        }

        public static void StylePlot(PlotView plot, int titleFontSize = 0)
        {
            if (plot.Model == null)
                return;

            var model = plot.Model;

            model.Background = OxyColors.White;

            if (plot.Model.PlotType != PlotType.Polar)
            {
                model.PlotAreaBorderColor = OxyColors.Gray;
                model.PlotAreaBorderThickness = new OxyThickness(1);
            }
            else
                model.PlotAreaBorderThickness = new OxyThickness(0);

            model.TextColor = OxyColors.Black;

            if (titleFontSize > 0)
                model.TitleFontSize = titleFontSize;

            foreach (var axis in model.Axes)
            {
                axis.TextColor = OxyColors.Black;
                axis.TitleColor = OxyColors.Black;

          //      axis.Font = NormalFont.Name;
          //      axis.FontSize = NormalFont.Size;

          //      axis.TitleFontSize = NormalFont.Size;

                axis.MajorGridlineStyle = LineStyle.Solid;
                axis.MajorGridlineColor = OxyColor.FromRgb(220, 220, 220);

                axis.MinorGridlineStyle = LineStyle.None;
            }
        }

        public static void StyleButtons(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button button)                
                    StyleButton(button);                
                if (control.HasChildren)                
                    StyleButtons(control);                
            }
        }


        public static void ApplyFonts(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                ApplyFont(control);

                if (control.HasChildren)
                {
                    ApplyFonts(control);
                }
            }
        }

        private static void ApplyFont(Control control)
        {
            switch (control)
            {
                case Button button:
                    button.Font = NormalFont;
                    break;

                case TextBox textBox:
                    if (textBox.Font.Size < 11F)
                        textBox.Font = NormalFont;
                    else
                        textBox.Font = new Font(NormalFont.FontFamily, textBox.Font.Size, textBox.Font.Style);
                    break;

                case ComboBox comboBox:
                    comboBox.Font = NormalFont;
                    break;

                case CheckBox checkBox:
                    checkBox.Font = NormalFont;
                    break;

                case RadioButton radioButton:
                    radioButton.Font = NormalFont;
                    break;

                case DataGridView grid:
                    grid.Font = TableFont;
                    grid.ColumnHeadersDefaultCellStyle.Font = TableHeaderFont;
                    break;

                case TabControl tabControl:
                    tabControl.Font = NormalFont;
                    break;

                case Label label:
                    if (label.Font.Bold && label.Font.Size >= 15F)
                        label.Font = PageTitleFont;
                    else if (label.Font.Bold || label.Font.Size >= 11F)                    
                        label.Font = SectionFont;                    
                    else                    
                        label.Font = NormalFont;                    

                    break;

                case MenuStrip menuItem:
                    menuItem.Font = NormalFont;
                    break;
            }
        }
    }
}
