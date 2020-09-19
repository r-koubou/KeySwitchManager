using CommandLine;

namespace KeySwitchManager.Cli.Commons
{
    public class CliArgumentsParser<T>
    {
        public T Option { get; }

        public bool ParsedArguments { get; }

        public CliArgumentsParser( string[] args )
        {
            ParsedArguments = ParseCommandlineOption( args, out var option );
            Option          = option;
        }

        private bool ParseCommandlineOption( string[] args, out T target )
        {
            target = default!;

            var result = Parser.Default.ParseArguments<T>( args );

            if( result.Tag == ParserResultType.NotParsed )
            {
                return false;
            }

            var parsed = (Parsed<T>)result;
            target = parsed.Value;

            return true;
        }
    }
}