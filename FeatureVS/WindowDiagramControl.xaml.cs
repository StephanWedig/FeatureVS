namespace FeatureVS {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for WindowDiagramControl.
    /// </summary>
    public partial class WindowDiagramControl : UserControl {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowDiagramControl"/> class.
        /// </summary>
        private static WindowDiagramControl instance = new WindowDiagramControl();
        private FeatureDiagram _diagram = new FeatureDiagram("");
        public FeatureDiagram Diagram {
            get { return _diagram; }
            set {
                canvDiagram.Children.Clear();
                _diagram = value;
                PaintDiagram();
            }
        }
        private WindowDiagramControl() {
            this.InitializeComponent();
            CreateContextMenu();
        }
        public static WindowDiagramControl getInstance() {
            return instance;
        }

        public void PaintDiagram() {
            /*if (_diagram == null || _diagram.RootFeature == null) {
                canvDiagram.Children.Clear();
                return;
            }
            double xpos = this.ActualWidth / 2 - GlobalConstants.FEATUREWIDTH / 2;
            double ypos = 50;*/
            PaintFeature(_diagram.RootFeature);
            //PaintFeature(Diagram.RootFeature, xpos, ypos);
        }
        private void PaintFeature(Feature feature) {
            Rectangle rectangle = new Rectangle {
                Stroke = GlobalConstants.FEATURESTROKEBRUSH,
                Width = GlobalConstants.FEATUREWIDTH,
                Height = GlobalConstants.FEATUREHEIGHT,
                RadiusX = 10,
                RadiusY = 10,
                Fill = GlobalConstants.FEATUREFILLBRUSH
            };
            rectangle.ContextMenu = menuStrip;
            Canvas.SetLeft(rectangle, feature.XPos);
            Canvas.SetTop(rectangle, feature.YPos);
            canvDiagram.Children.Add(rectangle);
            feature.SetRectangle(rectangle);
            TextBlock block = new TextBlock();
            block.Text = feature.Name;
            block.Foreground = GlobalConstants.TEXTBRUSH;
            feature.SetTextBlock(block);
            canvDiagram.Children.Add(block);
            Canvas.SetLeft(block, feature.XPos + (GlobalConstants.FEATUREWIDTH - block.ActualWidth) / 2);
            Canvas.SetTop(block, feature.YPos + (GlobalConstants.FEATUREHEIGHT - block.ActualHeight) / 2 - 10);
            if (feature.FeatureRelations.Count > 0) {
                foreach (FeatureRelation fr in feature.FeatureRelations) {
                    foreach (Feature f in fr.GetChildFeature()) {
                        //PaintRelation(fr, feature.XPos + GlobalConstants.FEATUREWIDTH / 2, feature.YPos + GlobalConstants.FEATUREHEIGHT, f.XPos + GlobalConstants.FEATUREWIDTH / 2, f.YPos);
                        PaintRelation(feature, f);
                        PaintFeature(f);
                    }
                }
            }
        }
        /*private void PaintFeature(Feature feature, double xpos, double ypos) {
            Rectangle rectangle = new Rectangle {
                Stroke = new SolidColorBrush(Colors.White),
                Width = GlobalConstants.FEATUREWIDTH,
                Height = GlobalConstants.FEATUREHEIGHT,
                RadiusX = 10,
                RadiusY = 10,
                Fill = new SolidColorBrush(Colors.LightGray)
            };
            if (feature.XPos != -1 && feature.YPos != -1) {
                xpos = feature.XPos;
                ypos = feature.YPos;
            }
            Canvas.SetLeft(rectangle, xpos);
            Canvas.SetTop(rectangle, ypos);
            canvDiagram.Children.Add(rectangle);
            TextBlock block = new TextBlock();
            block.Text = feature.Name;
            block.Foreground = new SolidColorBrush(Colors.Black);
            canvDiagram.Children.Add(block);
            Canvas.SetLeft(block, xpos + (GlobalConstants.FEATUREWIDTH - block.ActualWidth) / 2 - 10);
            Canvas.SetTop(block, ypos + (GlobalConstants.FEATUREHEIGHT - block.ActualHeight) / 2 - 10);
            if (feature.FeatureRelations.Count > 0) {
                int cntFeature = 0;
                foreach (FeatureRelation fr in feature.FeatureRelations) {
                    cntFeature += fr.getChildFeature().Count;
                }
                double xposFeature = xpos - (cntFeature - 1) * (GlobalConstants.FEATUREWIDTH + GlobalConstants.FEATUREDISX) / 2;
                double yposFeature = ypos + GlobalConstants.FEATUREHEIGHT + GlobalConstants.FEATUREDISY;
                foreach (FeatureRelation fr in feature.FeatureRelations) {
                    foreach (Feature f in fr.getChildFeature()) {
                        PaintRelation(fr, xpos + GlobalConstants.FEATUREWIDTH / 2, ypos + GlobalConstants.FEATUREHEIGHT, xposFeature + GlobalConstants.FEATUREWIDTH / 2, yposFeature);
                        PaintFeature(f, xposFeature, yposFeature);
                        xposFeature += GlobalConstants.FEATUREWIDTH + GlobalConstants.FEATUREDISX;
                    }
                }
            }
        }*/
        private void PaintRelation(Feature fromFeature, Feature toFeature) {
            Line line = toFeature.GetChildLine();
            if (line == null) {
                line = new Line {
                    Stroke = GlobalConstants.FEATURERELATIONBRUSH,
                    StrokeThickness = 1
                };
                toFeature.SetChildLine(line);
                canvDiagram.Children.Add(line);
            }
            line.X1 = fromFeature.XPos + GlobalConstants.FEATUREWIDTH / 2;
            line.X2 = toFeature.XPos + GlobalConstants.FEATUREWIDTH / 2;
            line.Y1 = fromFeature.YPos + GlobalConstants.FEATUREHEIGHT;
            line.Y2 = toFeature.YPos;
        }
        private void PaintRelation(Feature feature, double fromX, double fromY, double toX, double toY) {
            Line line = feature.GetChildLine();
            if (line == null) {
                line = new Line {
                    Stroke = GlobalConstants.FEATURERELATIONBRUSH,
                    X1 = fromX,
                    X2 = toX,
                    Y1 = fromY,
                    Y2 = toY,
                    StrokeThickness = 1
                };
                feature.SetChildLine(line);
                canvDiagram.Children.Add(line);
            }
            line.X1 = fromX;
            line.X2 = toX;
            line.Y1 = fromY;
            line.Y2 = toY;
        }
        private void RefreshRelation(Feature feature) {
            if (feature.FeatureRelations.Count > 0) {
                foreach (FeatureRelation fr in feature.FeatureRelations) {
                    foreach (Feature f in fr.GetChildFeature()) {
                        //PaintRelation(f, feature.XPos + GlobalConstants.FEATUREWIDTH / 2, feature.YPos + GlobalConstants.FEATUREHEIGHT, f.XPos + GlobalConstants.FEATUREWIDTH / 2, f.YPos);
                        PaintRelation(feature, f);
                    }
                }
            }
        }
        private Feature selectedFeature = null;
        private void canvDiagram_MouseMove(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                System.Diagnostics.Debug.WriteLine("MousePressed");
                if (selectedFeature != null) {
                    MoveRectangle(e, selectedFeature);
                } else {
                    MoveFeature(e, _diagram.RootFeature);
                }
            }
        }
        private void MoveRectangle(MouseEventArgs e, Feature feature) {
            Rectangle rec = feature.GetRectangle();
            Point canvPos = e.GetPosition(canvDiagram);
            Canvas.SetLeft(rec, canvPos.X - GlobalConstants.FEATUREWIDTH / 2);
            Canvas.SetTop(rec, canvPos.Y - GlobalConstants.FEATUREHEIGHT / 2);
            feature.XPos = canvPos.X - GlobalConstants.FEATUREWIDTH / 2;
            feature.YPos = canvPos.Y - GlobalConstants.FEATUREHEIGHT / 2;
            TextBlock block = feature.GetTextBlock();
            Canvas.SetLeft(block, feature.XPos + (GlobalConstants.FEATUREWIDTH - block.ActualWidth) / 2);
            Canvas.SetTop(block, feature.YPos + (GlobalConstants.FEATUREHEIGHT - block.ActualHeight) / 2);
            RefreshRelation(feature);
            Line line = feature.GetChildLine();
            PaintRelation(feature, line.X1, line.Y1, feature.XPos + GlobalConstants.FEATUREWIDTH / 2, feature.YPos);
        }
        private void MoveFeature(MouseEventArgs e, Feature feature) {
            Rectangle rec = feature.GetRectangle();
            /*if(rec.IsMouseOver)
                System.Diagnostics.Debug.WriteLine("IsMouseOver");
            if (selectedFeature != null) {
                System.Diagnostics.Debug.WriteLine("selectedFeature");
                rec = selectedFeature.GetRectangle();
                feature = selectedFeature;
            }*/

            if (rec.IsMouseOver) {
                selectedFeature = feature;
                MoveRectangle(e, feature);
            } else if (feature.FeatureRelations.Count > 0) {
                foreach (FeatureRelation fr in feature.FeatureRelations) {
                    foreach (Feature f in fr.GetChildFeature()) {
                        MoveFeature(e, f);
                    }
                }
            }
        }

        private void canvDiagram_MouseUp(object sender, MouseButtonEventArgs e) {
            selectedFeature = null;
        }

        private void canvDiagram_MouseLeave(object sender, MouseEventArgs e) {
            selectedFeature = null;
        }
        ContextMenu menuStrip = new ContextMenu();
        private void CreateContextMenu() {
            MenuItem menuItem = new MenuItem();
            menuItem.Header = "Create Feature Above";
            menuItem.Click += new RoutedEventHandler(CreateFeatureAbove);
            menuItem.Name = "CreateFeatureAbove";

            menuStrip.Items.Add(menuItem);
        }
        private void CreateFeatureAbove(object sender, EventArgs e) {

        }
    }
}