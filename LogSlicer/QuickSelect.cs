using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogSlicer
{
    //* Predefined Log set for commonly used snips 
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
            this.Name = "MissingNo";
            this.Types = new List<string>{"MissingNo"};
        }

        /// <summary>
        /// Creates new QuickSelect object
        /// </summary>
        /// <param name="name">Name of QuickSelect</param>
        /// <param name="types">List of log types to include</param>
        public QuickSelect(string name, List<string> types)
        {
            this.Name = name;
            this.Types = types;
            QuickSelects.Add(this);
        }

        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Saves QuickSelect object to app.config
        /// </summary>
        public void WriteToConfig()
        {
            Config.Save("QuickSelectNames", this.Name, false, false);
            
            foreach(string type in this.Types)
            {
                Config.Save(this.Name, type, false, false);
            }
        }

        public void Delete()
        {
            //Remote from master list
            List<string> quickSelectNames = Config.Load("QuickSelectNames", false).Split(',').ToList();
            quickSelectNames.Remove(this.Name);
            Config.Save("QuickSelectNames", string.Join(",", quickSelectNames.ToArray()), false, true);

            //Delete the quickselect log sets
            Config.Delete(this.Name);

            //Remove from QuickSelects List
            QuickSelects.Remove(QuickSelects.Find(x => x.Name == this.Name));

        }

        /// <summary>
        /// Loads QuickSelects from app.config
        /// </summary>
        /// <returns>List of configured log sets</returns>
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
