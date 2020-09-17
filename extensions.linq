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
	
}

public class ConsoleObserver<T> : IObserver<T>
{
	private readonly string _name;
	public ConsoleObserver(string name = "")
	{
		_name = name;
	}
	public void OnNext(T value)
	{
		Console.WriteLine("{0} - OnNext({1})", _name, value);
	}
	public void OnError(Exception error)
	{
		Console.WriteLine("{0} - OnError:", _name);
		Console.WriteLine("\t {0}", error);
	}
	public void OnCompleted()
	{
		Console.WriteLine("{0} - OnCompleted()", _name);
	}
}

// Define other methods, classes and namespaces here
public static class Extensions
{
	public static IDisposable SubscribeConsole<T>(
	this IObservable<T> observable,
	string name = "")
	{
		return observable.Subscribe(new ConsoleObserver<T>(name));
	}
}
