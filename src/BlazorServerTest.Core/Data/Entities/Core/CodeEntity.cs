namespace BlazorServerTest.Core.Data.Entities.Core;

public abstract class CodeEntity : Entity, ICodeEntity
{
    public string Code { get; set; } = string.Empty;
}