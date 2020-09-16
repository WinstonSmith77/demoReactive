<Query Kind="Program">
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System</Namespace>
  <Namespace>System.Collections.Specialized</Namespace>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Diagnostics</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.IO.Packaging</Namespace>
  <Namespace>System.Reactive</Namespace>
  <Namespace>System.Reactive.Concurrency</Namespace>
  <Namespace>System.Reactive.Disposables</Namespace>
  <Namespace>System.Reactive.Joins</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
  <Namespace>System.Reactive.PlatformServices</Namespace>
  <Namespace>System.Reactive.Subjects</Namespace>
  <Namespace>System.Reactive.Threading.Tasks</Namespace>
  <Namespace>System.Resources</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Security.Permissions</Namespace>
  <Namespace>System.Security.RightsManagement</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Threading.Tasks.Sources</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Converters</Namespace>
  <Namespace>System.Windows.Data</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Windows.Forms.Automation</Namespace>
  <Namespace>System.Windows.Forms.ComponentModel.Com2Interop</Namespace>
  <Namespace>System.Windows.Forms.Design</Namespace>
  <Namespace>System.Windows.Forms.Layout</Namespace>
  <Namespace>System.Windows.Forms.PropertyGridInternal</Namespace>
  <Namespace>System.Windows.Forms.VisualStyles</Namespace>
  <Namespace>System.Windows.Input</Namespace>
  <Namespace>System.Windows.Interop</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>System.Windows.Markup.Primitives</Namespace>
  <Namespace>System.Windows.Media</Namespace>
  <Namespace>System.Windows.Media.Converters</Namespace>
  <Namespace>System.Windows.Threading</Namespace>
  <Namespace>System.Drawing</Namespace>
  <Namespace>System.Drawing.Imaging</Namespace>
</Query>

void Main()
{
	var form = new Form();

	var control = new PictureBox();
	control.Width = 1000;
	control.Height = 1000;
	control.Dock = DockStyle.Top | DockStyle.Left;
	control.Image = new Bitmap(1000, 1000, PixelFormat.Format24bppRgb);

	using (var gra = Graphics.FromImage(control.Image))
	{
		gra.FillRectangle(Brushes.White, 0, 0, control.Image.Width, control.Image.Height);
	};

	var startX = int.MaxValue;
	var startY = int.MinValue;
	var pen = new Pen(Color.Black, 2);

	control.MouseMove += (sender, args) =>
	{
		using (var gra = Graphics.FromImage(control.Image))
		{
			if (args.Button != MouseButtons.Left)
			{
				startX = int.MaxValue;
				startY = int.MinValue;
				return;
			}

			if (startX != int.MaxValue)
			{
				gra.DrawLine(pen, startX, startY, args.X, args.Y);
			}
			startX = args.X;
			startY = args.Y;
		}

		control.Invalidate();
	};


	form.Controls.Add(control);
	form.ShowDialog();
}

// Define other methods and classes here
