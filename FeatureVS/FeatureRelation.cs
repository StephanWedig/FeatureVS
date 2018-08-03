using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace FeatureVS {
    [Serializable]
    public class FeatureRelation {
        /*
         * ORGroup: Select 1-n elements
         * AlternativeGroup: Select only once
         * Other: Select 0-n elements
         */
        public enum RelationType { ORGroup, AlternativeGroup, Other };
        private Feature _parent;
        private List<Feature> _childs = new List<Feature>();
        public List<Feature> Children {
            get { return _childs; }
        }
        private RelationType _type = RelationType.Other;
        public RelationType Type {
            get { return _type; }
            set { _type = value; }
        }
        public FeatureRelation(Feature parent) {
            _parent = parent;
            parent.addRelation(this);
        }
        public FeatureRelation() {
        }
        public Feature GetParent() {
            return _parent;
        }
        public void AddChildFeature(Feature child) {
            _childs.Add(child);
        }
        public List<Feature> GetChildFeature() {
            return _childs;
        }
    }
}
