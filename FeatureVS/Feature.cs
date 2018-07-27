using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureVS {
    [Serializable]
    public class Feature {
        private bool _isAbstract = false;
        private bool _isMandatory = false;
        private string _name = "";
        private List<FeatureRelation> _featureRelations = new List<FeatureRelation>();
        public bool IsAbstract {
            get { return _isAbstract; }
            set { _isAbstract = value; }
        }
        public bool IsMandatory {
            get { return _isMandatory; }
            set { _isMandatory = value; }
        }
        public string Name {
            get { return _name; }
            set { _name = value; }
        }
        public List<FeatureRelation> FeatureRelations {
            get { return _featureRelations; }
        }
        public void addRelation(FeatureRelation featureRelation) {
            if(_featureRelations.IndexOf(featureRelation) == -1)
                _featureRelations.Add(featureRelation);
        }
        public List<FeatureRelation> GetRelations() {
            return _featureRelations;
        }
    }
}
