using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    [Serializable()]
    public class Loss_factors
    {
        public double Turb_Avail_Loss;   // Turbine availability loss
        public double BOP_Avail_Loss;   // BOP availability loss
        public double Electrical_Loss;  // Electrical loss
        public double Icing_Loss;   // Icing loss
        public double Blade_Soil_Loss;  // Blade soiling loss
        public double Blade_Degrade_Loss;  // Blade degradation loss
        public double HighLowTemp_Loss;   // Extreme (Hi/Low) temperatre loss
        public double Power_Crv_Loss;  // power curve loss
        public double Turbulence_Loss;   // Turbulence loss
        public double Wake_Sect_Curtail_Loss;   // Wake sector management curtailment loss
        public double Environ_Curtail_Loss;  // Environmental curtailment loss
        public double Grid_Curtail_Loss;  // Grid curtailment loss
        
        public void Set_Defaults()
        {
            // Sets loss factors to default values
            Turb_Avail_Loss = 0.035f;
            BOP_Avail_Loss = 0.01f;
            Electrical_Loss = 0.025f;
            Icing_Loss = 0.0f;
            Blade_Soil_Loss = 0.01f;
            Blade_Degrade_Loss = 0.01f;
            HighLowTemp_Loss = 0.0f;
            Power_Crv_Loss = 0.015f;
            Turbulence_Loss = 0.005f;
            Wake_Sect_Curtail_Loss = 0.0f;
            Environ_Curtail_Loss = 0.0f;
            Grid_Curtail_Loss = 0.0f;
        }

        public double Get_Total_Losses()
        {
            // Calculates and returns total losses (other than wake loss)
            double Total_Loss_factor = 0; // Total other losses (not including wake losses) saved as decimal

            Total_Loss_factor = (1 - Turb_Avail_Loss) * (1 - BOP_Avail_Loss) * (1 - Electrical_Loss) * (1 - Icing_Loss) * (1 - Blade_Soil_Loss) * (1 - Blade_Degrade_Loss) * (1 - HighLowTemp_Loss) *
                (1 - Power_Crv_Loss) * (1 - Turbulence_Loss) * (1 - Wake_Sect_Curtail_Loss) * (1 - Environ_Curtail_Loss) * (1 - Grid_Curtail_Loss);
            Total_Loss_factor = 1 - Total_Loss_factor;

            return Total_Loss_factor;
        }
    }
}
