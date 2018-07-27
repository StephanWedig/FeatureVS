namespace FeatureVS {
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;

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
            set { _diagram = value; }
        }
        private WindowDiagramControl() {
            this.InitializeComponent();
        }
        public static WindowDiagramControl getInstance() {
            return instance;
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "WindowDiagram");
        }
    }
}