using BenchmarkDotNet.Attributes;
using SIPSorcery.Net;

namespace IceBenchmarks.Benchmarks;

public class IceServerParsingBenchmarks
{
    public IEnumerable<BenchmarkInput> Inputs()
    {
        yield return new("STUN", "stun:stun.example.com:3478");
        yield return new("TURN with credentials", "turn:turn.example.com:3478?transport=tcp;user1;pass1");
        yield return new("Multiple URLs", "\"stun:stun1.example.com, stun:stun2.example.com\"");
    }

    [ParamsSource(nameof(Inputs))]
    public BenchmarkInput Input { get; set; } = null!;

    [Benchmark]
    public IceServer Parse() => IceServer.ParseIceServer(Input.Value);
}
