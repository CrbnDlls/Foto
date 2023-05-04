using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Foto
{
    internal class Settings
    {
        XmlDocument settings = new XmlDocument();
        readonly string[] sizes = { "Foto9x12", "Foto12x15", "Foto18x24" };
        readonly string fileName = "settings.xml";

        public decimal Price9x12
        {
            get
            {
                return decimal.Parse(settings.SelectSingleNode($"/prices/{sizes[0]}").Attributes["price"].Value);
            }
            set
            {
                settings.SelectSingleNode($"/prices/{sizes[0]}").Attributes["price"].Value = value.ToString();
                settings.Save(fileName);
            }
        }

        public decimal Price12x15
        {
            get
            {
                return decimal.Parse(settings.SelectSingleNode($"/prices/{sizes[1]}").Attributes["price"].Value);
            }
            set
            {
                settings.SelectSingleNode($"/prices/{sizes[1]}").Attributes["price"].Value = value.ToString();
                settings.Save(fileName);
            }
        }

        public decimal Price18x24
        {
            get
            {
                return decimal.Parse(settings.SelectSingleNode($"/prices/{sizes[2]}").Attributes["price"].Value);
            }
            set
            {
                settings.SelectSingleNode($"/prices/{sizes[2]}").Attributes["price"].Value = value.ToString();
                settings.Save(fileName);
            }
        }

        public Settings()
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            if (File.Exists(fileName))
            {
                try
                {
                    settings.Load(fileName);
                }
                catch
                { //If not loaded we will create new
                }
                if (!IsSettingsOk())
                {
                    settings = new XmlDocument();
                    CreateSettingsFile();
                }
            }
            else
            {
                CreateSettingsFile();
            }
        }

        private void CreateSettingsFile()
        {
            XmlNode node = settings.CreateElement("prices");
            XmlElement price = settings.CreateElement(sizes[0]);
            XmlAttribute attribute = settings.CreateAttribute("price");
            attribute.Value = "10.5";
            price.Attributes.Append(attribute);
            node.AppendChild(price);
            price = settings.CreateElement(sizes[1]);
            attribute = settings.CreateAttribute("price");
            attribute.Value = "15.5";
            price.Attributes.Append(attribute);
            node.AppendChild(price);
            price = settings.CreateElement(sizes[2]);
            attribute = settings.CreateAttribute("price");
            attribute.Value = "18.5";
            price.Attributes.Append(attribute);
            node.AppendChild(price);
            settings.AppendChild(node);
            
            settings.Save(fileName);
        }

        private bool IsSettingsOk()
        {
            foreach (string size in sizes)
            {
                try
                {
                    decimal.Parse(settings.SelectSingleNode($"/prices/{size}").Attributes["price"].Value);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }
}
