using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static ContinuumNS.Met_Data_Filter;

namespace ContinuumNS
{
    internal class LegacyDataConverter : ISerializationSurrogate
    {        
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            throw new NotSupportedException();
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            if (obj is Met_Data_Filter.data)
            {
                Met_Data_Filter.data result = new Met_Data_Filter.data();

                result.timeStamp = info.GetDateTime("timeStamp");
                result.min = (float)info.GetDouble("min");
                result.max = (float)info.GetDouble("max");
                result.avg = (float)info.GetDouble("avg");
                result.SD = (float)info.GetDouble("SD");
                result.filterFlag =
                    (Filter_Flags)info.GetValue(
                        "filterFlag",
                        typeof(Filter_Flags));

                return result;
            }

            if (obj is Met_Data_Filter.Est_Alpha)
            {
                Met_Data_Filter.Est_Alpha result = new Met_Data_Filter.Est_Alpha();

                result.timeStamp = info.GetDateTime("timeStamp");
                result.alpha = (float)info.GetDouble("alpha");
                result.WD = (float)info.GetDouble("WD");
                result.WS = (float)info.GetDouble("WS");

                return result;
            }

            if (obj is Met_Data_Filter.Est_Data)
            {
                Met_Data_Filter.Est_Data result = new Met_Data_Filter.Est_Data();

                result.timeStamp = info.GetDateTime("timeStamp");
                result.WS = (float)info.GetDouble("WS");
                result.WD = (float)info.GetDouble("WD");
                result.SD = (float)info.GetDouble("SD");
                result.alpha = (float)info.GetDouble("alpha");

                return result;
            }

            if (obj is MCP.Site_data)
            {
                MCP.Site_data result = new MCP.Site_data();

                result.thisDate = info.GetDateTime("thisDate");
                result.thisWS = (float)info.GetDouble("thisWS");
                result.thisWD = (float)info.GetDouble("thisWD");

                return result;
            }

            if (obj is Reference.Wind_TS_with_Prod)
            {
                Reference.Wind_TS_with_Prod result = new Reference.Wind_TS_with_Prod();

                result.thisDate = info.GetDateTime("thisDate");
                result.WS = (float)info.GetDouble("WS");
                result.WD = (float)info.GetDouble("WD");
                result.prod = (float)info.GetDouble("prod");
                result.seaPress = (float)info.GetDouble("seaPress");
                result.surfPress = (float)info.GetDouble("surfPress");
                result.temperature = (float)info.GetDouble("temperature");
                result.gustWS = (float)info.GetDouble("gustWS");

                return result;
            }

            if (obj is Reference.YearAndProd)
            {
                Reference.YearAndProd result = new Reference.YearAndProd();

                result.prod = (float)info.GetDouble("prod");
                return result;
            }

            throw new SerializationException($"Unexpected type: {obj.GetType().FullName}");
        }
        
    }
}
