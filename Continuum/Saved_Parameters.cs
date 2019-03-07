using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    [Serializable()]
    public class Saved_Parameters
    {
        public string topoText; // Name of topography file
        public int topoMapColor; // Color scheme selected (not used with Nevron graphics)
        public bool topoMapAuto; // True if auto min/max is selected
        public double topoMapMin;   // topo plot min elevation contour
        public double topoMapMax;  // topo plot max elevation contour
        public double topoMapInterval;  // topo map contour interval
        public bool topoMap3D;   // False if 2D is selected, true if 3D (not used anymore)

        public int genMapMinUTMX;   // don//t need these
        public int genMapMaxUTMX;
        public int genMapMinUTMY;
        public int genMapMaxUTMY;
        public int genMapReso;

        public string savedFileName;
        public string pathName;
        
        public void ClearAll()
        {
            // Clears all saved parameters
            //  Notes_Maps = Nothing

            //  Wind_rose_Text = Nothing
            topoText = null;

            topoMapColor = 0;
            topoMapAuto = true;

            genMapMinUTMX = 0;
            genMapMinUTMY = 0;
            genMapMaxUTMX = 0;
            genMapMaxUTMY = 0;
            genMapReso = 0;

    }

    }
}
