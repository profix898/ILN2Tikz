using System;
using System.Windows.Forms;
using ILN2Tikz.Generator;
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

            var surface = new ILSurface((x, y) => (float) (Math.Sin(x) * Math.Cos(y) * Math.Exp(-(x * x + y * y) / 46)));
            surface.Colormap = Colormaps.ILNumerics;
            plotCube.Add(surface);

            ilPanel.Configure();
            ilPanel.Refresh();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            var exportString = ILN2Tikz.ILN2Tikz.ExportString(ilPanel.Scene);
        }
    }
}
