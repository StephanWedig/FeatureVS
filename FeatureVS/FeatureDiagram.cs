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
            FeatureRelation rel = new FeatureRelation(_rootFeature);
            rel.Type = FeatureRelation.RelationType.Other;
            Feature f1 = new Feature();
            f1.Name = "A";
            Feature f2 = new Feature();
            f2.Name = "B";
            rel.AddChildFeature(f1);
            rel.AddChildFeature(f2);
        }
        public static FeatureDiagram open() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "feature diagram (*.fd)|*.fd";
            if (openFileDialog.ShowDialog() != DialogResult.OK) {
                return null;
            }
            string filename = openFileDialog.FileName;
            FeatureDiagram diagram = new FeatureDiagram(filename);

            XmlSerializer ser = new XmlSerializer(typeof(FeatureDiagram));
            FileStream reader = new FileStream(filename, FileMode.Open);
            diagram.RootFeature = (Feature)ser.Deserialize(reader);
            reader.Close();
            System.Diagnostics.Debug.WriteLine(diagram.RootFeature.Name);
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
