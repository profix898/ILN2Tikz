using System;
using System.Drawing;
using System.Windows.Forms;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

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

            var plotCube = new ILPlotCube();

            if (comboBoxScene.SelectedIndex == 0) // LinePlot
            {
                ILArray<float> A = ILMath.tosingle(ILMath.rand(13, 10)) + ILMath.vec<float>(0, 12);
                var linePlot = ILLinePlot.CreateXPlots(A,
                    lineStyles: new[] { DashStyle.Solid, DashStyle.Solid, DashStyle.Solid, DashStyle.Solid, DashStyle.Dashed, DashStyle.Dashed, DashStyle.Dashed, DashStyle.PointDash, DashStyle.PointDash, DashStyle.PointDash, DashStyle.Dotted, DashStyle.Dotted, DashStyle.Dotted },
                    lineWidth: new[] {1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1});

                plotCube.Add(linePlot);
            }

            if (comboBoxScene.SelectedIndex == 1) // LinePlot (log)
            {
                ILArray<double> x = ILMath.logspace(0, 3);
                ILArray<double> y = ILMath.abs(ILMath.randn(1, 30)) * x * x;
                ILArray<float> A = ILMath.zeros<float>(2, x.Length);
                A[0, ILMath.full] = ILMath.tosingle(x);
                A[1, ILMath.full] = ILMath.tosingle(y);
                var linePlot = new ILLinePlot(A, markerStyle: MarkerStyle.Cross, markerColor: Color.Crimson);

                plotCube.Add(linePlot);
                plotCube.ScaleModes.XAxisScale = AxisScale.Logarithmic;
                plotCube.ScaleModes.YAxisScale = AxisScale.Logarithmic;
            }

            if (comboBoxScene.SelectedIndex == 2) // Surface
            {
                var surface = new ILSurface((x, y) =>
                    (float) (Math.Sin(x) * Math.Cos(y) * Math.Exp(-(x * x + y * y) / 46)),
                    -10, 10, 40, -5, 5, 40);
                surface.Colormap = Colormaps.ILNumerics;

                plotCube.Add(surface);
                plotCube.TwoDMode = false;
            }

            ilPanel.Scene = new ILScene();
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

            ILN2Tikz.ILN2Tikz.ExportFile(ilPanel.Scene, saveFileDialog.FileName);
        }

        private void btnExportText_Click(object sender, EventArgs e)
        {
            textBoxTikz.Text = ILN2Tikz.ILN2Tikz.ExportString(ilPanel.Scene);
        }
    }
}