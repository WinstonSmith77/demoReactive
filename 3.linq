<Query Kind="Program">
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System.Reactive</Namespace>
  <Namespace>System.Reactive.Concurrency</Namespace>
  <Namespace>System.Reactive.Disposables</Namespace>
  <Namespace>System.Reactive.Joins</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
  <Namespace>System.Reactive.PlatformServices</Namespace>
  <Namespace>System.Reactive.Subjects</Namespace>
  <Namespace>System.Reactive.Threading.Tasks</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Input</Namespace>
  <Namespace>System.Windows.Media</Namespace>
  <Namespace>System.Windows.Shapes</Namespace>
</Query>

void Main()
{
	var window = new Window();

	var canvas = new Canvas();
	canvas.VerticalAlignment = VerticalAlignment.Stretch;
	canvas.HorizontalAlignment = HorizontalAlignment.Stretch;
	canvas.Background = Brushes.Azure;

	window.Content = canvas;

	window.Show();

	var mouseDowns = Observable.FromEventPattern<MouseButtonEventHandler, MouseButtonEventArgs>(handler => canvas.MouseDown += handler, handler => canvas.MouseDown -= handler);
	var mouseUp = Observable.FromEventPattern<MouseButtonEventHandler, MouseButtonEventArgs>(handler => canvas.MouseUp += handler, handler => canvas.MouseUp -= handler);
	var movements = Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(handler => canvas.MouseMove += handler, handler => canvas.MouseMove -= handler);

	Polyline line = null;

	movements.SkipUntil(
			mouseDowns.Do(_ =>
								{
									line = new Polyline() { Stroke = Brushes.Black, StrokeThickness = 3 };
									canvas.Children.Add(line);
								}
						)
			)
	.TakeUntil(mouseUp)
	.Select(m => m.EventArgs.GetPosition(canvas))
	.Repeat()
	.Delay(TimeSpan.FromMilliseconds(1000))
	.Subscribe(pos => canvas.Dispatcher.Invoke((() => line.Points.Add(pos))));
}

// Define other methods, classes and namespaces here
