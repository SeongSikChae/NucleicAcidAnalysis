namespace NucleicAcidAnalyzer.NucleicAcid.Parser
{
    public sealed class NucleicAcidParseExpception(string? message, Exception? innerException = null) : Exception(message, innerException)
    {
    }
}
