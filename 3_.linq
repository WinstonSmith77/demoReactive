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

private class MoveOrDown
{
	public MouseButtonEventArgs Down;
	public MouseEventArgs Move;
}

void Main()
{
	var window = new Window();

	var canvas = new Canvas();
	canvas.VerticalAlignment = VerticalAlignment.Stretch;
	canvas.HorizontalAlignment = HorizontalAlignment.Stretch;
	canvas.Background = Brushes.Azure;

	window.Content = canvas;

	window.Show();

	var mouseDowns = Observable.FromEventPattern<MouseButtonEventHandler, MouseButtonEventArgs>(handler => canvas.MouseDown += handler, handler => canvas.MouseDown -= handler)
							   .Select(handler => new MoveOrDown { Down = handler.EventArgs });
	var mouseUp = Observable.FromEventPattern<MouseButtonEventHandler, MouseButtonEventArgs>(handler => canvas.MouseUp += handler, handler => canvas.MouseUp -= handler);
	var movements = Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(handler => canvas.MouseMove += handler, handler => canvas.MouseMove -= handler)
								.Select(handler => new MoveOrDown { Move = handler.EventArgs });

	

	movements
	.SkipUntil(mouseDowns)
	.TakeUntil(mouseUp)
	//.Select(m => m.Move.GetPosition(canvas))
	.Repeat()
	.Merge(mouseDowns)
	
	//.Delay(TimeSpan.FromMilliseconds(10))
	.Subscribe(pos => canvas.Dispatcher.Invoke((() =>
		{
			if (pos.Down != null)
			{
				var line = new Polyline() { Stroke = Brushes.Black, StrokeThickness = 3 };
				canvas.Children.Add(line);
			}
			else
			{
				var line = (Polyline)canvas.Children.Cast<UIElement>().Last();
				line.Points.Add(pos.Move.GetPosition(canvas));
			}
		})));
}

// Define other methods, classes and namespaces here
