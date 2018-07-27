using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace FeatureVS {
    public class FeatureDiagram {
        private string _Filename = "";
        private Feature _rootFeature;
        public FeatureDiagram() { }
        public FeatureDiagram (string filename) {
            _Filename = filename;
            _rootFeature = new Feature();
            _rootFeature.Name = "root";
            _rootFeature.XPos = 387.5;
            _rootFeature.YPos = 25;
            FeatureRelation rel = new FeatureRelation(_rootFeature);
            rel.Type = FeatureRelation.RelationType.Other;
            Feature f1 = new Feature();
            f1.Name = "A";
            f1.XPos = 50;
            f1.YPos = 125;
            Feature f2 = new Feature();
            f2.Name = "B";
            f2.XPos = 275;
            f2.YPos = 125;
            rel.AddChildFeature(f1);
            rel.AddChildFeature(f2);
            FeatureRelation rel1 = new FeatureRelation(_rootFeature);
            rel1.Type = FeatureRelation.RelationType.Other;
            Feature f3 = new Feature();
            f3.Name = "C";
            f3.XPos = 500;
            f3.YPos = 125;
            Feature f4 = new Feature();
            f4.Name = "D";
            f4.XPos = 725;
            f4.YPos = 125;
            rel1.AddChildFeature(f3);
            rel1.AddChildFeature(f4);
        }
        public static FeatureDiagram open() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "feature diagram (*.fd)|*.fd";
            if (openFileDialog.ShowDialog() != DialogResult.OK) {
                return null;
            }
            string filename = openFileDialog.FileName;
            FeatureDiagram diagram = new FeatureDiagram(filename);

            XmlSerializer ser = new XmlSerializer(typeof(Feature));
            FileStream reader = new FileStream(filename, FileMode.Open);
            diagram.RootFeature = (Feature)ser.Deserialize(reader);
            reader.Close();
            return diagram;
        }
        public Feature RootFeature {
            get { return _rootFeature; }
            set { _rootFeature = value; }
        }
        public void save() {
            if (_Filename == "")
                saveAs();

            XmlSerializer ser = new XmlSerializer(typeof(Feature));

            System.Diagnostics.Debug.WriteLine(RootFeature.GetRelations()[0].getChildFeature().Count);
            TextWriter writer = new StreamWriter(_Filename,false);
            ser.Serialize(writer, _rootFeature);
            writer.Close();
        }
        public void saveAs() {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Feature Diagram (*.fd)|*.fd";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            _Filename = saveFileDialog.FileName;
            save();
        }
    }
}
