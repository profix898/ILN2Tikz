using System;
using System.Drawing;
using System.Windows.Forms;
using ILN2Tikz;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using Size = System.Drawing.Size;

namespace TikzDemo
{
    public partial class TikzDemoForm : Form
    {
        public TikzDemoForm()
        {
            InitializeComponent();

            comboBoxScene.Items.Add("LinePlot");
            comboBoxScene.Items.Add("LinePlot (log)");
            comboBoxScene.Items.Add("Surface");
        }

        private void TikzDemoForm_Load(object sender, EventArgs e)
        {
            comboBoxScene.SelectedIndex = 0;
        }

        private void comboBoxScene_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxScene.SelectedIndex == -1)
                return;

            var plotCube = new PlotCube();
            plotCube.Axes.XAxis.Ticks.TickLength = -plotCube.Axes.XAxis.Ticks.TickLength; // Ticks Inside
            plotCube.Axes.YAxis.Ticks.TickLength = -plotCube.Axes.YAxis.Ticks.TickLength; // Ticks Inside
            plotCube.Axes.ZAxis.Ticks.TickLength = -plotCube.Axes.ZAxis.Ticks.TickLength; // Ticks Inside
            plotCube.AspectRatioMode = AspectRatioMode.MaintainRatios;

            if (comboBoxScene.SelectedIndex == 0) // LinePlot
            {
                Array<float> A = ILMath.tosingle(ILMath.rand(13, 10)) + ILMath.arange<float>(0, 12);
                var linePlot = LinePlot.CreateXPlots(A,
                    lineStyles: new[]
                    {
                        DashStyle.Solid, DashStyle.Solid, DashStyle.Solid, DashStyle.Solid, DashStyle.Dashed,
                        DashStyle.Dashed, DashStyle.Dashed, DashStyle.PointDash, DashStyle.PointDash,
                        DashStyle.PointDash, DashStyle.Dotted, DashStyle.Dotted, DashStyle.Dotted
                    },
                    lineWidth: new[] {1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1});

                plotCube.Add(linePlot);
            }

            if (comboBoxScene.SelectedIndex == 1) // LinePlot (log)
            {
                Array<double> x = ILMath.logspace(0, 3);
                Array<float> A = ILMath.zeros<float>(3, x.Length);
                A[0, Globals.full] = ILMath.tosingle(x);

                Array<double> y1 = 10 + ILMath.abs(ILMath.randn(1, 50)) * x;
                A[1, Globals.full] = ILMath.tosingle(y1);
                var linePlot1 = new LinePlot(A, markerStyle: MarkerStyle.Cross, markerColor: Color.Crimson);
                plotCube.Add(linePlot1);

                Array<double> y2 = ILMath.abs(ILMath.randn(1, 50)) * x * x;
                A[1, Globals.full] = ILMath.tosingle(y2);
                var linePlot2 = new LinePlot(A, markerStyle: MarkerStyle.Cross, markerColor: Color.BlueViolet);
                plotCube.Add(linePlot2);

                plotCube.ScaleModes.XAxisScale = AxisScale.Logarithmic;
                plotCube.ScaleModes.YAxisScale = AxisScale.Logarithmic;
                plotCube.Axes.XAxis.Label.Text = "Voltage / V_{rms}";

                //plotCube.Add(new Legend("one", "two"));
            }

            if (comboBoxScene.SelectedIndex == 2) // Surface
            {
                var surface = new Surface((x, y) =>
                        (float) (Math.Sin(x) * Math.Cos(y) * Math.Exp(-(x * x + y * y) / 46)),
                    -10, 10, 40, -5, 5, 40);
                surface.Colormap = Colormaps.ILNumerics;

                plotCube.Add(surface);
                plotCube.TwoDMode = false;
            }

            ilPanel.Scene = new Scene();
            ilPanel.Scene.Add(plotCube);

            ilPanel.Configure();
            ilPanel.Refresh();
        }

        private void btnExportFile_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "TIKZ Image|*.tikz";
            saveFileDialog.Title = "Save as TIKZ Image";
            saveFileDialog.ShowDialog();

            if (string.IsNullOrEmpty(saveFileDialog.FileName))
                return;

            var size = new Size(120, 120);
            var currentScene = ilPanel.GetCurrentScene() ?? ilPanel.Scene;
            ILN2TikzExport.ExportFile(currentScene, saveFileDialog.FileName, size);
        }

        private void btnExportText_Click(object sender, EventArgs e)
        {
            var currentScene = ilPanel.GetCurrentScene() ?? ilPanel.Scene;
            textBoxTikz.Text = ILN2TikzExport.ExportString(currentScene);
        }
    }
}