using System.Text;
using Xunit.Abstractions;

namespace Tests;

public class TestOutputHelperWriter(ITestOutputHelper output) : TextWriter
{
    public override Encoding Encoding => Encoding.UTF8;

    public override void WriteLine(string? message)
    {
        output.WriteLine(message);
    }

    public override void Write(string? message)
    {
        output.WriteLine(message);
    }
}