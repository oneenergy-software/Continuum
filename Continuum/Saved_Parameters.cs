using System;

namespace ContinuumNS
{
    /// <summary> Class that holds CFM file, topography, and land cover file path names. </summary>
    [Serializable()]
    public class Saved_Parameters
    {               
        /// <summary> Saved CFM filename </summary>
        public string savedFileName;
        /// <summary> Saved folder location </summary>
        public string pathName;
        /// <summary> Name of topography file (displayed on Input tab) </summary>
        public string topoFileName;
        /// <summary> Name of land cover file (displayed on Input tab) </summary>
        public string landCoverFileName;

        public int genMapMinUTMX;   // don//t need these
        public int genMapMaxUTMX;
        public int genMapMinUTMY;
        public int genMapMaxUTMY;
        public int genMapReso;

        /// <summary> Clears all saved parameters </summary>
        public void ClearAll()
        {                   
            savedFileName = "";
            pathName = "";
            topoFileName = "";
            landCoverFileName = "";
        }
    }
}
