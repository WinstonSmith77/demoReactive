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

#load ".\extensions"


void Main()
{
	var source = new[] {1,2,3}.ToObservable().Repeat(2);

	source.Skip(3).SubscribeConsole("A");
	
	Task.Delay(4000).GetAwaiter().GetResult();
	
	source.SubscribeConsole("B");
}

// Define other methods and classes here
