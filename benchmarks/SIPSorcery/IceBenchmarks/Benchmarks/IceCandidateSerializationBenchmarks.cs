using BenchmarkDotNet.Attributes;
using SIPSorcery.Net;

namespace IceBenchmarks.Benchmarks;

public class IceCandidateSerializationBenchmarks
{
#if !LibVersion
    private readonly System.Text.StringBuilder _builder = new();
#endif
    private RTCIceCandidate _candidate = null!;

    public IEnumerable<BenchmarkInput> Inputs()
    {
        yield return new("UDP host", "candidate:1 1 udp 2130706431 192.0.2.10 5000 typ host generation 0");
        yield return new("UDP server reflexive", "candidate:2 1 udp 1677734910 203.0.113.1 50000 typ srflx raddr 192.168.1.10 rport 8998 generation 0");
        yield return new("TCP relay", "candidate:3 1 tcp 1518280447 203.0.113.20 443 typ relay tcptype passive raddr 192.168.1.10 rport 5000 generation 0");
    }

    [ParamsSource(nameof(Inputs))]
    public BenchmarkInput Input { get; set; } = null!;

    [GlobalSetup]
    public void GlobalSetup() => _candidate = RTCIceCandidate.Parse(Input.Value);

    [Benchmark]
    public string String_ToString() => _candidate.ToString();

    [Benchmark]
    public int String_WriteString()
    {
#if LibVersion
        return 0;
#else
        _builder.Clear();
        _candidate.WriteString(_builder);
        return _builder.Length;
#endif
    }

    [Benchmark]
    public string ToJson() => _candidate.toJSON();
}
