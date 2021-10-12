using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_Msyt_Editor
{

    public class MSYT
    {
        public int group_count { get; set; }
        public int atr1_unknown { get; set; }
        public Dictionary<string, EntriesClass> entries { get; set; }
    }

    public class EntriesClass
    {
        public string attributes { get; set; }
        public Dictionary<string, ControlClass> contents { get; set; }
    }

    public class ControlClass
    {
        //Kind: 
        public string kind { get; set; }

        //Kind: Animation Params
        public string name { get; set; }

        //Kind: AutoAdvance Params
        public int frames { get; set; }
    }
}