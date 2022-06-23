using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneMenu
{
    public class SezioneMenu : IMenuItemConUrl
    {
        public string Titolo { get; set; }

        public string Url { get; set; } = "#";

        public bool HaLink
        {
            get
            {
                return this.Url != "#";
            }
        }

        [XmlAttribute(AttributeName = "target")]
        public string Target { get; set; } = "_self";

        [XmlAttribute(AttributeName = "completa-url")]
        public bool CompletaUrl { get; set; } = true;

        public List<MenuItem> Items { get; set; }

        public SezioneMenu()
        {
            this.Items = new List<MenuItem>();
        }
    }
}
