// See https://aka.ms/new-console-template for more information
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using System.Data;

Console.WriteLine("Hello, World!");

[ExtensionUri(Id)]
[DefaultExecutorUri(Id)]
public class VsTestExecutor : ITestDiscoverer, ITestExecutor
{
    static VsTestExecutor() {
        Count = int.TryParse(Environment.GetEnvironmentVariable("TEST_COUNT") ?? "1000", out var count) ? count : 1000;
        }
    public const string Id = "executor://perfy.testadapter";
    public static readonly Uri Uri = new Uri(Id);

    public static int Count { get; }

    public void Cancel()
    {
        // noop
    }

    public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
    {
        var sources = tests.Select(t => t.Source).Distinct().ToList();
        RunTests(sources, runContext, frameworkHandle);
    }

    public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
    {
        var location = typeof(VsTestExecutor).Assembly.Location;
        for (var i = 0; i < Count; i++) {
            var tc = new TestCase($"Test{i}", Uri, location);
            frameworkHandle.RecordResult(new TestResult(tc)
            {
                Outcome = TestOutcome.Passed
            });
        }
    }

    public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
    {
        var location = typeof(VsTestExecutor).Assembly.Location;
        for (var i = 0; i < Count; i++)
        {
            discoverySink.SendTestCase(new TestCase($"Test{i}", Uri, location));
        }   
    }
}
