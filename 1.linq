<Query Kind="Program">
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System.Collections.Specialized</Namespace>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Drawing</Namespace>
  <Namespace>System.Drawing.Imaging</Namespace>
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
  <Namespace>System.Windows.Interop</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>System.Windows.Markup.Primitives</Namespace>
  <Namespace>System.Windows.Media.Converters</Namespace>
  <Namespace>System.Windows.Threading</Namespace>
</Query>

void Main()
{
	var form = new Form();
	form.WindowState = FormWindowState.Maximized;

	control = new PictureBox();
	control.Width = 1000;
	control.Height = 1000;
	control.Dock = DockStyle.Top | DockStyle.Left;
	
    ClearControl();

	var groupBox = new FlowLayoutPanel();
	groupBox.Name = "Buttons";
	groupBox.Dock = DockStyle.Top | DockStyle.Right;

	var buttonOhne = new Button();
	buttonOhne.Text = "Ohne";
	groupBox.Controls.Add(buttonOhne);

	var buttonKurz = new Button();
	buttonKurz.Text = "Kurz";
	groupBox.Controls.Add(buttonKurz);

	var buttonLange = new Button();
	buttonLange.Text = "Lange";
	groupBox.Controls.Add(buttonLange);

	var buttonOff = new Button();
	buttonOff.Text = "Aus";
	groupBox.Controls.Add(buttonOff);

	var buttonClear = new Button();
	buttonClear.Text = "Clear";
	groupBox.Controls.Add(buttonClear);

	form.Controls.Add(groupBox);
	var observer = Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(
						handler => control.MouseMove += handler,
						handler => control.MouseMove -= handler);

	buttonOhne.Click += (sender, args) => CreateSubscription(observer, 0);
	buttonKurz.Click += (sender, args) => CreateSubscription(observer, .05);
	buttonLange.Click += (sender, args) => CreateSubscription(observer, 1);
	buttonOff.Click += (sender, args) => subscription?.Dispose();
	buttonClear.Click += (sender, args) => ClearControl();

	form.Controls.Add(control);
	form.ShowDialog();
}

void ClearControl()
{
	control.Image = new Bitmap(1000, 1000, PixelFormat.Format24bppRgb);
	using (var gra = Graphics.FromImage(control.Image))
	{
		gra.FillRectangle(Brushes.White, 0, 0, control.Image.Width, control.Image.Height);
	};
}

void CreateSubscription(IObservable<EventPattern<MouseEventArgs>> observer, double delay)
{
	subscription?.Dispose();
	subscription = observer.Delay(TimeSpan.FromSeconds(delay)).Subscribe(o => control.Invoke((MethodInvoker)(() => DrawLine(o))));
}


IDisposable subscription;
PictureBox control;
int startX = int.MaxValue;
int startY = int.MinValue;
Pen pen = new Pen(Color.Black, 2);


object @lock = new object();

void DrawLine(EventPattern<MouseEventArgs> o)
{
	var args = o.EventArgs;
	lock (@lock)
	{
		if (args.Button != MouseButtons.Left)
		{
			startX = int.MaxValue;
			startY = int.MinValue;
			return;
		}

		if (startX != int.MaxValue)
		{
			using (var gra = Graphics.FromImage(control.Image))
			{
				gra.DrawLine(pen, startX, startY, args.X, args.Y);
			}
			control.Invalidate();
		}

		startX = args.X;
		startY = args.Y;
	}
}

// Define other methods and classes here
