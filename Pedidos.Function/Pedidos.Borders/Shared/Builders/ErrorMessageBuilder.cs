namespace Pedidos.Borders.Shared.Builders
{
    public class ErrorMessageBuilder(string code, string messageTemplate)
    {
        public readonly string Code = code;

        public ErrorMessage Build(params object?[] args) => new(Code, string.Format(messageTemplate, args));
    }

    public class ErrorMessageBuilder<T1, T2>(string code, string messageTemplate, Func<T1, T2, object[]> map) : ErrorMessageBuilder(code, messageTemplate)
    {
        public ErrorMessage Build(T1 t1, T2 t2) => Build(map(t1, t2));
    }
}
