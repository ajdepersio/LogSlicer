using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogSlicer
{
    //* Functions to do any sort of Loading of settings or defaults.  
    public class QuickSelect
    {
        private string _name;
        private List<string> _types;
        private static List<QuickSelect> _quickSelects = new List<QuickSelect>();
        

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        


        public List<string> Types
        {
            get
            {
                return this._types;
            }
            set
            {
                this._types = value;
            }
        }

        public static List<QuickSelect> QuickSelects
        {
            get 
            {
                return _quickSelects;
            }
            set
            {
                _quickSelects = value;
            }
        }

        public QuickSelect()
        {
            this.Types = new List<string>{"MissingNo"};
        }

        public QuickSelect(string name, List<string> types)
        {
            this.Name = name;
            this.Types = types;
            QuickSelects.Add(this);
        }

        public void WriteToConfig()
        {
            Config.Save("QuickSelectNames", this.Name, false, false);
            
            foreach(string type in this.Types)
            {
                Config.Save(this.Name, type, false, false);
            }
        }

        public static List<QuickSelect> LoadQuickSelectsFromConfig()
        {
            List<QuickSelect> results = new List<QuickSelect>();
            List<string> quickSelectTypes = new List<string>();

            List<string> quickSelectNames = Config.Load("QuickSelectNames", false).Split(',').ToList();

            foreach(string quickSelect in quickSelectNames)
            {
                if (quickSelect != "")
                {
                    quickSelectTypes = Config.Load(quickSelect, false).Split(',').ToList();
                    results.Add(new QuickSelect(quickSelect, quickSelectTypes));
                }
            }
            return results;
        }

        
    }
}
