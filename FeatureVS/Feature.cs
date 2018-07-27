using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FeatureVS {
    [Serializable]
    public class Feature {
        private bool _isAbstract = false;
        private bool _isMandatory = false;
        private List<FeatureRelation> _featureRelations = new List<FeatureRelation>();
        private Rectangle _rectangle;
        private TextBlock _textBlock;
        public bool IsAbstract {
            get { return _isAbstract; }
            set { _isAbstract = value; }
        }
        public bool IsMandatory {
            get { return _isMandatory; }
            set { _isMandatory = value; }
        }
        public string Name { get; set; } = "";
        public double XPos { get; set; } = 0;
        public double YPos { get; set; } = 0;
        
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
        public void SetRectangle(Rectangle rectangle) {
            _rectangle = rectangle;
        }
        public Rectangle GetRectangle() {
            return _rectangle;
        }
        public void SetTextBlock(TextBlock textBlock) {
            _textBlock = textBlock;
        }
        public TextBlock GetTextBlock() {
            return _textBlock;
        }
    }
}
