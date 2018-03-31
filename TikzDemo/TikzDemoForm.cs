using System;
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
        }

        private void TikzDemoForm_Load(object sender, EventArgs e)
        {
            var plotCube = new ILPlotCube();
            plotCube.TwoDMode = false;
            ilPanel.Scene.Add(plotCube);

            ILArray<float> A = ILMath.tosingle(ILMath.rand(13, 10)) + ILMath.vec<float>(0, 12);
            var linePlot = ILLinePlot.CreateXPlots(A,
                lineStyles: new[] { DashStyle.Solid, DashStyle.Solid, DashStyle.Solid, DashStyle.Solid, DashStyle.Dashed, DashStyle.Dashed, DashStyle.Dashed, DashStyle.PointDash, DashStyle.PointDash, DashStyle.PointDash, DashStyle.Dotted, DashStyle.Dotted, DashStyle.Dotted },
                lineWidth: new[] {1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1});
            plotCube.Add(linePlot);

            //var surface = new ILSurface((x, y) => (float) (Math.Sin(x) * Math.Cos(y) * Math.Exp(-(x * x + y * y) / 46)));
            //surface.Colormap = Colormaps.ILNumerics;
            //plotCube.Add(surface);

            ilPanel.Configure();
            ilPanel.Refresh();
        }

        private void btnExportFile_Click(object sender, EventArgs e)
        {
            ILN2Tikz.ILN2Tikz.ExportFile(ilPanel.Scene, textBoxExportPath.Text);
        }

        private void btnExportText_Click(object sender, EventArgs e)
        {
            textBoxTikz.Text = ILN2Tikz.ILN2Tikz.ExportString(ilPanel.Scene);
        }
    }
}